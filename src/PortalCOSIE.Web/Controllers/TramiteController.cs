using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PortalCOSIE.Application.Interfaces;
using PortalCOSIE.Domain.Entities;

namespace PortalCOSIE.Web.Controllers
{
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
        public async Task<IActionResult> SolicitarCTCE()
        {
            ViewBag.Unidades = new SelectList(await _carreraService.ListarUnidadesAsync("Mecatronica"), "Id", "Nombre");
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Administrador, Alumno")]
        public IActionResult SolicitarCTCE(int id, string ContenidoSolicitud)
        {
            return View();
        }

    }
}