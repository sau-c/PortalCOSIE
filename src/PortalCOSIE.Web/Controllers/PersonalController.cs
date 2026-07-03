using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortalCOSIE.Application.Features.Security.Commands.ActualizarCelular;
using PortalCOSIE.Application.Features.Security.Commands.CrearPersonal;
using PortalCOSIE.Application.Features.Security.Commands.ToggleRol;
using PortalCOSIE.Application.Features.Security.Commands.VerificarCorreo;
using PortalCOSIE.Application.Features.Usuarios.Commands.EditarPersonal;
using PortalCOSIE.Application.Features.Usuarios.DTO;
using PortalCOSIE.Application.Features.Usuarios.Queries.ListarPersonal;

namespace PortalCOSIE.Web.Controllers;

[Authorize(Roles = "Administrador")]
public class PersonalController : Controller
{
    private readonly IMediator _mediator;

    public PersonalController(IMediator mediator)
        => _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> Index()
        => View(await _mediator.Send(new ListarPersonalQuery()));

    [HttpPost]
    public async Task<IActionResult> Crear(CrearPersonalDTO dto)
    {
        var result = await _mediator.Send(new CrearPersonalCommand(dto));
        if (!result.Succeeded)
            return Json(new { success = false, message = result.Errors });

        return Json(new { success = true, message = result.Value });
    }

    [HttpPost]
    public async Task<IActionResult> Editar(EditarPersonalCommand command)
    {
        var result = await _mediator.Send(command);
        if (!result.Succeeded)
            return Json(new { success = false, message = result.Errors });

        return Json(new { success = true, message = result.Value });
    }

    [HttpPost]
    public async Task<IActionResult> ActualizarRol(string userId, string rol)
    {
        var result = await _mediator.Send(new ToggleRolCommand(userId, rol));
        if (!result.Succeeded)
            return RedirectToAction(nameof(Index));

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> VerificarCorreo(string userId, string correo)
    {
        var result = await _mediator.Send(new VerificarCorreoCommand(userId, correo));
        if (!result.Succeeded)
            return Json(new { success = false, message = result.Errors });

        return Json(new { success = true, message = result.Value });
    }

    [HttpPost]
    public async Task<IActionResult> ActualizarCelular(string userId, string celular)
    {
        var result = await _mediator.Send(new ActualizarCelularCommand(userId, celular));
        if (!result.Succeeded)
            return Json(new { success = false, message = result.Errors });

        return Json(new { success = true, message = result.Value });
    }
}