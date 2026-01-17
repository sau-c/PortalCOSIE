using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PortalCOSIE.Application.Features.Carreras.Queries.Listar;
using PortalCOSIE.Application.Features.PeriodosConfig.Queries.ListarPeriodos;
using PortalCOSIE.Application.Features.Usuarios.Commands.EditarAlumno;
using PortalCOSIE.Application.Features.Usuarios.Queries.ListarAlumnos;
using PortalCOSIE.Application.Services;

namespace PortalCOSIE.Web.Controllers
{
    [Authorize(Roles = "Administrador, Personal")]
    public class AlumnoController : Controller
    {
        private readonly ISecurityService _securityService;
        private readonly IMediator _mediator;

        public AlumnoController(
            ISecurityService securityService,
            IMediator mediator
            )
        {
            _securityService = securityService;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ViewBag.Carreras = new SelectList(await _mediator.Send(new ListarCarrerasQuery()), "Id", "Nombre");
            ViewBag.Periodos= new SelectList(await _mediator.Send(new ListarPeriodosQuery()), "Periodo");
            return View(await _mediator.Send(new ListarAlumnosQuery()));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(EditarAlumnoCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.Succeeded)
            {
                return Json(new { success = false, message = result.Errors });
            }
            return Json(new { success = true, message = result.Value });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ActualizarRol(string userId, string rol)
        {
            var result = await _securityService.ToggleRol(userId, rol);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ActualizarCelular(string userId, string celular)
        {
            var result = await _securityService.ActualizarCelular(userId, celular);
            if (!result.Succeeded)
            {
                return Json(new { success = false, message = result.Errors });
            }
            return Json(new { success = true, message = result.Value });
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> VerificarCorreo(string userId, string correo)
        {
            var result = await _securityService.VerificarCorreo(userId, correo);
            if (!result.Succeeded)
            {
                return Json(new { success = false, message = result.Errors });
            }
            return Json(new { success = true, message = result.Value });
        }
    }
}