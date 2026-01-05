using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using PortalCOSIE.Application.Features.Carreras.Queries.Listar;
using PortalCOSIE.Application.Features.PeriodosConfig.Queries.ListarPeriodos;
using PortalCOSIE.Application.Features.Usuarios.Commands.RegistrarAlumno;
using PortalCOSIE.Application.Features.Usuarios.Queries.ObtenerUsuarioPorIdentityId;
using PortalCOSIE.Application.Features.Usuarios.DTO;
using PortalCOSIE.Application.Services;
using PortalCOSIE.Application.Features.Usuarios.Queries.ObtenerAlumnoCompleto;

namespace PortalCOSIE.Web.Controllers
{
    public class CuentaController : Controller
    {
        private readonly ISecurityService _securityService;
        private readonly IMediator _mediator;
        public CuentaController(
            ISecurityService securityService,
            IMediator mediator
            )
        {
            _securityService = securityService;
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return View(await _mediator.Send(new ObtenerUsuarioCompletoQuery(userId)));
        }

        [HttpGet]
        public IActionResult Crear()
        {
            if (User?.Identity?.IsAuthenticated == true)
            {
                return RedirectToAction("Index", "Calendario");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(CrearCuentaDTO dto)
        {
            var result = await _securityService.CrearUsuarioAsync(dto);

            if (!result.Succeeded)
            {
                return Json(new { success = false, message = result.Errors });
            }
            return Json(new { success = true, message = result.Value });
        }

        [HttpGet]
        public async Task<IActionResult> Confirmar(string correo, string token)
        {
            if (User?.Identity?.IsAuthenticated == true)
            {
                return RedirectToAction("Index", "Calendario");
            }

            var result = await _securityService.ConfirmarCorreoAsync(correo, token);

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
            {
                return RedirectToAction("Index", "Calendario");
            }
            ViewBag.Carreras = new SelectList(await _mediator.Send(new ListarCarrerasQuery()), "Id", "Nombre");
            ViewBag.Periodos = new SelectList(await _mediator.Send(new ListarPeriodosQuery()), "Periodo");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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
                dto.CarreraId
                )
            );

            if (!result.Succeeded)
            {
                return Json(new { success = false, message = result.Errors });
            }
            return Json(new { success = true, message = result.Value });
        }

        [HttpGet]
        public IActionResult Ingresar()
        {
            if (User?.Identity?.IsAuthenticated == true)
            {
                return RedirectToAction("Index", "Calendario");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Ingresar(IngresarDTO dto)
        {
            var result = await _securityService.IngresarUsuarioAsync(dto);
            if (!result.Succeeded)
            {
                return Json(new { success = false, message = result.Errors });
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (await _mediator.Send(new ObtenerUsuarioPorIdentityIdQuery(userId)) == null && !User.IsInRole("Administrador"))
            {
                return RedirectToAction(nameof(Registrar));
            }
            return RedirectToAction("Index", "Calendario");
        }

        [HttpGet]
        public IActionResult Recuperar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Recuperar(string correo)
        {
            var result = await _securityService.RecuperarContrasenaAsync(correo);
            if (!result.Succeeded)
            {
                return Json(new { success = false, message = result.Errors });
            }
            return Json(new { success = true, message = result.Value });
        }

        [HttpGet]
        public IActionResult Restablecer(string correo, string token)
        {
            if (string.IsNullOrEmpty(correo) || string.IsNullOrEmpty(token))
                return RedirectToAction(nameof(Ingresar));

            var model = new RestablecerDTO { Correo = correo, Token = token };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Restablecer(RestablecerDTO dto)
        {
            var result = await _securityService.RestablecerContrasenaAsync(dto);
            if (!result.Succeeded)
            {
                return Json(new { success = false, message = result.Errors });
            }
            return Json(new { success = true, message = result.Value });
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CambiarContrasena(CambiarContrasenaDTO dto)
        {
            var result = await _securityService.CambiarContrasena(dto);
            if (!result.Succeeded)
            {
                return Json(new { success = false, message = result.Errors });
            }
            return Json(new { success = true, message = result.Value });
        }

        [HttpGet]
        public async Task<IActionResult> ActualizarCorreo(string id, string correo, string token)
        {
            if (User?.Identity?.IsAuthenticated == true)
            {
                return RedirectToAction("Index", "Calendario");
            }

            var result = await _securityService.ActualizarCorreoAsync(id, correo, token);
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
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Salir()
        {
            await _securityService.CerrarSesionAsync();
            return RedirectToAction("Ingresar", "Cuenta");
        }

        [HttpGet]
        [Authorize]
        public IActionResult Denegado()
        {
            return View();
        }
    }
}
