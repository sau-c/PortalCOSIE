using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortalCOSIE.Application.DTO.Periodo;
using PortalCOSIE.Application.Interfaces;
using PortalCOSIE.Domain.Entities.Documentos;
using PortalCOSIE.Domain.Entities.Tramites;
using PortalCOSIE.Domain.Enums;

namespace PortalCOSIE.Web.Controllers
{
    public class ConfiguracionController : Controller
    {
        private readonly IPeriodosService _periodoService;
        private readonly ICarreraService _carreraService;
        private readonly ICatalogoService<EstadoTramite> _estadoTramiteService;
        private readonly ICatalogoService<EstadoDocumento> _estadoDocumentoService;
        private readonly ICatalogoService<TipoTramite> _tipoTramiteService;

        public ConfiguracionController(
            IPeriodosService periodoService,
            ICarreraService carreraService,
            ICatalogoService<EstadoTramite> estadoTramiteService,
            ICatalogoService<EstadoDocumento> estadoDocumentoService,
            ICatalogoService<TipoTramite> tipoTramiteService
            )
        {
            _periodoService = periodoService;
            _carreraService = carreraService;
            _estadoTramiteService = estadoTramiteService;
            _estadoDocumentoService = estadoDocumentoService;
            _tipoTramiteService = tipoTramiteService;
        }

        [HttpGet]
        [Authorize(Roles = "Administrador")]
        public IActionResult Index() => View();

        #region CARRERAS
        [HttpGet]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Carreras()
        {
            return PartialView("_Carreras", await _carreraService.ListarAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Carreras([FromForm] string nombre)
        {
            await _carreraService.CrearCarreraAsync(nombre);
            return Json(new { success = true, message = "Cambios guardados" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> EditarCarrera([FromForm] int id, [FromForm] string nombre)
        {
            await _carreraService.EditarCarreraAsync(id, nombre);
            return Json(new { success = true, message = "Cambios guardados" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> ToggleCarrera([FromForm] int id)
        {
            await _carreraService.ToggleCarrrera(id);
            return Json(new { success = true, message = "Cambios guardados" });
        }

        [HttpGet]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Unidades(string id)
        {
            return View(await _carreraService.ListarConUnidadesAsync(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Unidades([FromForm] string nombre, [FromForm] int carreraId, [FromForm] Semestre semestre)
        {
            await _carreraService.CrearUnidadAsync(nombre, carreraId, semestre);
            return Json(new { success = true, message = "Cambios guardados" });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> EditarUnidad([FromForm] int id, [FromForm] string nombre, [FromForm] string carreraNombre, [FromForm] Semestre semestre)
        {
            await _carreraService.EditarUnidadAsync(carreraNombre, id, nombre, semestre);
            return Json(new { success = true, message = "Cambios guardados" });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> ToggleUnidad([FromForm] string carreraNombre, [FromForm] int id)
        {
            await _carreraService.ToggleUnidad(carreraNombre, id);
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
            return PartialView("_Periodos", await _periodoService.BuscarPeriodoConfig());
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Periodos(PeriodoConfigDTO dto)
        {
            await _periodoService.EditarPeriodoConfig(dto);
            return Json(new { success = true, message = "Cambios guardados" });
        }
    }
}