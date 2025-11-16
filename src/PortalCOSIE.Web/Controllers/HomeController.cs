using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortalCOSIE.Application.Interfaces;

namespace PortalCOSIE.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICatalogoService _catalogoService;

        public HomeController(
            ICatalogoService catalogoService
            )
        {
            _catalogoService = catalogoService;
        }

        [HttpGet]
        [Authorize]
        //[Authorize(Roles = "Administrador, Personal, Alumno")]
        public async Task<IActionResult> Index()
        {
            if (User.IsInRole("Administrador") || User.IsInRole("Personal"))
            {
                return View(await _catalogoService.ListarSesiones());
            }
            else
            {
                return View(await _catalogoService.ListarSesionesActivas());
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador, Personal")]
        public async Task<IActionResult> CrearSesionCOSIE(string numeroSesion, DateTime fechaSesion, List<DateTime> fechasRecepcion)
        {
            await _catalogoService.CrearSesion(numeroSesion, fechaSesion, fechasRecepcion);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador, Personal")]
        public async Task<IActionResult> EditarSesionCOSIE(int id, string numeroSesion, DateTime fechaSesion, List<DateTime> fechasRecepcion)
        {
            await _catalogoService.EditarSesion(id, numeroSesion, fechaSesion, fechasRecepcion);
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador, Personal")]
        public async Task<IActionResult> ToggleSesionCOSIE(int id)
        {
            await _catalogoService.ToggleSesion(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
