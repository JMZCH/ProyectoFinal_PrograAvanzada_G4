using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using AP.quejasymasquejas.Business.Interfaces;
using AP.quejasymasquejas.Models.DTOs;

namespace AP.quejasymasquejas.Web.Controllers
{
    public class QuejasController : Controller
    {
        private readonly IQuejaService _quejaService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<QuejasController> _logger;

        public QuejasController(
            IQuejaService quejaService,
            UserManager<IdentityUser> userManager,
            ILogger<QuejasController> logger)
        {
            _quejaService = quejaService;
            _userManager = userManager;
            _logger = logger;
        }

        /// <summary>
        /// Lista todas las quejas
        /// GET: Quejas
        /// </summary>
        public async Task<IActionResult> Index()
        {
            var quejas = await _quejaService.GetAllQuejasAsync();
            return View(quejas);
        }

        /// <summary>
        /// Muestra detalles de una queja
        /// GET: Quejas/Details/5
        /// </summary>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var queja = await _quejaService.GetQuejaByIdAsync(id.Value);
            if (queja == null) return NotFound();

            return View(queja);
        }

        /// <summary>
        /// Formulario para crear queja
        /// GET: Quejas/Create
        /// </summary>
        [Authorize]
        public IActionResult Create()
        {
            return View(new QuejaCreateDto());
        }

        /// <summary>
        /// Procesa creación de queja
        /// POST: Quejas/Create
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create(QuejaCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            try
            {
                var userId = _userManager.GetUserId(User);
                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized();
                }

                await _quejaService.CreateQuejaAsync(dto, userId);
                TempData["Success"] = "¡Queja creada exitosamente!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear queja");
                ModelState.AddModelError("", "Error al crear la queja. Intente nuevamente.");
                return View(dto);
            }
        }

        /// <summary>
        /// Formulario para editar queja
        /// GET: Quejas/Edit/5
        /// </summary>
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var userId = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            // Verificar propiedad
            if (!await _quejaService.IsOwnerAsync(id.Value, userId))
            {
                return Forbid();
            }

            var queja = await _quejaService.GetQuejaForEditAsync(id.Value);
            if (queja == null) return NotFound();

            return View(queja);
        }

        /// <summary>
        /// Procesa edición de queja
        /// POST: Quejas/Edit/5
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, QuejaEditDto dto)
        {
            if (id != dto.QuejaId) return NotFound();

            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            try
            {
                var userId = _userManager.GetUserId(User);
                if (string.IsNullOrEmpty(userId)) return Unauthorized();

                var result = await _quejaService.UpdateQuejaAsync(dto, userId);
                if (!result)
                {
                    return Forbid();
                }

                TempData["Success"] = "¡Queja actualizada exitosamente!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar queja {QuejaId}", id);
                ModelState.AddModelError("", "Error al actualizar la queja. Intente nuevamente.");
                return View(dto);
            }
        }

        /// <summary>
        /// Confirmación para eliminar queja
        /// GET: Quejas/Delete/5
        /// </summary>
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var userId = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            // Verificar propiedad
            if (!await _quejaService.IsOwnerAsync(id.Value, userId))
            {
                return Forbid();
            }

            var queja = await _quejaService.GetQuejaByIdAsync(id.Value);
            if (queja == null) return NotFound();

            return View(queja);
        }

        /// <summary>
        /// Procesa eliminación de queja
        /// POST: Quejas/Delete/5
        /// </summary>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var userId = _userManager.GetUserId(User);
                if (string.IsNullOrEmpty(userId)) return Unauthorized();

                var result = await _quejaService.DeleteQuejaAsync(id, userId);
                if (!result)
                {
                    return Forbid();
                }

                TempData["Success"] = "¡Queja eliminada exitosamente!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar queja {QuejaId}", id);
                TempData["Error"] = "Error al eliminar la queja.";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
