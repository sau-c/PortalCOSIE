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
        private readonly ICatalogoService _catalogoService;

        public TramiteController(ITramiteService tramiteService, ICatalogoService catalogoService)
        {
            _tramiteService = tramiteService;
            _catalogoService = catalogoService;
        }

        [HttpGet]
        [Authorize(Roles = "Alumno")]
        public async Task<IActionResult> Index()
        {
            return View(await _tramiteService.ListarTodos());
        }

        [HttpGet]
        [Authorize(Roles = "Alumno")]
        public async Task<IActionResult> SolicitarCTE()
        {
            ViewBag.Unidades = new SelectList(await _catalogoService.ListarUnidadesAprendizajeAsync(), "Id", "Nombre", "Semestre");
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