using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortalCOSIE.Application.Interfaces;
using System.Security.Claims;

namespace PortalCOSIE.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        private readonly IUsuarioService _usuarioService;

        public HomeController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }
        [HttpGet]
        public IActionResult Index()
        
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (_usuarioService.BuscarUsuarioPorIdentityId(userId) == null && !User.IsInRole("Administrador"))
            {
                return RedirectToAction("Registrar", "Cuenta");
            }
            return View();
        }

    }
}
