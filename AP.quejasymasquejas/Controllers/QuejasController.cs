using AP.quejasymasquejas.Data;
using AP.quejasymasquejas.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AP.quejasymasquejas.Controllers
{
    public class QuejasController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public QuejasController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Quejas
        public async Task<IActionResult> Index()
        {
            var quejas = await _context.Quejas
                .Include(q => q.Usuario)
                .OrderByDescending(q => q.FechaRegistro)
                .ToListAsync();
            return View(quejas);
        }

        // GET: Quejas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var queja = await _context.Quejas
                .Include(q => q.Usuario)
                .FirstOrDefaultAsync(m => m.QuejaID == id);

            if (queja == null)
                return NotFound();

            return View(queja);
        }

        // GET: Quejas/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Quejas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Titulo,Descripcion,Prioridad,Categoria")] Queja queja)
        {
            ModelState.Remove("UsuarioId");
            ModelState.Remove("Usuario");

            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                queja.UsuarioId = user?.Id;
                queja.FechaRegistro = DateTime.Now;
                queja.Estado = "Pendiente";

                _context.Add(queja);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(queja);
        }

        // GET: Quejas/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var queja = await _context.Quejas.FindAsync(id);
            if (queja == null)
                return NotFound();

            var userId = _userManager.GetUserId(User);
            if (queja.UsuarioId != userId)
                return Forbid();

            return View(queja);
        }

        // POST: Quejas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("QuejaID,Titulo,Descripcion,Estado,Prioridad,Categoria")] Queja queja)
        {
            if (id != queja.QuejaID)
                return NotFound();

            var quejaOriginal = await _context.Quejas.AsNoTracking().FirstOrDefaultAsync(q => q.QuejaID == id);
            if (quejaOriginal == null)
                return NotFound();

            var userId = _userManager.GetUserId(User);
            if (quejaOriginal.UsuarioId != userId)
                return Forbid();

            ModelState.Remove("UsuarioId");
            ModelState.Remove("Usuario");

            if (ModelState.IsValid)
            {
                try
                {
                    queja.UsuarioId = quejaOriginal.UsuarioId;
                    queja.FechaRegistro = quejaOriginal.FechaRegistro;

                    _context.Update(queja);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuejaExists(queja.QuejaID))
                        return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(queja);
        }

        // GET: Quejas/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var queja = await _context.Quejas
                .Include(q => q.Usuario)
                .FirstOrDefaultAsync(m => m.QuejaID == id);

            if (queja == null)
                return NotFound();

            var userId = _userManager.GetUserId(User);
            if (queja.UsuarioId != userId)
                return Forbid();

            return View(queja);
        }

        // POST: Quejas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var queja = await _context.Quejas.FindAsync(id);
            if (queja == null)
                return NotFound();

            var userId = _userManager.GetUserId(User);
            if (queja.UsuarioId != userId)
                return Forbid();

            _context.Quejas.Remove(queja);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuejaExists(int id)
        {
            return _context.Quejas.Any(e => e.QuejaID == id);
        }
    }
}
