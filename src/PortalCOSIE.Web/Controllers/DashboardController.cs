using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PortalCOSIE.Application.Interfaces;
using PortalCOSIE.Application.Services;

namespace PortalCOSIE.Web.Controllers
{
    [Authorize(Roles = "Administrador, Personal")]
    public class DashboardController : Controller
    {
        private readonly IDashboardService _dashboardService;
        private readonly IPeriodosService _periodoService;
        private readonly ICarreraService _carreraService;

        public DashboardController(
            IDashboardService dashboardService,
            IPeriodosService periodoService,
            ICarreraService carreraService)
        {
            _dashboardService = dashboardService;
            _periodoService = periodoService;
            _carreraService = carreraService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ViewBag.Periodos = new SelectList(await _periodoService.ListarPeriodos(), "Periodo");
            ViewBag.Carreras = new SelectList(await _carreraService.ListarActivasAsync(), "Id", "Nombre");
            return View(await _dashboardService.ObtenerTarjetasContables());
        }

        [HttpGet]
        public async Task<IActionResult> MasReprobadas(int? carreraId, string periodo)
        {
            return Json(await _dashboardService.ObtenerUnidadesMasReprobadasPorCarrera(carreraId, periodo));
        }

        [HttpGet]
        public async Task<IActionResult> SolicitudesPorCarrera(string periodo)
        {
            var datos = await _dashboardService.ObtenerSolicitudesPorCarrera(periodo);
            return Json(datos);
        }
    }
}
