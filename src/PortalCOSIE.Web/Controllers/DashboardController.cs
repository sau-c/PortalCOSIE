using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortalCOSIE.Application.Interfaces;

namespace PortalCOSIE.Web.Controllers
{
    [Authorize(Roles = "Administrador, Personal")]
    public class DashboardController : Controller
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _dashboardService.ObtenerTarjetasContables());
        }

        [HttpGet]
        public async Task<IActionResult> MasReprobadas(int? carreraId)
        {
            return Json(await _dashboardService.ObtenerUnidadesMasReprobadasPorCarrera(carreraId));
        }

        [HttpGet]
        public async Task<IActionResult> SolicitudesPorCarrera(string rango)
        {
            var datos = await _dashboardService.ObtenerSolicitudesPorCarrera(rango);
            return Json(datos);
        }
    }
}
