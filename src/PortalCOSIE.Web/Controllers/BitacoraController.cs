using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PortalCOSIE.Web.Controllers
{
    public class BitacoraController : Controller
    {
        [HttpGet]
        [Authorize(Roles = "Administrador")]
        public IActionResult Index() => View();
    }
}
