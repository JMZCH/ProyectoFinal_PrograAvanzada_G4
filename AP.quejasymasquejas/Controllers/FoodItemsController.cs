using AP.quejasymasquejas.Data;
using AP.quejasymasquejas.Models;
using AP.quejasymasquejas.web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AP.quejasymasquejas.web.Controllers
{
    public class FoodItemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FoodItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /FoodItems
        public async Task<IActionResult> Index(string? q)
        {
            var query = _context.FoodItems
                .Include(f => f.Role)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(q))
            {
                query = query.Where(f =>
                    f.Name.Contains(q) ||
                    (f.Category ?? "").Contains(q) ||
                    (f.Brand ?? "").Contains(q) ||
                    (f.Barcode ?? "").Contains(q));
            }

            ViewData["q"] = q;
            var list = await query
                .OrderByDescending(f => f.DateAdded)
                .ThenBy(f => f.Name)
                .ToListAsync();

            return View(list);
        }

        // GET: /FoodItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null) return NotFound();

            var item = await _context.FoodItems
                .Include(f => f.Role)
                .FirstOrDefaultAsync(m => m.FoodItemID == id);

            if (item is null) return NotFound();
            return View(item);
        }

        // GET: /FoodItems/Create
        public IActionResult Create()
        {
            PopulateRolesDropDown();
            return View(new FoodItem { IsActive = true, DateAdded = DateTime.UtcNow });
        }

        // POST: /FoodItems/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FoodItem model)
        {
            if (ModelState.IsValid)
            {
                if (model.DateAdded == null) model.DateAdded = DateTime.UtcNow;
                _context.FoodItems.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            PopulateRolesDropDown(model.RoleId);
            return View(model);
        }

        // GET: /FoodItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return NotFound();
            var item = await _context.FoodItems.FindAsync(id);
            if (item is null) return NotFound();

            PopulateRolesDropDown(item.RoleId);
            return View(item);
        }

        // POST: /FoodItems/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, FoodItem model)
        {
            if (id != model.FoodItemID) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(model);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _context.FoodItems.AnyAsync(e => e.FoodItemID == id))
                        return NotFound();
                    throw;
                }
            }
            PopulateRolesDropDown(model.RoleId);
            return View(model);
        }

        // GET: /FoodItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return NotFound();
            var item = await _context.FoodItems
                .Include(f => f.Role)
                .FirstOrDefaultAsync(m => m.FoodItemID == id);
            if (item is null) return NotFound();

            return View(item);
        }

        // POST: /FoodItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _context.FoodItems.FindAsync(id);
            if (item != null)
            {
                _context.FoodItems.Remove(item);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private void PopulateRolesDropDown(int? selectedRoleId = null)
        {
            var roles = _context.Roles
                .OrderBy(r => r.RoleName)
                .Select(r => new { r.RoleId, r.RoleName })
                .ToList();

            ViewData["RoleId"] = new SelectList(roles, "RoleId", "RoleName", selectedRoleId);
        }
    }
}
