using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PortalCOSIE.Application.DTO.Cuenta;
using PortalCOSIE.Application.Interfaces;

namespace PortalCOSIE.Web.Controllers
{
    [Authorize(Roles = "Administrador, Personal")]
    public class AlumnoController : Controller
    {
        private readonly ISecurityService _securityService;
        private readonly IUsuarioService _usuarioService;
        private readonly ICarreraService _carreraService;
        private readonly ICatalogoService _catalogoService;

        public AlumnoController(
            ISecurityService securityService,
            IUsuarioService usuarioService,
            ICarreraService carreraService,
            ICatalogoService catalogoService
            )
        {
            _securityService = securityService;
            _usuarioService = usuarioService;
            _carreraService = carreraService;
            _catalogoService = catalogoService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ViewBag.Carreras = new SelectList(await _carreraService.ListarActivasAsync(), "Id", "Nombre");
            ViewBag.Periodos = new SelectList(await _catalogoService.ListarPeriodos(), "Periodo");
            return View(await _securityService.ListarAlumnos());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(AlumnoDTO dto)
        {
            var result = await _usuarioService.EditarAlumno(dto);
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
            var result = await _securityService.VerificarCorreoAsync(userId, correo);
            if (!result.Succeeded)
            {
                return Json(new { success = false, message = result.Errors });
            }
            return Json(new { success = true, message = result.Value });
        }
    }
}