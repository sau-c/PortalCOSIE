using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortalCOSIE.Application;
using PortalCOSIE.Application.Interfaces;
using PortalCOSIE.Application.Interfaces.Common;
using PortalCOSIE.Domain.Entities;

namespace PortalCOSIE.Web.Controllers
{
    [Authorize]
    public class TramiteController : Controller
    {
        private readonly ITramiteService _tramiteService;
        public TramiteController(ITramiteService tramiteService)
        {
            _tramiteService = tramiteService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _tramiteService.ListarTodos());
        }

        [HttpGet]
        public IActionResult SolicitarCTE() {
            return View();
        }

        [HttpGet]
        public IActionResult SolicitarCGC()
        {
            return View();
        }
    }
}