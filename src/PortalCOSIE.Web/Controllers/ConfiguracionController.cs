using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortalCOSIE.Application.Interfaces;
using PortalCOSIE.Domain;

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
            return PartialView("_Carreras", await _carreraService.ListarTodoCarrerasAsync());
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

        [HttpGet]
        [Authorize(Roles = "Administrador, Personal")]
        public async Task<IActionResult> Unidades(string id)
        {
            return View(await _carreraService.ListarCarreraConUnidadesAsync(id));
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador, Personal")]
        public async Task<IActionResult> ToggleUnidad(int id)
        {
            await _carreraService.EliminarUnidad(id);
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region ESTADO_TRAMITE
        [HttpGet]
        [Authorize(Roles = "Administrador, Personal")]
        public async Task<IActionResult> Estados()
        {
            return PartialView("_EstadoTramite", await _catalogoService.ListarTodoEstadoTramite());
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
            return PartialView("_EstadoDocumento", await _catalogoService.ListarTodoEstadosDocumento());
        }

        [HttpPost]
        [Authorize(Roles = "Administrador, Personal")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EstadosDocumento(string nombre)
        {
            //try
            //{
            //    await _catalogoService.CrearEstadoDocumento(nombre);
            //    return RedirectToAction(nameof(Index));
            //}
            //catch (DomainException ex)
            //{
            //    TempData["AlertType"] = "error";
            //    TempData["AlertMessage"] = $"{ex.Message}";
            //    return RedirectToAction(nameof(Index));
            //}
            //catch (Exception ex)
            //{
            //    TempData["AlertType"] = "error";
            //    TempData["AlertMessage"] = $"{ex.Message}";
            //    return RedirectToAction(nameof(Index));
            //}

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
            return PartialView("_TipoTramite", await _catalogoService.ListarTodoTipoTramite());
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
            return PartialView("_Periodos", await _catalogoService.ListarPeriodoConfig());
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