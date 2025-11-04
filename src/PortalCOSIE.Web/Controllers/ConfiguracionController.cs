using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortalCOSIE.Application.Interfaces;
using PortalCOSIE.Domain;
using PortalCOSIE.Domain.Enums;

namespace PortalCOSIE.Web.Controllers
{
    [Authorize]
    public class ConfiguracionController : Controller
    {
        private readonly ICatalogoService _catalogoService;
        private readonly ICarreraService _carreraService;

        public ConfiguracionController(
            ICarreraService catalogoService,
            ICatalogoService tramiteService
            )
        {
            _carreraService = catalogoService;
            _catalogoService = tramiteService;
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
        public async Task<IActionResult> Carreras(string nombre)
        {
            try
            {
                await _carreraService.CrearCarreraAsync(nombre);
                return RedirectToAction(nameof(Index));
            }
            catch (DomainException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador, Personal")]
        public async Task<IActionResult> EditarCarrera(int id, string Nombre)
        {
            await _carreraService.EditarCarreraAsync(id, Nombre);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador, Personal")]
        public async Task<IActionResult> ToggleCarrera(int id)
        {
            await _carreraService.ToggleCarrrera(id);
            return RedirectToAction(nameof(Index));
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
        public async Task<IActionResult> Unidades(string nombre, int carreraId, Semestre semestre)
        {
            await _carreraService.CrearUnidadAsync(nombre, carreraId, semestre);
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador, Personal")]
        public async Task<IActionResult> EditarUnidad(string carreraNombre, int id, string nombre, Semestre semestre)
        {
            await _carreraService.EditarUnidadAsync(carreraNombre, id, nombre, semestre);
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador, Personal")]
        public async Task<IActionResult> ToggleUnidad(string carreraNombre, int id)
        {
            await _carreraService.ToggleUnidad(carreraNombre, id);
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region ESTADO_TRAMITE
        [HttpGet]
        [Authorize(Roles = "Administrador, Personal")]
        public async Task<IActionResult> Estados()
        {
            return PartialView("_EstadoTramite", await _catalogoService.ListarEstadoTramite());
        }

        [HttpPost]
        [Authorize(Roles = "Administrador, Personal")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Estados(string nombre)
        {
            await _catalogoService.CrearEstadoTramite(nombre);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [Authorize(Roles = "Administrador, Personal")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarEstadoTramite(int id, string nombre)
        {
            await _catalogoService.EditarEstadoTramite(id, nombre);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador, Personal")]
        public async Task<IActionResult> ToggleEstado(int id)
        {
            await _catalogoService.ToggleEstadoTramite(id);
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region ESTADO_DOCUMENTO
        [HttpGet]
        [Authorize(Roles = "Administrador, Personal")]
        public async Task<IActionResult> EstadosDocumento()
        {
            return PartialView("_EstadoDocumento", await _catalogoService.ListarEstadosDocumento());
        }

        [HttpPost]
        [Authorize(Roles = "Administrador, Personal")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EstadosDocumento(string nombre)
        {
            await _catalogoService.CrearEstadoDocumento(nombre);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador, Personal")]
        public async Task<IActionResult> EditarEstadoDocumento(int id, string nombre)
        {
            await _catalogoService.EditarEstadoDocumento(id, nombre);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador, Personal")]
        public async Task<IActionResult> ToggleEstadoDocumento(int id)
        {
            await _catalogoService.ToggleEstadoDocumento(id);
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region TIPO_TRAMITE
        [HttpGet]
        [Authorize(Roles = "Administrador, Personal")]
        public async Task<IActionResult> Tipo()
        {
            return PartialView("_TipoTramite", await _catalogoService.ListarTipoTramite());
        }

        [HttpPost]
        [Authorize(Roles = "Administrador, Personal")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Tipo(string nombre)
        {
            await _catalogoService.CrearTipoTramite(nombre);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [Authorize(Roles = "Administrador, Personal")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarTipo(int id, string nombre)
        {
            await _catalogoService.EditarTipoTramite(id, nombre);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador, Personal")]
        public async Task<IActionResult> ToggleTipo(int id)
        {
            await _catalogoService.ToggleTipoTramite(id);
            return RedirectToAction(nameof(Index));
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
            return RedirectToAction(nameof(Index));
        }
    }
}