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
        private readonly ISecurityService _securityService;
        private readonly ICuentaCorreoService _cuentaCorreoService;
        private readonly IUsuarioService _usuarioService;
        private readonly ICarreraService _carreraService;
        private readonly IPeriodosService _catalogoService;

        public CuentaController(
            ISecurityService securityService,
            ICuentaCorreoService cuentaCorreoService,
            IUsuarioService usuarioService,
            ICarreraService carreraService,
            IPeriodosService catalogoService
            )
        {
            _securityService = securityService;
            _cuentaCorreoService = cuentaCorreoService;
            _usuarioService = usuarioService;
            _carreraService = carreraService;
            _catalogoService = catalogoService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (User.IsInRole("Administrador"))
            {
                var admin = await _securityService.ListarAlumnos();
                return View("_Admin", admin);
            }

            if (User.IsInRole("Personal"))
            {
                var personal = await _usuarioService.BuscarPersonal(userId);
                return View("_Personal", personal);
            }

            if (User.IsInRole("Alumno"))
            {
                var alumno = await _securityService.BuscarAlumnoCompleto(userId);
                return View("_Alumno", alumno);
            }
            return Json(new { success = false, message = "No tienes un rol valido" });
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

            var result = await _cuentaCorreoService.ConfirmarCorreoAsync(correo, token);

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

            if (await _usuarioService.BuscarUsuarioPorIdentityId(userId) != null)
            {
                return RedirectToAction("Index", "Calendario");
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
            var result = await _usuarioService.RegistrarAlumno(dto, userId);

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
            if (await _usuarioService.BuscarUsuarioPorIdentityId(userId) == null && !User.IsInRole("Administrador"))
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

            var result = await _cuentaCorreoService.ActualizarCorreoAsync(id, correo, token);
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
