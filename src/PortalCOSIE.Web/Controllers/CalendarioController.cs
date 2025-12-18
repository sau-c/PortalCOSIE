using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortalCOSIE.Application.Interfaces;

namespace PortalCOSIE.Web.Controllers
{
    public class CalendarioController : Controller
    {
        private readonly IPeriodosService _catalogoService;

        public CalendarioController(
            IPeriodosService catalogoService
            )
        {
            _catalogoService = catalogoService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index()
        {
            if (User.IsInRole("Administrador"))
            {
                return View(await _catalogoService.ListarSesiones());
            }
            return View(await _catalogoService.ListarSesionesActivas());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> CrearSesionCOSIE(string numeroSesion, DateTime fechaSesion, List<DateTime> fechasRecepcion)
        {
            await _catalogoService.CrearSesion(numeroSesion, fechaSesion, fechasRecepcion);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> EditarSesionCOSIE(int id, string numeroSesion, DateTime fechaSesion, List<DateTime> fechasRecepcion)
        {
            await _catalogoService.EditarSesion(id, numeroSesion, fechaSesion, fechasRecepcion);
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> ToggleSesionCOSIE(int id)
        {
            await _catalogoService.ToggleSesion(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Error(string message, int code = 500)
        {
            // Asignamos el código de estado HTTP real para que el navegador no crea que fue un éxito (200)
            Response.StatusCode = code;

            // Puedes usar ViewBag o un Modelo dedicado
            ViewBag.ErrorMessage = message ?? "Ocurrió un error inesperado.";
            ViewBag.ErrorCode = code;

            // Si la vista espera un modelo específico (ej. ErrorViewModel), créalo aquí:
            // var model = new ErrorViewModel { RequestId = User?.Id ?? HttpContext ...etc};
            return View("Error");
        }
    }
}
