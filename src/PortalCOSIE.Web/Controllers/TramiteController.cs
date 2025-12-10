using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PortalCOSIE.Application.DTO.Tramites;
using PortalCOSIE.Application.Interfaces;
using PortalCOSIE.Domain.Entities.Documentos;
using PortalCOSIE.Web.Models;
using System.Security.Claims;

namespace PortalCOSIE.Web.Controllers
{
    public class TramiteController : Controller
    {
        private readonly ITramiteService _tramiteService;
        private readonly ICatalogoService<EstadoDocumento, int> _catalogoService;
        private readonly ICarreraService _carreraService;
        private readonly IPeriodosService _periodoService;
        private readonly ISecurityService _securityService;

        public TramiteController(
            ITramiteService tramiteService,
            ICatalogoService<EstadoDocumento, int> catalogoService,
            ICarreraService carreraService,
            IPeriodosService periodoService,
            ISecurityService securityService
            )
        {
            _tramiteService = tramiteService;
            _catalogoService = catalogoService;
            _carreraService = carreraService;
            _periodoService = periodoService;
            _securityService = securityService;
        }

        [HttpGet]
        [Authorize(Roles = "Administrador, Personal, Alumno")]
        public async Task<IActionResult> Index()
        {
            var userId = User?.FindFirstValue(ClaimTypes.NameIdentifier);
            var rol = User?.FindFirstValue(ClaimTypes.Role);
            return View(await _tramiteService.ListarTodos(rol, userId));
        }

        [HttpGet]
        [Authorize(Roles = "Administrador, Alumno")]
        public async Task<IActionResult> SolicitarCTCE()
        {
            var userId = User?.FindFirstValue(ClaimTypes.NameIdentifier);
            var alumno = await _securityService.BuscarAlumnoCompleto(userId);

            ViewBag.Unidades = new SelectList(await _carreraService.ListarUnidadesAsync(alumno.Carrera.Id), "Id", "Nombre");
            ViewBag.Periodos = new SelectList(await _periodoService.ListarPeriodos(), "Periodo");
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Alumno")]
        public async Task<IActionResult> SolicitarCTCE(SolicitudCtceVM model)
        {
            var userId = User?.FindFirstValue(ClaimTypes.NameIdentifier);
            //var alumno = await _securityService.BuscarAlumnoCompleto(userId);

            var solicitudDto = new SolicitudCtceDTO
            {
                Peticion = model.Peticion,
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
            return Json(new { success = true, message = "Solicitud enviada con éxito." });
        }

        [HttpGet]
        [Authorize(Roles = "Administrador, Personal, Alumno")]
        public async Task<IActionResult> SeguimientoCTCE(int tramiteId)
        {
            var userId = User?.FindFirstValue(ClaimTypes.NameIdentifier);
            var tramite = await _tramiteService.BuscarDetalleCTCEPorId(tramiteId, userId);
            ViewBag.EstadoDocumento = new SelectList(await _catalogoService.ListarActivosAsync(), "Id", "Nombre");

            return View(tramite);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador, Personal")]
        public async Task<IActionResult> TomarTramite(int tramiteId)
        {
            var userId = User?.FindFirstValue(ClaimTypes.NameIdentifier);
            await _tramiteService.AsignarPersonal(tramiteId, userId);
            return Json(new { success = true, message = "Tomado con éxito." });
        }
    }
}