using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortalCOSIE.Application.Features.Carreras.Commands.AgregarUnidad;
using PortalCOSIE.Application.Features.Carreras.Commands.Crear;
using PortalCOSIE.Application.Features.Carreras.Commands.Editar;
using PortalCOSIE.Application.Features.Carreras.Commands.EditarUnidad;
using PortalCOSIE.Application.Features.Carreras.Commands.Toggle;
using PortalCOSIE.Application.Features.Carreras.Commands.ToggleUnidad;
using PortalCOSIE.Application.Features.Carreras.Queries.Listar;
using PortalCOSIE.Application.Features.Carreras.Queries.ObtenerDetalle;
using PortalCOSIE.Application.Features.PeriodosConfig.Commands.EditarPeriodoConfig;
using PortalCOSIE.Application.Features.PeriodosConfig.Queries.ObtenerPeriodoConfig;
using PortalCOSIE.Application.Features.Tramites.Queries.ListarEstadosTramite;

namespace PortalCOSIE.Web.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class ConfiguracionController : Controller
    {
        private readonly IMediator _mediator;
        public ConfiguracionController(IMediator mediator)
            => _mediator = mediator;

        [HttpGet]
        public IActionResult Index() => View();

        #region CARRERAS
        [HttpGet]
        public async Task<IActionResult> Carreras()
            => PartialView("_Carreras", await _mediator.Send(new ListarCarrerasQuery(true)));

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Carreras(CrearCarreraCommand command)
        {
            await _mediator.Send(command);
            return Json(new { success = true, message = "Cambios guardados" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarCarrera(int id, string nombre)
        {
            await _mediator.Send(new EditarCarreraCommand(id, nombre));
            return Json(new { success = true, message = "Cambios guardados" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleCarrera(int id)
        {
            await _mediator.Send(new ToggleCarreraCommand(id));
            return Json(new { success = true, message = "Cambios guardados" });
        }

        [HttpGet]
        public async Task<IActionResult> Unidades(int carreraId)
            => View(await _mediator.Send(new ObtenerCarreraDetalleQuery(carreraId)));

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Unidades(AgregarUnidadCommand command)
        {
            await _mediator.Send(command);
            return Json(new { success = true, message = "Cambios guardados" });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarUnidad(EditarUnidadCommand command)
        {
            await _mediator.Send(command);
            return Json(new { success = true, message = "Cambios guardados" });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleUnidad(ToggleUnidadCommand command)
        {
            await _mediator.Send(command);
            return Json(new { success = true, message = "Cambios guardados" });
        }
        #endregion

        [HttpGet]
        public async Task<IActionResult> Estados()
            => PartialView("_EstadoTramite", await _mediator.Send(new ListarEstadoTramiteQuery()));

        [HttpGet]
        public async Task<IActionResult> EstadosDocumento()
            => PartialView("_EstadoDocumento", await _mediator.Send(new ListarEstadoDocumentoQuery()));

        [HttpGet]
        public async Task<IActionResult> Tipo()
            => PartialView("_TipoTramite", await _mediator.Send(new ListarTipoTramiteQuery()));

        [HttpGet]
        public async Task<IActionResult> Periodos()
            => PartialView("_Periodos", await _mediator.Send(new ObtenerPeriodoConfigQuery()));

        [HttpPost]
        public async Task<IActionResult> Periodos(EditarPeriodoConfigCommand command)
        {
            await _mediator.Send(command);
            return Json(new { success = true, message = "Cambios guardados" });
        }
    }
}