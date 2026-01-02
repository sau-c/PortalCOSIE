using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PortalCOSIE.Application.Interfaces;
using PortalCOSIE.Application.Features.Carreras.Queries.Listar;
using PortalCOSIE.Application.Features.PeriodosConfig.Queries.ListarPeriodos;
using PortalCOSIE.Application.Features.Usuarios.Commands.EditarAlumno;

namespace PortalCOSIE.Web.Controllers
{
    [Authorize(Roles = "Administrador, Personal")]
    public class AlumnoController : Controller
    {
        private readonly ISecurityService _securityService;
        private readonly ICuentaCorreoService _cuentaCorreoService;
        private readonly IMediator _mediator;

        public AlumnoController(
            ISecurityService securityService,
            ICuentaCorreoService cuentaCorreoService,
            IMediator mediator
            )
        {
            _securityService = securityService;
            _cuentaCorreoService = cuentaCorreoService;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ViewBag.Carreras = new SelectList(await _mediator.Send(new ListarCarrerasQuery()), "Id", "Nombre");
            ViewBag.Periodos= new SelectList(await _mediator.Send(new ListarPeriodosQuery()), "Periodo");
            return View(await _securityService.ListarAlumnos());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(EditarAlumnoCommand command)
        {
            //var result = await _usuarioService.EditarAlumno(command);
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
            var result = await _securityService.ActualizarCelularAsync(userId, celular);
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
            var result = await _cuentaCorreoService.VerificarCorreoAsync(userId, correo);
            if (!result.Succeeded)
            {
                return Json(new { success = false, message = result.Errors });
            }
            return Json(new { success = true, message = result.Value });
        }
    }
}