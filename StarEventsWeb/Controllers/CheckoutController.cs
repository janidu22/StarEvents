using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QRCoder;
using StarEvents.DataAccess.Data;
using StarEvents.DataAccess.EmailHelper;
using StarEvents.DataAccess.Models;
using StarEventsWeb.ViewModels;
using System.Text;

namespace StarEventsWeb.Controllers
{
    [Authorize]
    [Route("Checkout")] // base route
    public class CheckoutController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger<CheckoutController> _logger;

        public CheckoutController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IEmailSender emailSender, ILogger<CheckoutController> logger)
        {
            _context = context;
            _userManager = userManager;
            _emailSender = emailSender;
            _logger = logger;
        }

        [HttpGet("BuyNow/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> BuyNow(int id)
        {
            var ev = await _context.Events.Include(e => e.Venue).FirstOrDefaultAsync(e => e.Id == id && e.IsActive);
            if (ev == null) return NotFound();
            var vm = new BuyTicketViewModel
            {
                EventId = ev.Id,
                Title = ev.Title,
                VenueName = ev.Venue?.Name ?? string.Empty,
                StartDateTime = ev.StartDateTime,
                TicketPrice = ev.TicketPrice,
                AvailableTickets = ev.AvailableTickets,
                Quantity = 1
            };
            return View(vm);
        }

        [HttpPost("BuyNow")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BuyNow(BuyTicketViewModel vm)
        {
            var ev = await _context.Events.Include(e => e.Venue).FirstOrDefaultAsync(e => e.Id == vm.EventId && e.IsActive);
            if (ev == null) return NotFound();

            if (vm.Quantity <= 0) ModelState.AddModelError(nameof(vm.Quantity), "Quantity must be at least 1");
            if (vm.Quantity > ev.AvailableTickets) ModelState.AddModelError(nameof(vm.Quantity), $"Only {ev.AvailableTickets} tickets left");
            if (!ModelState.IsValid)
            {
                vm.Title = ev.Title; vm.TicketPrice = ev.TicketPrice; vm.AvailableTickets = ev.AvailableTickets; vm.StartDateTime = ev.StartDateTime; vm.VenueName = ev.Venue?.Name ?? string.Empty;
                return View(vm);
            }

            using var tx = await _context.Database.BeginTransactionAsync();
            Booking booking;
            Ticket ticket;
            try
            {
                ticket = await GetOrCreateDefaultTicket(ev);
                var remaining = ticket.QuantityAvailable - ticket.QuantitySold;
                if (vm.Quantity > remaining)
                {
                    ModelState.AddModelError(nameof(vm.Quantity), $"Only {remaining} tickets left");
                    await tx.RollbackAsync();
                    vm.Title = ev.Title; vm.TicketPrice = ev.TicketPrice; vm.AvailableTickets = ev.AvailableTickets; vm.StartDateTime = ev.StartDateTime; vm.VenueName = ev.Venue?.Name ?? string.Empty;
                    return View(vm);
                }

                var userId = _userManager.GetUserId(User)!;
                var customer = await _context.Customers.FirstOrDefaultAsync(c => c.UserId == userId);
                if (customer == null)
                {
                    customer = new Customer { UserId = userId, Address = string.Empty, DateOfBirth = DateTime.UtcNow, LoyaltyPoints = 0 };
                    _context.Customers.Add(customer);
                    await _context.SaveChangesAsync();
                }

                booking = new Booking
                {
                    CustomerID = customer.Id,
                    EventID = ev.Id,
                    BookingDate = DateTime.UtcNow,
                    TotalAmount = vm.Quantity * ticket.Price,
                    PaymentStatus = "Pending"
                };
                _context.Bookings.Add(booking);
                await _context.SaveChangesAsync();

                var detail = new BookingDetails
                {
                    BookingID = booking.Id,
                    Quantity = vm.Quantity,
                    Subtotal = vm.Quantity * ticket.Price,
                    TicketID = ticket.Id
                };
                _context.BookingDetails.Add(detail);

                // reduce counts
                ticket.QuantitySold += vm.Quantity;
                ev.AvailableTickets -= vm.Quantity;

                // create individual e-tickets with unique codes
                var issuedList = new List<ETicket>();
                for (int i = 1; i <= vm.Quantity; i++)
                {
                    var code = GenerateCode(booking.Id, ticket.Id, i);
                    issuedList.Add(new ETicket
                    {
                        BookingID = booking.Id,
                        TicketID = ticket.Id,
                        QRCode = code,
                        IssuedDate = DateTime.UtcNow,
                        IsUsed = false
                    });
                }
                _context.ETickets.AddRange(issuedList);

                await _context.SaveChangesAsync();
                await tx.CommitAsync();

                // Email QRs (best effort)
                try
                {
                    var user = await _userManager.GetUserAsync(User);
                    if (user?.Email is string to && !string.IsNullOrWhiteSpace(to))
                    {
                        var svgGrid = new StringBuilder();
                        foreach (var et in issuedList)
                        {
                            var svg = RenderQrSvg(et.QRCode);
                            svgGrid.Append($"<div style='display:inline-block;margin:6px;border:1px solid #eee;padding:6px'><div style='font-size:12px;color:#666'>Ticket #{et.Id}</div>{svg}</div>");
                        }
                        var emailBody = $@"<p>Thanks for your reservation for <strong>{ev.Title}</strong>.</p>
<p>Date: {ev.StartDateTime:dd MMM yyyy HH:mm}<br/>Venue: {ev.Venue?.Name}</p>
<p>Please present the QR(s) at entry:</p>
<div>{svgGrid}</div>";
                        await _emailSender.SendEmailAsync(to, $"Your e-tickets for {ev.Title}", emailBody);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "Failed to send confirmation email for booking {BookingId}", booking.Id);
                }

                TempData["LastBookingId"] = booking.Id;
                return RedirectToAction("Tickets", new { id = booking.Id });
            }
            catch (Exception ex)
            {
                await tx.RollbackAsync();
                _logger.LogError(ex, "Error while creating booking");
                ModelState.AddModelError(string.Empty, "Unable to complete purchase at this time. Please try again.");
                vm.Title = ev.Title; vm.TicketPrice = ev.TicketPrice; vm.AvailableTickets = ev.AvailableTickets; vm.StartDateTime = ev.StartDateTime; vm.VenueName = ev.Venue?.Name ?? string.Empty;
                return View(vm);
            }
        }

        [HttpGet("Tickets/{id}")]
        public async Task<IActionResult> Tickets(int id)
        {
            var userId = _userManager.GetUserId(User)!;
            var booking = await _context.Bookings
                .Include(b => b.Event)
                .FirstOrDefaultAsync(b => b.Id == id);
            if (booking == null) return NotFound();
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.UserId == userId);
            if (customer == null || booking.CustomerID != customer.Id) return Forbid();

            var ets = await _context.ETickets.Where(e => e.BookingID == booking.Id).OrderBy(e => e.Id).ToListAsync();
            var vm = new BookingTicketsViewModel
            {
                BookingId = booking.Id,
                EventTitle = booking.Event.Title,
                EventDateText = booking.Event.StartDateTime.ToString("dd MMM yyyy HH:mm"),
                Count = ets.Count,
                Tickets = ets.Select(e => new TicketCardVm
                {
                    ETicketId = e.Id,
                    Code = e.QRCode,
                    Svg = RenderQrSvg(e.QRCode),
                    IsUsed = e.IsUsed
                }).ToList()
            };
            return View(vm);
        }

        [HttpGet("MyBookings")]
        public async Task<IActionResult> MyBookings()
        {
            var userId = _userManager.GetUserId(User)!;
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.UserId == userId);
            if (customer == null)
            {
                return View(new List<Booking>());
            }
            var bookings = await _context.Bookings
                .Include(b => b.Event)
                .Where(b => b.CustomerID == customer.Id)
                .OrderByDescending(b => b.BookingDate)
                .ToListAsync();
            return View(bookings);
        }

        [HttpGet("Qr/{id}")]
        public async Task<IActionResult> Qr(int id, int? et)
        {
            var userId = _userManager.GetUserId(User)!;
            var booking = await _context.Bookings.Include(b => b.Event).FirstOrDefaultAsync(b => b.Id == id);
            if (booking == null) return NotFound();
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.UserId == userId);
            if (customer == null || booking.CustomerID != customer.Id) return Forbid();

            string payload;
            if (et.HasValue)
            {
                var eticket = await _context.ETickets.FirstOrDefaultAsync(x => x.Id == et.Value && x.BookingID == id);
                if (eticket == null) return NotFound();
                payload = eticket.QRCode;
            }
            else
            {
                payload = $"Booking:{booking.Id};Event:{booking.EventID};Title:{booking.Event.Title};Date:{booking.Event.StartDateTime:O};Venue:{booking.Event.VenueID}";
            }

            var generator = new QRCodeGenerator();
            var data = generator.CreateQrCode(payload, QRCodeGenerator.ECCLevel.Q);
            var png = new PngByteQRCode(data).GetGraphic(20);
            return File(png, "image/png", et.HasValue ? $"booking-{id}-et-{et}.png" : $"booking-{id}-qr.png");
        }

        private async Task<Ticket> GetOrCreateDefaultTicket(Event ev)
        {
            var ticket = await _context.Tickets.FirstOrDefaultAsync(t => t.EventID == ev.Id && t.TicketType == "General");
            if (ticket != null)
            {
                if (ticket.Price != ev.TicketPrice)
                {
                    ticket.Price = ev.TicketPrice;
                    await _context.SaveChangesAsync();
                }
                return ticket;
            }
            ticket = new Ticket
            {
                EventID = ev.Id,
                TicketType = "General",
                Price = ev.TicketPrice,
                QuantityAvailable = ev.TotalTickets,
                QuantitySold = ev.TotalTickets - ev.AvailableTickets
            };
            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();
            return ticket;
        }

        private static string GenerateCode(int bookingId, int ticketId, int index)
        {
            return $"B:{bookingId};T:{ticketId};N:{index};TS:{DateTimeOffset.UtcNow.ToUnixTimeSeconds()}";
        }

        private static string RenderQrSvg(string payload)
        {
            var gen = new QRCodeGenerator();
            var data = gen.CreateQrCode(payload, QRCodeGenerator.ECCLevel.Q);
            return new SvgQRCode(data).GetGraphic(2);
        }
    }
}
