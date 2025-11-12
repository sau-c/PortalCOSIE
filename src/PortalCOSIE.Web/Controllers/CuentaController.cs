using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        private readonly ICarreraService _carreraService;
        private readonly ICatalogoService _catalogoService;

        public CuentaController(
            IAuthService authService,
            ISecurityService securityService,
            IUsuarioService usuarioService,
            ICarreraService carreraService,
            ICatalogoService catalogoService
            )
        {
            _authService = authService;
            _securityService = securityService;
            _usuarioService = usuarioService;
            _carreraService = carreraService;
            _catalogoService = catalogoService;
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
                return RedirectToAction("Index", "Home");
            }

            var result = await _securityService.ConfirmarCorreoAsync(correo, token);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    TempData["Message"] = result.Value;
                }
            }
            else
            {
                TempData["Message"] = result.Value;
            }
            return RedirectToAction(nameof(Ingresar));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Registrar()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (await _usuarioService.BuscarUsuarioPorIdentityId(userId) != null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Carreras = new SelectList(await _carreraService.ListarActivasAsync(), "Id", "Nombre");
            ViewBag.Periodos = new SelectList(await _catalogoService.ListarPeriodos(), "Periodo");
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

            if (await _usuarioService.BuscarUsuarioPorIdentityId(userId) != null)
            {
                ModelState.AddModelError(string.Empty, "No es posible registrar este usuario");
            }

            var result = await _usuarioService.RegistrarAlumno(dto, userId);

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
            var result = await _authService.IngresarUsuarioAsync(dto);
            if (!result.Succeeded)
            {
                return Json(new { success = false, message = result.Errors });
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (await _usuarioService.BuscarUsuarioPorIdentityId(userId) == null && !User.IsInRole("Administrador"))
            {
                return RedirectToAction(nameof(Registrar));
            }
            return RedirectToAction("Index", "Home");
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //[Authorize]
        //public async Task<IActionResult> Registrar(RegistrarAlumnoDTO dto)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        ViewBag.Carreras = new SelectList(await _carreraService.ListarActivasAsync(), "Id", "Nombre");
        //        ViewBag.Periodos = new SelectList(await _catalogoService.ListarPeriodos(), "Periodo");
        //        return View(dto);
        //    }

        //    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        //    if (_usuarioService.BuscarUsuarioPorIdentityId(userId) != null || User.IsInRole("Administrador"))
        //    {
        //        ModelState.AddModelError(string.Empty, "No es posible registrar este usuario");
        //    }

        //    var result = await _usuarioService.RegistrarAlumnoPendiente(dto);

        //    if (!result.Succeeded)
        //    {
        //        foreach (var error in result.Errors)
        //        {
        //            ModelState.AddModelError(string.Empty, error);
        //            ViewBag.Carreras = new SelectList(await _carreraService.ListarActivasAsync(), "Id", "Nombre");
        //            ViewBag.Periodos = new SelectList(await _catalogoService.ListarPeriodos(), "Periodo");
        //        }
        //        return View(dto);
        //    }

        //    return RedirectToAction("Index", "Home");
        //}

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

        [HttpGet]
        [Authorize(Roles = "Personal, Alumno")]
        public async Task<IActionResult> MisDatos()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var alumno = await _usuarioService.BuscarAlumno(userId);
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
