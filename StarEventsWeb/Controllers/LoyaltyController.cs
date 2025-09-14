using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StarEvents.DataAccess.Data;
using StarEvents.DataAccess.Models;

namespace StarEventsWeb.Controllers
{
    [Authorize(Roles = "Admin")] // Only Admin manages loyalty references
    public class LoyaltyController : Controller
    {
        private readonly ApplicationDbContext _context;
        public LoyaltyController(ApplicationDbContext context) { _context = context; }

        // GET: Loyalty
        public async Task<IActionResult> Index()
        {
            var items = await _context.LoyaltyPointsHistories
                .OrderByDescending(l => l.Id)
                .ToListAsync();
            return View(items);
        }

        // GET: Loyalty/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var item = await _context.LoyaltyPointsHistories.FirstOrDefaultAsync(l => l.Id == id);
            if (item == null) return NotFound();
            return View(item);
        }

        // GET: Loyalty/Create
        public IActionResult Create() => View();

        // POST: Loyalty/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Points,Description")] LoyalityPointHistory loyalty)
        {
            if (ModelState.IsValid)
            {
                _context.Add(loyalty);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(loyalty);
        }

        // GET: Loyalty/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var loyalty = await _context.LoyaltyPointsHistories.FindAsync(id);
            if (loyalty == null) return NotFound();
            return View(loyalty);
        }

        // POST: Loyalty/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Points,Description")] LoyalityPointHistory loyalty)
        {
            if (id != loyalty.Id) return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(loyalty);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await LoyaltyExists(loyalty.Id)) return NotFound(); else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(loyalty);
        }

        // GET: Loyalty/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var item = await _context.LoyaltyPointsHistories.FirstOrDefaultAsync(l => l.Id == id);
            if (item == null) return NotFound();
            return View(item);
        }

        // POST: Loyalty/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _context.LoyaltyPointsHistories.FindAsync(id);
            if (item != null)
            {
                _context.LoyaltyPointsHistories.Remove(item);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> LoyaltyExists(int id) => await _context.LoyaltyPointsHistories.AnyAsync(e => e.Id == id);
    }
}
