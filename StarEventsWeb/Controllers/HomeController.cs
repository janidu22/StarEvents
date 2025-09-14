using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StarEvents.DataAccess.Data;
using StarEvents.DataAccess.Models;
using StarEventsWeb.ViewModels;

namespace StarEventsWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Events([FromQuery] EventSearchViewModel vm)
        {
            // Build base query (public browsing: only active and upcoming)
            var query = _context.Events
                .Include(e => e.Venue)
                .Where(e => e.IsActive && e.EndDateTime >= DateTime.UtcNow)
                .AsQueryable();

            // Distinct categories and locations for filters
            vm.Categories = await _context.Events
                .Select(e => e.Category)
                .Where(s => s != null && s != "")
                .Distinct()
                .OrderBy(s => s)
                .Select(s => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = s, Text = s })
                .ToListAsync();

            vm.Locations = await _context.Venues
                .Select(v => v.Location)
                .Where(s => s != null && s != "")
                .Distinct()
                .OrderBy(s => s)
                .Select(s => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = s, Text = s })
                .ToListAsync();

            // Apply filters
            if (!string.IsNullOrWhiteSpace(vm.Query))
            {
                var q = vm.Query.ToLower();
                query = query.Where(e => e.Title.ToLower().Contains(q) || e.Description.ToLower().Contains(q));
            }
            if (!string.IsNullOrWhiteSpace(vm.Category))
            {
                query = query.Where(e => e.Category == vm.Category);
            }
            if (!string.IsNullOrWhiteSpace(vm.Location))
            {
                query = query.Where(e => e.Venue.Location == vm.Location);
            }
            if (vm.StartFrom.HasValue)
            {
                query = query.Where(e => e.StartDateTime >= vm.StartFrom.Value);
            }
            if (vm.EndTo.HasValue)
            {
                query = query.Where(e => e.EndDateTime <= vm.EndTo.Value);
            }

            // Execute
            vm.Results = await query
                .OrderBy(e => e.StartDateTime)
                .Select(e => new EventListItemViewModel
                {
                    Id = e.Id,
                    Title = e.Title,
                    Category = e.Category,
                    StartDateTime = e.StartDateTime,
                    EndDateTime = e.EndDateTime,
                    TicketPrice = e.TicketPrice,
                    VenueName = e.Venue.Name,
                    Location = e.Venue.Location
                })
                .ToListAsync();

            return View(vm);
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
