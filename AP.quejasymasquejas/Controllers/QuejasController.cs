using AP.quejasymasquejas.Data;
using AP.quejasymasquejas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AP.quejasymasquejas.web.Controllers
{
    public class QuejasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public QuejasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Quejas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Quejas.ToListAsync());
        }

        // GET: Quejas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var queja = await _context.Quejas
                .FirstOrDefaultAsync(m => m.QuejaID == id);
            if (queja == null)
            {
                return NotFound();
            }

            return View(queja);
        }

        // GET: Quejas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Quejas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("QuejaID,Titulo,Descripcion,FechaRegistro,Estado,Prioridad,Categoria")] Queja queja)
        {
            if (ModelState.IsValid)
            {
                _context.Add(queja);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(queja);
        }

        // GET: Quejas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var queja = await _context.Quejas.FindAsync(id);
            if (queja == null)
            {
                return NotFound();
            }
            return View(queja);
        }

        // POST: Quejas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("QuejaID,Titulo,Descripcion,FechaRegistro,Estado,Prioridad,Categoria")] Queja queja)
        {
            if (id != queja.QuejaID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(queja);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuejaExists(queja.QuejaID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(queja);
        }

        // GET: Quejas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var queja = await _context.Quejas
                .FirstOrDefaultAsync(m => m.QuejaID == id);
            if (queja == null)
            {
                return NotFound();
            }

            return View(queja);
        }

        // POST: Quejas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var queja = await _context.Quejas.FindAsync(id);
            if (queja != null)
            {
                _context.Quejas.Remove(queja);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuejaExists(int id)
        {
            return _context.Quejas.Any(e => e.QuejaID == id);
        }
    }
}
