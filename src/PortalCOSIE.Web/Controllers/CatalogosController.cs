using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortalCOSIE.Application.Interfaces;
using PortalCOSIE.Domain;
using PortalCOSIE.Domain.Entities;

namespace PortalCOSIE.Web.Controllers
{
    [Authorize]
    public class CatalogosController : Controller
    {
        private readonly ITramiteService _tramiteService;
        private readonly ICarreraService _carreraService;

        public CatalogosController(ICarreraService catalogoService, ITramiteService tramiteService)
        {
            _carreraService = catalogoService;
            _tramiteService = tramiteService;
        }

        [HttpGet]
        [Authorize(Roles = "Administrador, Personal")]
        public IActionResult Index() => View();

        #region CARRERAS
        [HttpGet]
        [Authorize(Roles = "Administrador, Personal")]
        public async Task<IActionResult> Carreras()
        {
            return View(await _carreraService.ListarCarrerasAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador, Personal")]
        public async Task<IActionResult> Crear(string nombre)
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
        public async Task<IActionResult> Editar(string id)
        {
            return View(await _carreraService.ListarCarreraConUnidadesAsync(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador, Personal")]
        public async Task<IActionResult> Editar(int id, string Nombre)
        {
            await _carreraService.EditarCarreraAsync(id, Nombre);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador, Personal")]
        public async Task<IActionResult> Eliminar(int id)
        {
            await _carreraService.EliminarCarrrera(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador, Personal")]
        public async Task<IActionResult> EliminarUnidad(int id)
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
            return PartialView("_TablaEstadoTramite", await _tramiteService.ListarEstados());
        }

        [HttpPost]
        [Authorize(Roles = "Administrador, Personal")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Estados(TramiteEstado tramiteEstado)
        {
            await _tramiteService.CrearEstado(tramiteEstado);
            return RedirectToAction(nameof(Estados));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador, Personal")]
        public async Task<IActionResult> EliminarEstado(int id)
        {
            await _tramiteService.EliminarEstado(id);
            return RedirectToAction(nameof(Estados));
        }
        #endregion

        #region ESTADO_DOCUMENTO
        [HttpGet]
        [Authorize(Roles = "Administrador, Personal")]
        public async Task<IActionResult> EstadosDocumento()
        {
            return PartialView("_TablaEstadoDocumento", await _tramiteService.ListarEstadosDocumento());
        }

        [HttpPost]
        [Authorize(Roles = "Administrador, Personal")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EstadosDocumento(DocumentoEstado documentoEstado)
        {
            await _tramiteService.CrearEstadoDocumento(documentoEstado);
            return RedirectToAction(nameof(EstadosDocumento));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador, Personal")]
        public async Task<IActionResult> EliminarEstadoDocumento(int id)
        {
            await _tramiteService.EliminarEstado(id);
            return RedirectToAction(nameof(EstadosDocumento));
        }
        #endregion

        //TipoTramite
        [HttpGet]
        [Authorize(Roles = "Administrador, Personal")]
        public async Task<IActionResult> Tipo()
        {
            return PartialView("_TablaTipoTramite", await _tramiteService.ListarTipoTramite());
        }

        [HttpPost]
        [Authorize(Roles = "Administrador, Personal")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Tipo(TipoTramite tipoTramite)
        {
            await _tramiteService.CrearTipoTramite(tipoTramite);
            return RedirectToAction(nameof(Tipo));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador, Personal")]
        public async Task<IActionResult> EliminarTipo(int id)
        {
            await _tramiteService.EliminarTipoTramite(id);
            return RedirectToAction(nameof(Tipo));
        }
    }
}