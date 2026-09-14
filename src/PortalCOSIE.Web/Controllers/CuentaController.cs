using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PortalCOSIE.Application.Features.Certificados.Commands.RegistrarCa;
using PortalCOSIE.Application.Features.Certificados.Commands.SolicitarAcuse;
using PortalCOSIE.Application.Features.Certificados.Commands.SolicitarAlumno;
using PortalCOSIE.Application.Features.Certificados.Queries.ObtenerCa;
using PortalCOSIE.Application.Features.Certificados.Queries.ObtenerCertificadoAcuse;
using PortalCOSIE.Application.Features.Certificados.Queries.ObtenerMiCertificado;
using PortalCOSIE.Application.Features.Carreras.Queries.Listar;
using PortalCOSIE.Application.Features.PeriodosConfig.Queries.ListarPeriodos;
using PortalCOSIE.Application.Features.Security.Commands.ActualizarCorreo;
using PortalCOSIE.Application.Features.Security.Commands.CambiarContrasena;
using PortalCOSIE.Application.Features.Security.Commands.CerrarSesion;
using PortalCOSIE.Application.Features.Security.Commands.ConfirmarCorreo;
using PortalCOSIE.Application.Features.Security.Commands.CrearCuenta;
using PortalCOSIE.Application.Features.Security.Commands.Ingresar;
using PortalCOSIE.Application.Features.Security.Commands.RecuperarContrasena;
using PortalCOSIE.Application.Features.Security.Commands.RestablecerContrasena;
using PortalCOSIE.Application.Features.Usuarios.Commands.RegistrarAlumno;
using PortalCOSIE.Application.Features.Usuarios.DTO;
using PortalCOSIE.Application.Features.Usuarios.Queries.ObtenerAlumnoCompleto;
using PortalCOSIE.Application.Features.Usuarios.Queries.ObtenerUsuarioPorIdentityId;
using PortalCOSIE.Web.Models;
using System.Security.Claims;

namespace PortalCOSIE.Web.Controllers;

public class CuentaController : Controller
{
    private readonly IMediator _mediator;

