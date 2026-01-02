using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortalCOSIE.Application.Features.SesionesCOSIE.Commands.Crear;
using PortalCOSIE.Application.Features.SesionesCOSIE.Commands.Editar;
using PortalCOSIE.Application.Features.SesionesCOSIE.Commands.Toggle;
using PortalCOSIE.Application.Features.SesionesCOSIE.Queries.ListarDetalle;

namespace PortalCOSIE.Web.Controllers
{
    public class CalendarioController : Controller
    {
        private readonly IMediator _mediator;

        public CalendarioController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var esAdministrador = User.IsInRole("Administrador");
            return View(await _mediator.Send(new ListarSesionesCOSIEQuery(esAdministrador)));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> CrearSesionCOSIE(CrearSesionCOSIECommand command)
        {
            await _mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> EditarSesionCOSIE(EditarSesionCOSIECommand command)
        {
            await _mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> ToggleSesionCOSIE(ToggleSesionCOSIECommand command)
        {
            await _mediator.Send(command);
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
