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

            // Mapeo a DTO (Convirtiendo a Bytes aquí para liberar el IFormFile)
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

                CartaExposicionMotivos = ConvertirArchivo(model.CartaExposicionMotivos),
                Identificacion = ConvertirArchivo(model.Identificacion),
                BoletaGlobal = ConvertirArchivo(model.BoletaGlobal),
                Probatorios = ConvertirArchivo(model.Probatorios)
            };

            // Ahora el servicio recibe datos puros (DTOs con byte[]) y no sabe nada de HTTP.
            await _tramiteService.SolicitarCTCE(solicitudDto, userId);
            return Json(new { success = true, message = "Solicitud enviada con éxito." });
        }

        // === Helper para convertir IFormFile a DTO ===
        private ArchivoDescargaDTO ConvertirArchivo(IFormFile archivo)
        {
            if (archivo == null || archivo.Length == 0)
                throw new ArgumentException("El archivo no puede estar vacío.");

            using (var memoryStream = new MemoryStream())
            {
                archivo.CopyTo(memoryStream);
                return new ArchivoDescargaDTO
                {
                    Nombre = archivo.FileName,
                    Contenido = memoryStream.ToArray() // Aquí obtenemos el byte[] desconectado del HTTP
                };
            }
        }

        [HttpGet]
        [Authorize(Roles = "Administrador, Personal, Alumno")]
        public async Task<IActionResult> SeguimientoCTCE(int tramiteId)
        {
            var userId = User?.FindFirstValue(ClaimTypes.NameIdentifier);
            var tramite = await _tramiteService.BuscarTramiteCTCEPorId(tramiteId, userId);
            ViewBag.EstadoDocumento = new SelectList(await _catalogoService.ListarActivosAsync(), "Id", "Nombre");

            return View(tramite);
        }

        [HttpGet]
        [Authorize(Roles = "Administrador, Personal")]
        public async Task<IActionResult> Documento(int id)
        {
            var userId = User?.FindFirstValue(ClaimTypes.NameIdentifier);
            var archivo = await _tramiteService.ObtenerDocumentoPorId(id, userId);

            // Opción A: Devolver como archivo para visualizar (Inline)
            // El navegador intentará mostrarlo (Chrome/Edge tienen visor PDF nativo)
            return File(archivo.Contenido, archivo.TipoContenido);

            // Opción B (Si quisiera forzar descarga):
            // return File(archivo.Contenido, archivo.TipoContenido, archivo.Nombre);
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