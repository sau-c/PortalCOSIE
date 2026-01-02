using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PortalCOSIE.Application.Features.Carreras.Queries.ListarUnidades;
using PortalCOSIE.Application.Features.PeriodosConfig.Queries.ListarPeriodos;
using PortalCOSIE.Application.Features.Tramites.Commands.AsignarPersonal;
using PortalCOSIE.Application.Features.Tramites.Commands.SolicitarCTCE;
using PortalCOSIE.Application.Features.Tramites.DTO;
using PortalCOSIE.Application.Features.Tramites.Queries.ListarEstadosTramite;
using PortalCOSIE.Application.Features.Tramites.Queries.ListarTramites;
using PortalCOSIE.Application.Features.Tramites.Queries.ObtenerDocumentoPorId;
using PortalCOSIE.Application.Features.Tramites.Queries.ObtenerTramiteCTCEPorId;
using PortalCOSIE.Web.Models;
using System.Security.Claims;

namespace PortalCOSIE.Web.Controllers
{
    public class TramiteController : Controller
    {
        private readonly IMediator _mediator;

        public TramiteController(IMediator mediator)
            => _mediator = mediator;

        [HttpGet]
        [Authorize(Roles = "Administrador, Personal, Alumno")]
        public async Task<IActionResult> Index()
        {
            var userId = User?.FindFirstValue(ClaimTypes.NameIdentifier);
            var rol = User?.FindFirstValue(ClaimTypes.Role);
            return View(await _mediator.Send(new ListarTramitesQuery(userId, rol)));
        }

        [HttpGet]
        [Authorize(Roles = "Administrador, Alumno")]
        public async Task<IActionResult> SolicitarCTCE()
        {
            string userId = User?.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewBag.Unidades = new SelectList(await _mediator.Send(new ListarUnidadesQuery(userId)), "Id", "Nombre");
            ViewBag.Periodos = new SelectList(await _mediator.Send(new ListarPeriodosQuery()), "Periodo");
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
            await _mediator.Send(new SolicitarCTCECommand(solicitudDto, userId));
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
            var tramite = await _mediator.Send(new ObtenerTramiteCTCEPorIdQuery(userId, tramiteId));
            ViewBag.EstadoDocumento = new SelectList(await _mediator.Send(new ListarEstadoDocumentoQuery()), "Id", "Nombre");

            return View(tramite);
        }

        [HttpGet]
        [Authorize(Roles = "Administrador, Personal")]
        public async Task<IActionResult> Documento(int id)
        {
            var userId = User?.FindFirstValue(ClaimTypes.NameIdentifier);
            var archivo = await _mediator.Send(new ObtenerDocumentoPorIdQuery(id, userId));
            return File(archivo.Contenido, archivo.TipoContenido);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador, Personal")]
        public async Task<IActionResult> TomarTramite(int tramiteId)
        {
            var userId = User?.FindFirstValue(ClaimTypes.NameIdentifier);
            await _mediator.Send(new AsignarPersonalCommand(userId, tramiteId));
            return Json(new { success = true, message = "Tomado con éxito." });
        }
    }
}