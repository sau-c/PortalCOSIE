using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PortalCOSIE.Application.Interfaces;

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
        [Authorize(Roles = "Alumno")]
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
    }
}