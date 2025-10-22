using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortalCOSIE.Application.Interfaces;
using PortalCOSIE.Domain;
using PortalCOSIE.Domain.Entities;
using PortalCOSIE.Domain.Interfaces;

namespace PortalCOSIE.Web.Controllers
{
    [Authorize]
    public class ConfiguracionController : Controller
    {
        private readonly ICatalogoService _catalogoService;
        private readonly ICarreraService _carreraService;
        private readonly IUnitOfWork _unitOfWork;

        public ConfiguracionController(
            ICarreraService catalogoService,
            ICatalogoService tramiteService,
            IUnitOfWork unitOfWork
            )
        {
            _carreraService = catalogoService;
            _catalogoService = tramiteService;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Authorize(Roles = "Administrador, Personal")]
        public IActionResult Index() => View();

        #region CARRERAS
        [HttpGet]
        [Authorize(Roles = "Administrador, Personal")]
        public async Task<IActionResult> Carreras()
        {
            return PartialView("_Carreras", await _carreraService.ListarCarrerasAsync());
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
        public async Task<IActionResult> CarreraUnidades(string id)
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
        public async Task<IActionResult> DesactivarCarrera(int id)
        {
            await _carreraService.EliminarCarrrera(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador, Personal")]
        public async Task<IActionResult> DesactivarUnidad(int id)
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
            return PartialView("_EstadoTramite", await _catalogoService.ListarEstados());
        }

        [HttpPost]
        [Authorize(Roles = "Administrador, Personal")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Estados(EstadoTramite estadoTramite)
        {
            await _catalogoService.CrearEstado(estadoTramite);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [Authorize(Roles = "Administrador, Personal")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarEstado(EstadoTramite estadoTramite)
        {
            await _catalogoService.EditarEstado(estadoTramite);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador, Personal")]
        public async Task<IActionResult> DesactivarEstado(int id)
        {
            await _catalogoService.EliminarEstado(id);
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
        public async Task<IActionResult> EstadosDocumento(EstadoDocumento estadoDocumento)
        {
            await _catalogoService.CrearEstadoDocumento(estadoDocumento);
            return RedirectToAction(nameof(EstadosDocumento));
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador, Personal")]
        public async Task<IActionResult> EditarEstadoDocumento(EstadoDocumento estadoDocumento)
        {
            await _catalogoService.EditarEstadoDocumento(estadoDocumento);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador, Personal")]
        public async Task<IActionResult> DesactivarEstadoDocumento(int id)
        {
            await _catalogoService.EliminarEstado(id);
            return RedirectToAction(nameof(EstadosDocumento));
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
        public async Task<IActionResult> Tipo(TipoTramite tipoTramite)
        {
            await _catalogoService.CrearTipoTramite(tipoTramite);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [Authorize(Roles = "Administrador, Personal")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarTipo(TipoTramite tipoTramite)
        {
            await _catalogoService.EditarTipoTramite(tipoTramite);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador, Personal")]
        public async Task<IActionResult> DesactivarTipo(int id)
        {
            await _catalogoService.EliminarTipoTramite(id);
            return RedirectToAction(nameof(Index));
        }
        
        #endregion

        [HttpGet]
        [Authorize(Roles = "Administrador, Personal")]
        public async Task<IActionResult> Periodos()
        {
            return PartialView("_Periodos", await _catalogoService.ListarConfiguracionPeriodos());
        }
    }
}