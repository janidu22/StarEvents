using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StarEvents.DataAccess.Data;
using StarEvents.DataAccess.Models;
using StarEventsWeb.ViewModels;

namespace StarEventsWeb.Controllers
{
    [Authorize(Roles = "Admin,Event Organizer")]
    public class EventsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<EventsController> _logger;

        public EventsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, ILogger<EventsController> logger)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
        }

        // GET: Events
        public async Task<IActionResult> Index()
        {
            var query = _context.Events
                .Include(e => e.Organizer)
                .Include(e => e.Loyality)
                .Include(e => e.Venue)
                .AsQueryable();

            if (User.IsInRole("Event Organizer"))
            {
                var userId = _userManager.GetUserId(User)!;
                var organizer = await _context.EventOrganizers.FirstOrDefaultAsync(o => o.UserId == userId);
                if (organizer != null)
                {
                    query = query.Where(e => e.OrganizerID == organizer.Id);
                }
            }
            return View(await query.ToListAsync());
        }

        // GET: Events/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var ev = await _context.Events
                .Include(e => e.Organizer)
                .Include(e => e.Loyality)
                .Include(e => e.Venue)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ev == null) return NotFound();
            if (User.IsInRole("Event Organizer") && !await OrganizerOwnsEvent(ev)) return Forbid();
            return View(ev);
        }

        // GET: Events/Create
        public async Task<IActionResult> Create()
        {
            var vm = new EventFormViewModel { IsAdmin = User.IsInRole("Admin") };
            await PopulateDropdowns(vm);
            return View(vm);
        }

        // POST: Events/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EventFormViewModel vm)
        {
            vm.IsAdmin = User.IsInRole("Admin");
            if (!ModelState.IsValid || vm.StartDateTime >= vm.EndDateTime)
            {
                if (vm.StartDateTime >= vm.EndDateTime)
                    ModelState.AddModelError(nameof(vm.EndDateTime), "End time must be after start time");
                await PopulateDropdowns(vm);
                return View(vm);
            }

            int organizerId;
            if (User.IsInRole("Event Organizer"))
            {
                var userId = _userManager.GetUserId(User)!;
                var organizer = await _context.EventOrganizers.FirstOrDefaultAsync(o => o.UserId == userId);
                if (organizer == null)
                {
                    var user = await _userManager.FindByIdAsync(userId);
                    organizer = new EventOrganizer
                    {
                        UserId = userId,
                        CompanyName = user?.FullName + " Org" ?? "Organizer Company",
                        ContactPerson = user?.FullName ?? "Organizer",
                        ContactEmail = user?.Email ?? "noreply@example.com",
                        ContactPhone = "N/A",
                        BankAccountDetails = "N/A"
                    };
                    _context.EventOrganizers.Add(organizer);
                    await _context.SaveChangesAsync();
                }
                organizerId = organizer.Id;
            }
            else
            {
                if (vm.OrganizerID == null || vm.OrganizerID <= 0)
                {
                    ModelState.AddModelError(nameof(vm.OrganizerID), "Select an organizer");
                    await PopulateDropdowns(vm);
                    return View(vm);
                }
                organizerId = vm.OrganizerID.Value;
            }

            var ev = new Event
            {
                OrganizerID = organizerId,
                VenueID = vm.VenueID,
                Title = vm.Title,
                Category = vm.Category,
                Description = vm.Description,
                StartDateTime = vm.StartDateTime!.Value,
                EndDateTime = vm.EndDateTime!.Value,
                TicketPrice = vm.TicketPrice,
                TotalTickets = vm.TotalTickets,
                AvailableTickets = vm.AvailableTickets.GetValueOrDefault(vm.TotalTickets),
                IsActive = vm.IsActive,
                LoyalityId = vm.LoyalityId,
                CreatedAt = DateTime.UtcNow
            };

            if (ev.AvailableTickets <= 0 || ev.AvailableTickets > ev.TotalTickets)
                ev.AvailableTickets = ev.TotalTickets;

            _context.Events.Add(ev);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Events/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var e = await _context.Events.FindAsync(id);
            if (e == null) return NotFound();
            if (User.IsInRole("Event Organizer") && !await OrganizerOwnsEvent(e)) return Forbid();
            var vm = new EventFormViewModel
            {
                Id = e.Id,
                OrganizerID = User.IsInRole("Admin") ? e.OrganizerID : null,
                VenueID = e.VenueID,
                Title = e.Title,
                Category = e.Category,
                Description = e.Description,
                StartDateTime = e.StartDateTime,
                EndDateTime = e.EndDateTime,
                TicketPrice = e.TicketPrice,
                TotalTickets = e.TotalTickets,
                AvailableTickets = e.AvailableTickets,
                IsActive = e.IsActive,
                LoyalityId = e.LoyalityId,
                IsAdmin = User.IsInRole("Admin")
            };
            await PopulateDropdowns(vm);
            return View(vm);
        }

        // POST: Events/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EventFormViewModel vm)
        {
            vm.IsAdmin = User.IsInRole("Admin");
            if (id != vm.Id) return NotFound();
            if (!ModelState.IsValid || vm.StartDateTime >= vm.EndDateTime)
            {
                if (vm.StartDateTime >= vm.EndDateTime)
                    ModelState.AddModelError(nameof(vm.EndDateTime), "End time must be after start time");
                await PopulateDropdowns(vm);
                return View(vm);
            }

            var existing = await _context.Events.FirstOrDefaultAsync(e => e.Id == id);
            if (existing == null) return NotFound();

            if (User.IsInRole("Event Organizer"))
            {
                var userId = _userManager.GetUserId(User)!;
                var organizer = await _context.EventOrganizers.FirstOrDefaultAsync(o => o.UserId == userId);
                if (organizer == null) return Forbid();
                existing.OrganizerID = organizer.Id;
            }
            else
            {
                if (vm.OrganizerID == null || vm.OrganizerID <= 0)
                {
                    ModelState.AddModelError(nameof(vm.OrganizerID), "Select an organizer");
                    await PopulateDropdowns(vm);
                    return View(vm);
                }
                existing.OrganizerID = vm.OrganizerID.Value;
            }

            existing.VenueID = vm.VenueID;
            existing.Title = vm.Title;
            existing.Category = vm.Category;
            existing.Description = vm.Description;
            existing.StartDateTime = vm.StartDateTime!.Value;
            existing.EndDateTime = vm.EndDateTime!.Value;
            existing.TicketPrice = vm.TicketPrice;
            existing.TotalTickets = vm.TotalTickets;
            existing.AvailableTickets = vm.AvailableTickets.GetValueOrDefault(vm.TotalTickets);
            if (existing.AvailableTickets <= 0 || existing.AvailableTickets > existing.TotalTickets)
                existing.AvailableTickets = existing.TotalTickets;
            existing.IsActive = vm.IsActive;
            existing.LoyalityId = vm.LoyalityId;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Events/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var ev = await _context.Events
                .Include(e => e.Organizer)
                .Include(e => e.Loyality)
                .Include(e => e.Venue)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ev == null) return NotFound();
            if (User.IsInRole("Event Organizer") && !await OrganizerOwnsEvent(ev)) return Forbid();
            return View(ev);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ev = await _context.Events.FindAsync(id);
            if (ev == null) return RedirectToAction(nameof(Index));
            if (User.IsInRole("Event Organizer") && !await OrganizerOwnsEvent(ev)) return Forbid();
            _context.Events.Remove(ev);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> EventExists(int id) => await _context.Events.AnyAsync(e => e.Id == id);

        private async Task<bool> OrganizerOwnsEvent(Event ev)
        {
            var userId = _userManager.GetUserId(User)!;
            var organizer = await _context.EventOrganizers.FirstOrDefaultAsync(o => o.UserId == userId);
            return organizer != null && ev.OrganizerID == organizer.Id;
        }

        private async Task PopulateDropdowns(EventFormViewModel vm)
        {
            vm.Venues = await _context.Venues
                .OrderBy(v => v.Name)
                .Select(v => new SelectListItem
                {
                    Value = v.Id.ToString(),
                    Text = v.Name + " — " + v.Location + " (Cap: " + v.Capacity + ")"
                }).ToListAsync();

            vm.Loyalities = await _context.LoyaltyPointsHistories
                .OrderBy(l => l.Name)
                .Select(l => new SelectListItem
                {
                    Value = l.Id.ToString(),
                    Text = (string.IsNullOrEmpty(l.Name) ? l.Description : l.Name) + " — " + l.Points + " pts"
                }).ToListAsync();

            if (vm.IsAdmin)
            {
                vm.Organizers = await _context.EventOrganizers
                    .Include(o => o.User)
                    .OrderBy(o => o.CompanyName)
                    .Select(o => new SelectListItem
                    {
                        Value = o.Id.ToString(),
                        Text = o.CompanyName + (o.User != null ? " — " + o.User.FullName : string.Empty)
                    }).ToListAsync();
            }
        }
    }
}
