using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortalCOSIE.Application.Interfaces;

namespace PortalCOSIE.Web.Controllers
{
    [Authorize]
    public class CarreraController : Controller
    {
        private readonly ICatalogoService _catalogoService;

        public CarreraController(ICatalogoService catalogoService)
        {
            _catalogoService = catalogoService;
        }

        [HttpGet]
        [Authorize(Roles = "Administrador, Personal")]
        public async Task<IActionResult> Index()
        {
            return View(await _catalogoService.ListarCarrerasAsync());
        }

        [HttpGet]
        [Authorize(Roles = "Administrador, Personal")]
        public async Task<IActionResult> UnidadAprendizaje(int id)
        {
            return View(await _catalogoService.ListarUnidadesAprendizajeAsync(id));
        }
    }
}