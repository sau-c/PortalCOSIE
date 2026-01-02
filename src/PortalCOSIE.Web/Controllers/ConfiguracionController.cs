using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortalCOSIE.Application.Features.Carreras.Commands.Crear;
using PortalCOSIE.Application.Features.Carreras.Commands.Editar;
using PortalCOSIE.Application.Features.Carreras.Commands.Toggle;
using PortalCOSIE.Application.Features.Carreras.Queries.Listar;
using PortalCOSIE.Application.Features.Carreras.Queries.ObtenerDetalle;
using PortalCOSIE.Application.Features.Carreras.Commands.AgregarUnidad;
using PortalCOSIE.Application.Features.Carreras.Commands.EditarUnidad;
using PortalCOSIE.Application.Features.Carreras.Commands.ToggleUnidad;
using PortalCOSIE.Application.Features.PeriodosConfig.Commands.EditarPeriodoConfig;
using PortalCOSIE.Application.Features.PeriodosConfig.Queries.ObtenerPeriodoConfig;
using PortalCOSIE.Domain.Entities.Documentos;
using PortalCOSIE.Domain.Entities.Tramites;
using PortalCOSIE.Application.Services;

namespace PortalCOSIE.Web.Controllers
{
    public class ConfiguracionController : Controller
    {
        private readonly ICatalogoService<EstadoTramite, int> _estadoTramiteService;
        private readonly ICatalogoService<EstadoDocumento, int> _estadoDocumentoService;
        private readonly ICatalogoService<TipoTramite, int> _tipoTramiteService;
        private readonly IMediator _mediator;

        public ConfiguracionController(
            ICatalogoService<EstadoTramite, int> estadoTramiteService,
            ICatalogoService<EstadoDocumento, int> estadoDocumentoService,
            ICatalogoService<TipoTramite, int> tipoTramiteService,
            IMediator mediator
            )
        {
            _estadoTramiteService = estadoTramiteService;
            _estadoDocumentoService = estadoDocumentoService;
            _tipoTramiteService = tipoTramiteService;
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize(Roles = "Administrador")]
        public IActionResult Index() => View();

        #region CARRERAS
        [HttpGet]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Carreras()
        {
            return PartialView("_Carreras", await _mediator.Send(new ListarCarrerasQuery(true)));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Carreras(string nombre)
        {
            await _mediator.Send(new CrearCarreraCommand(nombre));
            return Json(new { success = true, message = "Cambios guardados" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> EditarCarrera(int id, string nombre)
        {
            await _mediator.Send(new EditarCarreraCommand(id, nombre));
            return Json(new { success = true, message = "Cambios guardados" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> ToggleCarrera(int id)
        {
            await _mediator.Send(new ToggleCarreraCommand(id));
            return Json(new { success = true, message = "Cambios guardados" });
        }

        [HttpGet]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Unidades(int carreraId)
        {
            return View(await _mediator.Send(new ObtenerCarreraDetalleQuery(carreraId)));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Unidades(AgregarUnidadCommand command)
        {
            await _mediator.Send(command);
            return Json(new { success = true, message = "Cambios guardados" });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> EditarUnidad(EditarUnidadCommand command)
        {
            await _mediator.Send(command);
            return Json(new { success = true, message = "Cambios guardados" });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> ToggleUnidad(ToggleUnidadCommand command)
        {
            //await _carreraService.ToggleUnidad(carreraId, unidadId);
            await _mediator.Send(command);
            return Json(new { success = true, message = "Cambios guardados" });
        }
        #endregion

        #region ESTADO_TRAMITE
        [HttpGet]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Estados()
        {
            return PartialView("_EstadoTramite", await _estadoTramiteService.ListarAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> ToggleEstado([FromForm] int id)
        {
            await _estadoTramiteService.ToggleAsync(id);
            return Json(new { success = true, message = "Cambios guardados" });
        }
        #endregion

        #region ESTADO_DOCUMENTO
        [HttpGet]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> EstadosDocumento()
        {
            return PartialView("_EstadoDocumento", await _estadoDocumentoService.ListarAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> ToggleEstadoDocumento([FromForm] int id)
        {
            await _estadoDocumentoService.ToggleAsync(id);
            return Json(new { success = true, message = "Cambios guardados" });
        }
        #endregion

        #region TIPO_TRAMITE
        [HttpGet]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Tipo()
        {
            return PartialView("_TipoTramite", await _tipoTramiteService.ListarAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> ToggleTipo([FromForm] int id)
        {
            await _tipoTramiteService.ToggleAsync(id);
            return Json(new { success = true, message = "Cambios guardados" });
        }

        #endregion

        [HttpGet]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Periodos()
        {
            return PartialView("_Periodos", await _mediator.Send(new ObtenerPeriodoConfigQuery()));
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Periodos(EditarPeriodoConfigCommand command)
        {
            await _mediator.Send(command);
            return Json(new { success = true, message = "Cambios guardados" });
        }
    }
}