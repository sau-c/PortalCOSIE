using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortalCOSIE.Application.DTO.Cuenta;
using PortalCOSIE.Application.Interfaces;
using System.Security.Claims;

namespace PortalCOSIE.Web.Controllers
{
    public class CuentaController : Controller
    {
        private readonly IAuthService _authService;
        private readonly ISecurityService _securityService;
        private readonly IUsuarioService _usuarioService;

        public CuentaController(IAuthService authService, ISecurityService securityService, IUsuarioService usuarioService)
        {
            _authService = authService;
            _securityService = securityService;
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public IActionResult Crear()
        {
            if (User?.Identity?.IsAuthenticated == true)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(CrearCuentaDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            var result = await _securityService.CrearUsuarioAsync(dto);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error);
                }
                return View(dto);
            }
            else
            {
                TempData["Message"] = result.Value;
            }

            return View("Ingresar");
        }

        [HttpGet]
        public async Task<IActionResult> Confirmar(string correo, string token)
        {
            if (User?.Identity?.IsAuthenticated == true)
            {
                return RedirectToAction("Index", "Home");
            }

            var result = await _securityService.ConfirmarCorreoAsync(correo, token);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error);
                }
            }
            else
            {
                TempData["Message"] = result.Value;
            }

            return View("Ingresar");
        }

        [HttpGet]
        public IActionResult Ingresar()
        {
            if (User?.Identity?.IsAuthenticated == true)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Ingresar(IngresarDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            var result = await _authService.IngresarUsuarioAsync(dto);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error);
                }
                return View(dto);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Authorize]
        public IActionResult Registrar()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (_usuarioService.BuscarUsuarioPorIdentityId(userId) != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Registrar(RegistrarDTO dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            if (_usuarioService.BuscarUsuarioPorIdentityId(userId) != null)
            {
                ModelState.AddModelError(string.Empty, "No es posible registrar este usuario");
            }

            var result = _usuarioService.RegistrarAlumno(dto, userId);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error);
                }
                return View(dto);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Recuperar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Recuperar(CorreoDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            var result = await _securityService.RecuperarContrasenaAsync(dto);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error);
                }
                return View(dto);
            }
            TempData["Message"] = result.Value;
            return View("Ingresar");
        }

        [HttpGet]
        public IActionResult Restablecer(string correo, string token)
        {
            if (string.IsNullOrEmpty(correo) || string.IsNullOrEmpty(token))
                return RedirectToAction("Error", "Home");

            var model = new RestablecerDTO { Correo = correo, Token = token };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Restablecer(RestablecerDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            var result = await _securityService.RestablecerContrasenaAsync(dto);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error);
                }
                return View(dto);
            }
            return RedirectToAction("Ingresar");
        }

        [HttpGet]
        [Authorize]
        public IActionResult MisDatos()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var alumno = _usuarioService.BuscarAlumnoPorId(userId);
            if (alumno == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(alumno);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Salir()
        {
            await _authService.CerrarSesionAsync();
            return RedirectToAction("Ingresar", "Cuenta");
        }

        [HttpGet]
        public IActionResult Denegado()
        {
            return View();
        }
    }
}
