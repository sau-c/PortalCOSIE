using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PortalCOSIE.Application;
using PortalCOSIE.Application.Interfaces;
using System.Security.Claims;

namespace PortalCOSIE.Web.Controllers
{
    [Authorize]
    public class UsuarioController : Controller
    {
        private readonly ISecurityService _securityService;
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(ISecurityService securityService, IUsuarioService usuarioService)
        {
            _securityService = securityService;
            _usuarioService = usuarioService;
        }

        [HttpGet]
        [Authorize(Roles = "Administrador, Personal")]
        public async Task<IActionResult> Index()
        {
            return View(await _securityService.ListarUsuarios());
        }

        [HttpGet]
        [Authorize(Roles = "Administrador, Personal")]
        public IActionResult Editar(string id)
        {
            return View(_usuarioService.BuscarAlumnoPorId(id));
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //[Authorize(Roles = "Administrador, Personal")]
        //public async Task<IActionResult> Editar(string id)
        //{
        //    //var result = await _securityService.ActualizarRol(id, nuevoRol);
        //    return RedirectToAction("Index");
        //}

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