    public CuentaController(IMediator mediator)
        => _mediator = mediator;

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Index()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrWhiteSpace(userId))
            return RedirectToAction(nameof(Ingresar));

        if (User.IsInRole("Administrador"))
        {
            ViewData["CaCertificado"] = await _mediator.Send(new ObtenerCaQuery());
            ViewData["CertificadoAcuse"] = await _mediator.Send(new ObtenerCertificadoAcuseQuery());
        }
        if (User.IsInRole("Alumno"))
            ViewData["MiCertificado"] = await _mediator.Send(new ObtenerMiCertificadoQuery(userId));

        var usuario = await _mediator.Send(new ObtenerUsuarioCompletoQuery(userId));
        if (usuario == null)
        {
            usuario = new UsuarioDTO
            {
                IdentityUserId = userId,
                Nombre = User.IsInRole("Administrador") ? "Administrador" : (User.Identity?.Name ?? "Usuario"),
                ApellidoPaterno = string.Empty,
                ApellidoMaterno = string.Empty,
                Correo = User.Identity?.Name ?? string.Empty,
                CorreoConfirmado = true,
                Celular = string.Empty,
                Rol = User.IsInRole("Administrador") ? "Administrador" : User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value ?? string.Empty
            };
        }

        return View(usuario);
    }

    [HttpPost]
    [Authorize(Roles = "Alumno")]
    public async Task<IActionResult> SolicitarCertificado([FromBody] SolicitarCertificadoRequest request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrWhiteSpace(request.CsrBase64))
            return Json(new { success = false, message = "La solicitud CSR es obligatoria." });

        var result = await _mediator.Send(new SolicitarCertificadoAlumnoCommand(userId!, request.CsrBase64));
        if (!result.Succeeded)
            return Json(new { success = false, message = string.Join(", ", result.Errors) });

        return Json(new { success = true, message = result.Value });
    }

    [HttpPost]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> SolicitarCertificadoAcuse([FromBody] SolicitarCertificadoRequest request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrWhiteSpace(request.CsrBase64))
            return Json(new { success = false, message = "La solicitud CSR es obligatoria." });

        var result = await _mediator.Send(new SolicitarCertificadoAcuseCommand(userId!, request.CsrBase64));
        if (!result.Succeeded)
            return Json(new { success = false, message = string.Join(", ", result.Errors) });

        return Json(new { success = true, message = result.Value });
    }

    [HttpPost]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> RegistrarCa(IFormFile certificadoCa)
    {
        if (certificadoCa == null || certificadoCa.Length == 0)
            return Json(new { success = false, message = "El archivo .cer de la CA es obligatorio." });

        await using var stream = certificadoCa.OpenReadStream();
        var result = await _mediator.Send(new RegistrarCaCommand(stream));

        if (!result.Succeeded)
            return Json(new { success = false, message = result.Errors });

        return Json(new { success = true, message = result.Value });
    }

    [HttpGet]
    public IActionResult Crear()
    {
        if (User?.Identity?.IsAuthenticated == true)
            return RedirectToAction("Index", "Calendario");

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Crear(CrearCuentaDTO dto)
    {
        var result = await _mediator.Send(new CrearCuentaCommand(dto));
        if (!result.Succeeded)
            return Json(new { success = false, message = result.Errors });

        return Json(new { success = true, message = result.Value });
    }

    [HttpGet]
    public async Task<IActionResult> Confirmar(string correo, string token)
    {
        if (User?.Identity?.IsAuthenticated == true)
            return RedirectToAction("Index", "Calendario");

        var result = await _mediator.Send(new ConfirmarCorreoCommand(correo, token));

        if (!result.Succeeded)
        {
            TempData["MessageType"] = "error";
            TempData["Message"] = string.Join(", ", result.Errors);
            return RedirectToAction(nameof(Ingresar));
        }

        TempData["MessageType"] = "success";
        TempData["Message"] = result.Value;
        return RedirectToAction(nameof(Ingresar));
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Registrar()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (await _mediator.Send(new ObtenerUsuarioPorIdentityIdQuery(userId)) != null)
            return RedirectToAction("Index", "Calendario");

        ViewBag.Carreras = new SelectList(await _mediator.Send(new ListarCarrerasQuery()), "Id", "Nombre");
        ViewBag.Periodos = new SelectList(await _mediator.Send(new ListarPeriodosQuery()), "Periodo");
        return View();
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Registrar(RegistrarDTO dto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var result = await _mediator.Send(new RegistrarAlumnoCommand(
            userId,
            dto.Nombre,
            dto.ApellidoPaterno,
            dto.ApellidoMaterno,
            dto.NumeroBoleta,
            dto.PeriodoIngreso,
            dto.CarreraId));

        if (!result.Succeeded)
            return Json(new { success = false, message = result.Errors });

        return Json(new { success = true, message = result.Value });
    }

    [HttpGet]
    public IActionResult Ingresar()
    {
        if (User?.Identity?.IsAuthenticated == true)
            return RedirectToAction("Index", "Calendario");

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Ingresar(IngresarDTO dto)
    {
        var result = await _mediator.Send(new IngresarCommand(dto));
        if (!result.Succeeded)
            return Json(new { success = false, message = result.Errors });

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (await _mediator.Send(new ObtenerUsuarioPorIdentityIdQuery(userId)) == null && !User.IsInRole("Administrador"))
            return RedirectToAction(nameof(Registrar));

        return RedirectToAction("Index", "Calendario");
    }

    [HttpGet]
    public IActionResult Recuperar() => View();

    [HttpPost]
    public async Task<IActionResult> Recuperar(string correo)
    {
        var result = await _mediator.Send(new RecuperarContrasenaCommand(correo));
        if (!result.Succeeded)
            return Json(new { success = false, message = result.Errors });

        return Json(new { success = true, message = result.Value });
    }

    [HttpGet]
    public IActionResult Restablecer(string correo, string token)
    {
        if (string.IsNullOrEmpty(correo) || string.IsNullOrEmpty(token))
            return RedirectToAction(nameof(Ingresar));

        return View(new RestablecerDTO { Correo = correo, Token = token });
    }

    [HttpPost]
    public async Task<IActionResult> Restablecer(RestablecerDTO dto)
    {
        var result = await _mediator.Send(new RestablecerContrasenaCommand(dto));
        if (!result.Succeeded)
            return Json(new { success = false, message = result.Errors });

        return Json(new { success = true, message = result.Value });
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CambiarContrasena(CambiarContrasenaDTO dto)
    {
        var result = await _mediator.Send(new CambiarContrasenaCommand(dto));
        if (!result.Succeeded)
            return Json(new { success = false, message = result.Errors });

        return Json(new { success = true, message = result.Value });
    }

    [HttpGet]
    public async Task<IActionResult> ActualizarCorreo(string id, string correo, string token)
    {
        if (User?.Identity?.IsAuthenticated == true)
            return RedirectToAction("Index", "Calendario");

        var result = await _mediator.Send(new ActualizarCorreoCommand(id, correo, token));
        if (!result.Succeeded)
        {
            TempData["MessageType"] = "error";
            TempData["Message"] = string.Join(", ", result.Errors);
            return RedirectToAction(nameof(Ingresar));
        }

        TempData["MessageType"] = "success";
        TempData["Message"] = result.Value;
        return RedirectToAction(nameof(Ingresar));
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Salir()
    {
        await _mediator.Send(new CerrarSesionCommand());
        return RedirectToAction("Ingresar", "Cuenta");
    }

    [HttpGet]
    [Authorize]
    public IActionResult Denegado() => View();
}