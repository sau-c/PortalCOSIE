using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortalCOSIE.Application.Interfaces;
using PortalCOSIE.Domain.Enums;

namespace PortalCOSIE.Web.Controllers
{
    public class ConfiguracionController : Controller
    {
        private readonly ICatalogoService _catalogoService;
        private readonly ICarreraService _carreraService;
        private readonly IEstadoTramiteService _estadoTramiteService;
        private readonly ITipoTramiteService _tipoTramiteService;
        private readonly IEstadoDocumentoService _estadoDocumentoService;

        public ConfiguracionController(
            ICatalogoService tramiteService,
            ICarreraService catalogoService,
            IEstadoTramiteService estadoTramiteService,
            ITipoTramiteService tipoTramiteService,
            IEstadoDocumentoService estadoDocumentoService
            )
        {
            _catalogoService = tramiteService;
            _carreraService = catalogoService;
            _estadoTramiteService = estadoTramiteService;
            _tipoTramiteService = tipoTramiteService;
            _estadoDocumentoService = estadoDocumentoService;
        }

        [HttpGet]
        [Authorize(Roles = "Administrador, Personal")]
        public IActionResult Index() => View();

        #region CARRERAS
        [HttpGet]
        [Authorize(Roles = "Administrador, Personal")]
        public async Task<IActionResult> Carreras()
        {
            return PartialView("_Carreras", await _carreraService.ListarAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador, Personal")]
        public async Task<IActionResult> Carreras([FromForm] string nombre)
        {
            await _carreraService.CrearCarreraAsync(nombre);
            return Json(new { success = true, message = "Cambios guardados" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador, Personal")]
        public async Task<IActionResult> EditarCarrera([FromForm] int id, [FromForm] string nombre)
        {
            await _carreraService.EditarCarreraAsync(id, nombre);
            return Json(new { success = true, message = "Cambios guardados" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador, Personal")]
        public async Task<IActionResult> ToggleCarrera([FromForm] int id)
        {
            await _carreraService.ToggleCarrrera(id);
            return Json(new { success = true, message = "Cambios guardados" });
        }

        [HttpGet]
        [Authorize(Roles = "Administrador, Personal")]
        public async Task<IActionResult> Unidades(string id)
        {
            return View(await _carreraService.ListarConUnidadesAsync(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador, Personal")]
        public async Task<IActionResult> Unidades([FromForm] string nombre, [FromForm] int carreraId, [FromForm] Semestre semestre)
        {
            await _carreraService.CrearUnidadAsync(nombre, carreraId, semestre);
            return Json(new { success = true, message = "Cambios guardados" });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador, Personal")]
        public async Task<IActionResult> EditarUnidad([FromForm] int id, [FromForm] string nombre, [FromForm] string carreraNombre, [FromForm] Semestre semestre)
        {
            await _carreraService.EditarUnidadAsync(carreraNombre, id, nombre, semestre);
            return Json(new { success = true, message = "Cambios guardados" });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador, Personal")]
        public async Task<IActionResult> ToggleUnidad([FromForm] string carreraNombre, [FromForm] int id)
        {
            await _carreraService.ToggleUnidad(carreraNombre, id);
            return Json(new { success = true, message = "Cambios guardados" });
        }
        #endregion

        #region ESTADO_TRAMITE
        [HttpGet]
        [Authorize(Roles = "Administrador, Personal")]
        public async Task<IActionResult> Estados()
        {
            return PartialView("_EstadoTramite", await _estadoTramiteService.ListarEstadoTramite());
        }

        [HttpPost]
        [Authorize(Roles = "Administrador, Personal")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Estados([FromForm] string nombre)
        {
            await _estadoTramiteService.CrearEstadoTramite(nombre);
            return Json(new { success = true, message = "Cambios guardados" });
        }

        [HttpPost]
        [Authorize(Roles = "Administrador, Personal")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarEstadoTramite([FromForm] int id, [FromForm] string nombre)
        {
            await _estadoTramiteService.EditarEstadoTramite(id, nombre);
            return Json(new { success = true, message = "Cambios guardados" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador, Personal")]
        public async Task<IActionResult> ToggleEstado([FromForm] int id)
        {
            await _estadoTramiteService.ToggleEstadoTramite(id);
            return Json(new { success = true, message = "Cambios guardados" });
        }
        #endregion

        #region ESTADO_DOCUMENTO
        [HttpGet]
        [Authorize(Roles = "Administrador, Personal")]
        public async Task<IActionResult> EstadosDocumento()
        {
            return PartialView("_EstadoDocumento", await _estadoDocumentoService.ListarEstadosDocumento());
        }

        [HttpPost]
        [Authorize(Roles = "Administrador, Personal")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EstadosDocumento([FromForm] string nombre)
        {
            await _estadoDocumentoService.CrearEstadoDocumento(nombre);
            return Json(new { success = true, message = "Cambios guardados" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador, Personal")]
        public async Task<IActionResult> EditarEstadoDocumento([FromForm] int id, [FromForm] string nombre)
        {
            await _estadoDocumentoService.EditarEstadoDocumento(id, nombre);
            return Json(new { success = true, message = "Cambios guardados" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador, Personal")]
        public async Task<IActionResult> ToggleEstadoDocumento([FromForm] int id)
        {
            await _estadoDocumentoService.ToggleEstadoDocumento(id);
            return Json(new { success = true, message = "Cambios guardados" });
        }
        #endregion

        #region TIPO_TRAMITE
        [HttpGet]
        [Authorize(Roles = "Administrador, Personal")]
        public async Task<IActionResult> Tipo()
        {
            return PartialView("_TipoTramite", await _tipoTramiteService.ListarTipoTramite());
        }

        [HttpPost]
        [Authorize(Roles = "Administrador, Personal")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Tipo([FromForm] string nombre)
        {
            await _tipoTramiteService.CrearTipoTramite(nombre);
            return Json(new { success = true, message = "Cambios guardados" });
        }

        [HttpPost]
        [Authorize(Roles = "Administrador, Personal")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarTipo([FromForm] int id, [FromForm] string nombre)
        {
            await _tipoTramiteService.EditarTipoTramite(id, nombre);
            return Json(new { success = true, message = "Cambios guardados" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador, Personal")]
        public async Task<IActionResult> ToggleTipo([FromForm] int id)
        {
            await _tipoTramiteService.ToggleTipoTramite(id);
            return Json(new { success = true, message = "Cambios guardados" });
        }

        #endregion

        [HttpGet]
        [Authorize(Roles = "Administrador, Personal")]
        public async Task<IActionResult> Periodos()
        {
            return PartialView("_Periodos", await _catalogoService.BuscarPeriodoConfig());
        }

        [HttpPost]
        [Authorize(Roles = "Administrador, Personal")]
        public async Task<IActionResult> Periodos(int anioInicio, int periodoInicio, int anioFin, int periodoFin)
        {
            await _catalogoService.EditarPeriodoConfig(anioInicio, periodoInicio, anioFin, periodoFin);
            return Json(new { success = true, message = "Cambios guardados" });
        }
    }
}