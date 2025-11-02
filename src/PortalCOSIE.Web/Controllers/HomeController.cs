using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortalCOSIE.Application.Interfaces;
using System.Security.Claims;

namespace PortalCOSIE.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IUsuarioService _usuarioService;
        private readonly ICatalogoService _catalogoService;

        public HomeController(
            IUsuarioService usuarioService,
            ICatalogoService catalogoService
            )
        {
            _usuarioService = usuarioService;
            _catalogoService = catalogoService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (_usuarioService.BuscarUsuarioPorIdentityId(userId) == null && !User.IsInRole("Administrador"))
            {
                return RedirectToAction("Registrar", "Cuenta");
            }
            return View(await _catalogoService.ListarSesionCOSIE());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador, Personal")]
        public async Task<IActionResult> CrearSesionCOSIE(string numeroSesion, DateTime fechaSesion, List<DateTime> fechasRecepcion)
        {
            await _catalogoService.CrearSesionCOSIE(numeroSesion, fechaSesion, fechasRecepcion);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador, Personal")]
        public async Task<IActionResult> EditarSesionCOSIE(int id, string numeroSesion, DateTime fechaSesion, List<DateTime> fechasRecepcion)
        {
            await _catalogoService.EditarSesionCOSIE(id, numeroSesion, fechaSesion, fechasRecepcion);
            return RedirectToAction(nameof(Index));
        }
    }
}
