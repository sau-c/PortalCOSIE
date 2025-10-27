using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PortalCOSIE.Application.DTO.Usuario;
using PortalCOSIE.Application.Interfaces;

namespace PortalCOSIE.Web.Controllers
{
    [Authorize]
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
        [Authorize(Roles = "Administrador, Personal")]
        public async Task<IActionResult> Index()
        {
            return View(await _securityService.ListarAlumnos());
        }

        [HttpGet]
        [Authorize(Roles = "Administrador, Personal")]
        public async Task<IActionResult> Editar(string id)
        {
            ViewBag.Carreras = new SelectList(await _carreraService.ListarCarrerasAsync(), "Id", "Nombre");
            ViewBag.Periodos = new SelectList(await _catalogoService.ListarPeriodos(), "Periodo");
            return View(await _usuarioService.BuscarAlumno(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador, Personal")]
        public async Task<IActionResult> Editar(AlumnoDTO dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Carreras = new SelectList(await _carreraService.ListarCarrerasAsync(), "Id", "Nombre");
                ViewBag.Periodos = new SelectList(await _catalogoService.ListarPeriodos(), "Periodo");
                return View(dto);
            }
            var result = await _usuarioService.EditarAlumno(dto);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ViewBag.Carreras = new SelectList(await _carreraService.ListarCarrerasAsync(), "Id", "Nombre");
                    ViewBag.Periodos = new SelectList(await _catalogoService.ListarPeriodos(), "Periodo");
                    ModelState.AddModelError(string.Empty, error);
                }
                return View(dto);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador, Personal")]
        public async Task<IActionResult> ActualizarRol(string userId, string rol)
        {
            var result = await _securityService.ToggleRol(userId, rol);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador, Personal")]
        public async Task<IActionResult> ActualizarCorreo(string userId, string correo)
        {
            var result = await _securityService.ActualizarCorreoAsync(userId, correo);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador, Personal")]
        public async Task<IActionResult> Eliminar(string id)
        {
            var result = await _securityService.EliminarUsuario(id);
            return RedirectToAction(nameof(Index), result);
        }

    }
}