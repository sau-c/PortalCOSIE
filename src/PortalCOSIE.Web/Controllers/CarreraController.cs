using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortalCOSIE.Application.Interfaces;
using PortalCOSIE.Domain;
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
        public async Task<IActionResult> Crear(string nombre)
        {
            try
            {
                await _carreraService.CrearCarreraAsync(nombre);
                return RedirectToAction("Index");
            }
            catch (DomainException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return RedirectToAction("Index");
            }
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
        public async Task<IActionResult> Eliminar(int id)
        {
            await _carreraService.EliminarCarrrera(id);
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