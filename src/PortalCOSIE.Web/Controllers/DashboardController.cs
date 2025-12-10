using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PortalCOSIE.Application.Interfaces;

namespace PortalCOSIE.Web.Controllers
{
    [Authorize(Roles = "Administrador, Personal")]
    public class DashboardController : Controller
    {
        private readonly IDashboardQueryService _dashboardService;
        private readonly IPeriodosService _periodoService;
        private readonly ICarreraService _carreraService;

        public DashboardController(
            IDashboardQueryService dashboardService,
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
            var periodos = await _periodoService.ListarPeriodos();
            ViewBag.Periodos = periodos
                .Select(p => new SelectListItem
                {
                    Text = p,
                    Value = p
                })
                .ToList();
            ViewBag.Carreras = new SelectList(await _carreraService.ListarActivasAsync(), "Id", "Nombre");
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> SolicitudesPorCarrera(string periodo)
        {
            var datos = await _dashboardService.ObtenerSolicitudesPorCarrera(periodo);
            return Json(datos);
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerEstadoTramitesCTCE(string periodo)
        {
            var datos = await _dashboardService.ObtenerEstadoTramitesCTCE(periodo);
            return Json(datos);
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerEstadoDocumentosCTCE(string periodo)
        {
            var datos = await _dashboardService.ObtenerEstadoDocumentosCTCE(periodo);
            return Json(datos);
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerRolesAlumnos()
        {
            var datos = await _dashboardService.ObtenerRolesAlumnos();
            return Json(datos);
        }
        [HttpGet]
        public async Task<IActionResult> MasReprobadas(int? carreraId, string periodo)
        {
            return Json(await _dashboardService.ObtenerUnidadesMasReprobadasPorCarrera(carreraId, periodo));
        }

    }
}
