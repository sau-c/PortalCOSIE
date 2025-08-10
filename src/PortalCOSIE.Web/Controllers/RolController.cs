using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortalCOSIE.Application.Interfaces;

namespace PortalCOSIE.Web.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class RolController : Controller
    {
        private readonly ISecurityService _securityService;

        public RolController(ISecurityService securityService)
        {
            _securityService = securityService;
        }

        [HttpGet]
        public async Task <IActionResult> Index()
        {
            return View(await _securityService.ListarRoles());
        }

        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Crear(Rol rol)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _rolService.Crear(rol);
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(rol);
        //}

        //[HttpGet]
        //public IActionResult Editar(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var rol = _rolService.ObtenerPorId((int)id);
        //    if (rol == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(rol);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Editar(int id, Rol rol)
        //{
        //    if (id != rol.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _rolService.Actualizar(rol);
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!RolExists(rol.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(rol);
        //}

        //[HttpGet]
        //public IActionResult Eliminar(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var rol = _rolService.ObtenerPorId((int)id);
        //    if (rol == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(rol);
        //}

        //// POST: Rol/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Eliminar(int id)
        //{
        //    var rol = _rolService.ObtenerPorId(id);
        //    if (rol != null)
        //    {
        //        _rolService.Eliminar(id);
        //    }

        //    return RedirectToAction(nameof(Index));
        //}

        //private bool RolExists(int id)
        //{
        //    return _rolService.ObtenerPorId(id) != null;
        //}
    }
}
