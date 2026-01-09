using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortalCOSIE.Application.Features.Usuarios.Commands.EditarPersonal;
using PortalCOSIE.Application.Features.Usuarios.DTO;
using PortalCOSIE.Application.Features.Usuarios.Queries.ListarPersonal;
using PortalCOSIE.Application.Services;

namespace PortalCOSIE.Web.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class PersonalController : Controller
    {
        private readonly ISecurityService _securityService;
        private readonly IMediator _mediator;

        public PersonalController(
            ISecurityService securityService,
            IMediator mediator
            )
        {
            _securityService = securityService;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
            => View(await _mediator.Send(new ListarPersonalQuery()));

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(CrearPersonalDTO dto)
        {
            var result = await _securityService.CrearPersonalAsync(dto);
            if (!result.Succeeded)
            {
                return Json(new { success = false, message = result.Errors });
            }
            return Json(new { success = true, message = result.Value });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(EditarPersonalCommand command)
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
        public async Task<IActionResult> VerificarCorreo(string userId, string correo)
        {
            var result = await _securityService.VerificarCorreoAsync(userId, correo);
            if (!result.Succeeded)
            {
                return Json(new { success = false, message = result.Errors });
            }
            return Json(new { success = true, message = result.Value });
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
    }
}