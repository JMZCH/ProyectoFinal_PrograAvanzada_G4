using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using AP.quejasymasquejas.Business.Interfaces;
using AP.quejasymasquejas.Models.ViewModels;

namespace AP.quejasymasquejas.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IQuejaService _quejaService;

        public HomeController(ILogger<HomeController> logger, IQuejaService quejaService)
        {
            _logger = logger;
            _quejaService = quejaService;
        }

        /// <summary>
        /// Página principal con dashboard de quejas
        /// </summary>
        public async Task<IActionResult> Index()
        {
            try
            {
                var dashboard = await _quejaService.GetDashboardDataAsync(5);
                return View(dashboard);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al cargar el dashboard");
                return View(new DashboardViewModel());
            }
        }

        /// <summary>
        /// Página de privacidad
        /// </summary>
        public IActionResult Privacy()
        {
            return View();
        }

        /// <summary>
        /// Página de error
        /// </summary>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel 
            { 
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier 
            });
        }
    }
}
