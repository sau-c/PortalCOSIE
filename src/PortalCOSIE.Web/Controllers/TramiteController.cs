using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortalCOSIE.Application;
using PortalCOSIE.Application.Interfaces.Common;
using PortalCOSIE.Domain.Entities;

namespace PortalCOSIE.Web.Controllers
{
    [Authorize]
    public class TramiteController : Controller
    {
        private readonly IService<Tramite> _tramiteService;
        public TramiteController(IService<Tramite> tramiteService)
        {
            _tramiteService = tramiteService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(_tramiteService.ListarTodos());
        }

        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Crear(Tramite tramite)
        {
            if (ModelState.IsValid)
            {
                _tramiteService.Crear(tramite);
                return RedirectToAction(nameof(Index));
            }
            return View(tramite);
        }

        [HttpGet]
        public IActionResult Editar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tramite = _tramiteService.ObtenerPorId((int)id);
            if (tramite == null)
            {
                return NotFound();
            }
            return View(tramite);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(int id, Tramite tramite)
        {
            if (id != tramite.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _tramiteService.Actualizar(tramite);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TramiteExists(tramite.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tramite);
        }

        [HttpGet]
        public IActionResult Eliminar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tramite = _tramiteService.ObtenerPorId((int)id);
            if (tramite == null)
            {
                return NotFound();
            }

            return View(tramite);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Eliminar(int id)
        {
            var tramite = _tramiteService.ObtenerPorId(id);
            if (tramite != null)
            {
                _tramiteService.Eliminar(id);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool TramiteExists(int id)
        {
            return _tramiteService.ObtenerPorId(id) != null;
        }
    }
}