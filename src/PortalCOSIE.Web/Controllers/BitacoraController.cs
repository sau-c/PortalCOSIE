using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PortalCOSIE.Web.Controllers
{
    [Authorize(Roles = "Administrador, Personal")]
    public class BitacoraController : Controller
    {
        [HttpGet]
        public IActionResult Index() => View();
    }
}
