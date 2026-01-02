using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PortalCOSIE.Application.Features.Carreras.Queries.Listar;
using PortalCOSIE.Application.Features.PeriodosConfig.Queries.ListarPeriodos;
using PortalCOSIE.Application.Features.Dashboard.Queries.ObtenerMasReprobadasCTCE;
using PortalCOSIE.Application.Features.Dashboard.Queries.ObtenerRolesAlumnos;
using PortalCOSIE.Application.Features.Dashboard.Queries.ObtenerEstadoDocumentosCTCE;
using PortalCOSIE.Application.Features.Dashboard.Queries.ObtenerSolicitudesPorCarrera;
using PortalCOSIE.Application.Features.Dashboard.Queries.ObtenerEstadoTramitesCTCE;

namespace PortalCOSIE.Web.Controllers
{
    [Authorize(Roles = "Administrador, Personal")]
    [Produces("application/json")]
    public class DashboardController : Controller
    {
        private readonly IMediator _mediator;
        public DashboardController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var periodos = await _mediator.Send(new ListarPeriodosQuery());
            ViewBag.Periodos = periodos
                .Select(p => new SelectListItem
                {
                    Text = p,
                    Value = p
                })
                .ToList();
            ViewBag.Carreras = new SelectList(await _mediator.Send(new ListarCarrerasQuery()), "Id", "Nombre");

            return View();
        }
        
        [HttpGet]
        public async Task<IActionResult> SolicitudesPorCarrera(ObtenerSolicitudesPorCarreraQuery query)
            => Json(await _mediator.Send(query));
        
        [HttpGet]
        public async Task<IActionResult> ObtenerEstadoTramitesCTCE(ObtenerEstadoTramitesCTCEQuery query)
            => Json(await _mediator.Send(query));
        
        [HttpGet]
        public async Task<IActionResult> ObtenerEstadoDocumentosCTCE(ObtenerEstadoDocumentosCTCEQuery query)
            => Json(await _mediator.Send(query));
        
        [HttpGet]
        public async Task<IActionResult> ObtenerRolesAlumnos()
            => Json(await _mediator.Send(new ObtenerRolesAlumnosQuery()));
        
        [HttpGet]
        public async Task<IActionResult> MasReprobadas(ObtenerMasReprobadasQuery query)
            => Json(await _mediator.Send(query));
    }
}
