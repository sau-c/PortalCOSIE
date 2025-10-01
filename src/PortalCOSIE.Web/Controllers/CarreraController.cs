using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortalCOSIE.Application.Interfaces;
using PortalCOSIE.Domain.Entities;

namespace PortalCOSIE.Web.Controllers
{
    [Authorize]
    public class CarreraController : Controller
    {
        private readonly ICarreraService _carreraService;

        public CarreraController(ICarreraService catalogoService)
        {
            _carreraService = catalogoService;
        }

        [HttpGet]
        [Authorize(Roles = "Administrador, Personal")]
        public async Task<IActionResult> Index()
        {
            return View(await _carreraService.ListarCarrerasAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador, Personal")]
        public IActionResult Crear(Carrera carrera)
        {
            //_carreraService.CrearCarrera(carrera);
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Administrador, Personal")]
        public async Task<IActionResult> Editar(string id)
        {
            return View(await _carreraService.ListarCarreraConUnidadesAsync(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador, Personal")]
        public async Task<IActionResult> Editar(int id, string Nombre)
        {
            await _carreraService.EditarCarreraAsync(id, Nombre);
            return Redirect("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador, Personal")]
        public async Task<IActionResult> EliminarUnidad(int id)
        {
            await _carreraService.EliminarUnidad(id);
            return Redirect("Index");
        }
    }
}