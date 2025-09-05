using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PortalCOSIE.Application.DTO.Usuario;
using PortalCOSIE.Application.Interfaces;

namespace PortalCOSIE.Web.Controllers
{
    [Authorize]
    public class PersonalController : Controller
    {
        private readonly ISecurityService _securityService;
        private readonly IUsuarioService _usuarioService;
        private readonly ICatalogoService _catalogoService;

        public PersonalController(ISecurityService securityService, IUsuarioService usuarioService, ICatalogoService catalogoService)
        {
            _securityService = securityService;
            _usuarioService = usuarioService;
            _catalogoService = catalogoService;
        }

        [HttpGet]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Index()
        {
            return View(await _securityService.ListarPersonal());
        }

        [HttpGet]
        [Authorize(Roles = "Administrador")]
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public IActionResult Crear(PersonalConIdentityDTO dto)
        {
            return Redirect("Index");
        }

        [HttpGet]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Editar(string id)
        {
            return View(await _usuarioService.BuscarPersonal(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador, Personal")]
        public async Task<IActionResult> Editar(AlumnoDTO dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Carreras = new SelectList(await _catalogoService.ListarCarrerasAsync(), "Id", "Nombre");
                
                return View(dto);
            }
            var result = await _usuarioService.EditarAlumno(dto);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error);
                }
                return View(dto);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador, Personal")]
        public async Task<IActionResult> Eliminar(string id)
        {
            var result = await _securityService.EliminarUsuario(id);
            return RedirectToAction("Index", result);
        }

    }
}