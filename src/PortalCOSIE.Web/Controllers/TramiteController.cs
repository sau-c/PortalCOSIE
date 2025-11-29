using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PortalCOSIE.Application.Interfaces;
using PortalCOSIE.Web.Models;
using System.Security.Claims;
using PortalCOSIE.Domain.Entities.Documentos;
using PortalCOSIE.Application.DTO.Tramites;

namespace PortalCOSIE.Web.Controllers
{
    public class TramiteController : Controller
    {
        private readonly ITramiteService _tramiteService;
        private readonly ICarreraService _carreraService;
        private readonly IPeriodosService _catalogoService;
        private readonly ISecurityService _securityService;

        public TramiteController(
            ITramiteService tramiteService,
            ICarreraService carreraService,
            IPeriodosService catalogoService,
            ISecurityService securityService
            )
        {
            _tramiteService = tramiteService;
            _carreraService = carreraService;
            _catalogoService = catalogoService;
            _securityService = securityService;
        }

        [HttpGet]
        [Authorize(Roles = "Administrador, Personal, Alumno")]
        public async Task<IActionResult> Index()
        {
            return View(await _tramiteService.ListarTodos());
        }

        [HttpGet]
        [Authorize(Roles = "Administrador, Alumno")]
        public async Task<IActionResult> SolicitarCTCE()
        {
            var userId = User?.FindFirstValue(ClaimTypes.NameIdentifier);
            var alumno = await _securityService.BuscarAlumnoCompleto(userId);

            ViewBag.Unidades = new SelectList(await _carreraService.ListarUnidadesAsync(alumno.Carrera.Nombre), "Id", "Nombre");
            ViewBag.Periodos = new SelectList(await _catalogoService.ListarPeriodos(), "Periodo");
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Administrador, Alumno")]
        public async Task<IActionResult> SolicitarCTCE(SolicitudCtceVM model)
        {
            var userId = User?.FindFirstValue(ClaimTypes.NameIdentifier);
            //var alumno = await _securityService.BuscarAlumnoCompleto(userId);

            var solicitudDto = new SolicitudCtceDTO
            {
                Situacion = model.Situacion,
                TieneDictamenesAnteriores = model.TieneDictamenesAnteriores,
                UnidadesReprobadas = model.UnidadesReprobadas.Select(u => new UnidadReprobadaDto
                {
                    UnidadId = u.UnidadId,
                    PeriodoCursado = u.PeriodoCursado,
                    PeriodoRecursado = u.PeriodoRecursado
                }).ToList(),
                CartaExposicionMotivos = new DocumentoDto
                {
                    Nombre = model.CartaExposicionMotivos.FileName,
                    Contenido = model.CartaExposicionMotivos.OpenReadStream()
                },
                Probatorios = new DocumentoDto
                {
                    Nombre = model.CartaExposicionMotivos.FileName,
                    Contenido = model.CartaExposicionMotivos.OpenReadStream()
                },
                Identificacion = new DocumentoDto
                {
                    Nombre = model.CartaExposicionMotivos.FileName,
                    Contenido = model.CartaExposicionMotivos.OpenReadStream()
                },
                BoletaGlobal = new DocumentoDto
                {
                    Nombre = model.CartaExposicionMotivos.FileName,
                    Contenido = model.CartaExposicionMotivos.OpenReadStream()
                }
            };

            // 2. Llamar al servicio. El servicio NO sabe qué es un IFormFile.
            await _tramiteService.SolicitarCTCE(solicitudDto, userId);

            return Json(new { success = true, message = "Trámite solicitado con éxito." });
        }

    }
}