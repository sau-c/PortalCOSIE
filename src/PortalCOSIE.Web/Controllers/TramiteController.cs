using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PortalCOSIE.Application.Interfaces;
using PortalCOSIE.Domain.Entities;

namespace PortalCOSIE.Web.Controllers
{
    [Authorize]
    public class TramiteController : Controller
    {
        private readonly ITramiteService _tramiteService;
        private readonly ICarreraService _carreraService;

        public TramiteController(ITramiteService tramiteService, ICarreraService catalogoService)
        {
            _tramiteService = tramiteService;
            _carreraService = catalogoService;
        }

        [HttpGet]
        [Authorize(Roles = "Administrador, Personal, Alumno")]
        public async Task<IActionResult> Index()
        {
            return View(await _tramiteService.ListarTodos());
        }

        [HttpGet]
        [Authorize(Roles = "Administrador, Alumno")]
        public async Task<IActionResult> SolicitarCTE(string carrera)
        {
            ViewBag.Unidades = new SelectList(await _carreraService.ListarUnidadesAsync(carrera), "Id", "Nombre");
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Alumno")]
        public IActionResult SolicitarCGC()
        {
            return View();
        }

        // Catalogo
        [HttpGet]
        [Authorize(Roles = "Administrador, Personal")]
        public async Task<IActionResult> Estados()
        {
            return View(await _tramiteService.ListarEstados());
        }

        [HttpPost]
        [Authorize(Roles = "Administrador, Personal")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Estados(TramiteEstado tramiteEstado)
        {
            await _tramiteService.CrearEstado(tramiteEstado);
            return Redirect("Estados");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador, Personal")]
        public async Task<IActionResult> EliminarEstado(int id)
        {
            await _tramiteService.EliminarEstado(id);
            return Redirect("Estados");
        }

    }
}