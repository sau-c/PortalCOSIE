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
using PortalCOSIE.Application.Features.Tramites.Queries.DescargarDocumento;
using PortalCOSIE.Application.Features.Tramites.Queries.ObtenerTramiteCTCEPorId;
using PortalCOSIE.Web.Models;
using System.Security.Claims;
using PortalCOSIE.Application.Features.Tramites.Commands.CancelarTramite;
using PortalCOSIE.Domain.Entities.Tramites.CTCE;
using PortalCOSIE.Application.Features.Tramites.Queries.DescargarDocumentosPorTramite;

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
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Alumno")]
        public async Task<IActionResult> SolicitarCTCE(SolicitudCtceVM model)
        {
            var userId = User?.FindFirstValue(ClaimTypes.NameIdentifier);

            // Creamos los DTOs de los archivos directamente con Stream
            ArchivoDTO carta = ObtenerDocumentoDTO(model.CartaExposicionMotivos);
            ArchivoDTO identificacion = ObtenerDocumentoDTO(model.Identificacion);
            ArchivoDTO boleta = ObtenerDocumentoDTO(model.BoletaGlobal);
            ArchivoDTO probatorios = ObtenerDocumentoDTO(model.Probatorios);

            // Enviamos comando con DTOs desconectados de HTTP
            await _mediator.Send(new SolicitarCTCECommand(
                userId,
                model.TieneDictamenesAnteriores,
                model.Peticion,
                model.UnidadesReprobadas,
                carta,
                identificacion,
                boleta,
                probatorios
            ));

            return Json(new { success = true, message = "Solicitud enviada con éxito." });
        }

        // === Helper: convierte IFormFile a DocumentoDTO con Stream ===
        private ArchivoDTO ObtenerDocumentoDTO(IFormFile archivo)
        {
            if (archivo == null || archivo.Length == 0)
                throw new ArgumentException("El archivo no puede estar vacío.");

            if (archivo.Length > 3 * 1024 * 1024) // 3 MB
                throw new ArgumentException("El archivo excede el tamaño máximo permitido de 3 MB.");

            return new ArchivoDTO
            {
                Nombre = archivo.FileName,
                Contenido = archivo.OpenReadStream()
            };
        }

        [HttpGet]
        [Authorize(Roles = "Administrador, Personal, Alumno")]
        public async Task<IActionResult> SeguimientoCTCE(int tramiteId)
        {
            var userId = User?.FindFirstValue(ClaimTypes.NameIdentifier);
            var userRole = User?.FindFirstValue(ClaimTypes.Role);

            var tramite = await _mediator.Send(new ObtenerTramiteCTCEPorIdQuery(userId, userRole, tramiteId));
            ViewBag.EstadoDocumento = new SelectList(await _mediator.Send(new ListarEstadoDocumentoQuery()), "Id", "Nombre");

            return View(tramite);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Documento(int Id)
        {
            var userId = User?.FindFirstValue(ClaimTypes.NameIdentifier);
            var rol = User?.FindFirstValue(ClaimTypes.Role);
            var resultado = await _mediator.Send(new DescargarDocumentoQuery(userId, rol, Id));
            // 1. Usamos System.Net.Mime.ContentDisposition para formatear correctamente la cabecera
            var contentDisposition = new System.Net.Mime.ContentDisposition
            {
                FileName = resultado.Nombre,
                Inline = true  // <--- True = abrir pestana, False = descargar
            };

            Response.Headers.Append("Content-Disposition", contentDisposition.ToString());

            // Al no pasar el nombre aquí, ASP.NET respeta la cabecera que acabamos de crear arriba
            return File(resultado.Contenido, resultado.ContentType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Personal")]
        public async Task<IActionResult> TomarTramite(int tramiteId)
        {
            var userId = User?.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _mediator.Send(new AsignarPersonalCommand(userId, tramiteId));
            if (result.Succeeded)
                return Json(new { success = true, message = result.Value });
            return Json(new { success = false, message = result.Errors.FirstOrDefault() });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Personal")]
        public async Task<IActionResult> GuardarValidacion(TramiteCTCE dto)
        {
            //return await _mediator.Send(new );
            return (IActionResult)Task.CompletedTask;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Personal")]
        public async Task<IActionResult> CancelarTramite(int tramiteId, string observaciones)
        {
            var result = await _mediator.Send(new CancelarTramiteCommand(tramiteId, observaciones));
            if (result.Succeeded)
                return Json(new { success = true, message = result.Value });
            return Json(new { success = false, message = result.Errors.FirstOrDefault() });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DescargarDocumentos(int tramiteId)
        {
            var userId = User?.FindFirstValue(ClaimTypes.NameIdentifier);
            var rol = User?.FindFirstValue(ClaimTypes.Role);
            var resultado = await _mediator.Send(new DescargarDocumentosPorTramiteQuery(userId, rol, tramiteId));
            // 1. Usamos System.Net.Mime.ContentDisposition para formatear correctamente la cabecera
            var contentDisposition = new System.Net.Mime.ContentDisposition
            {
                FileName = resultado.Nombre,
                Inline = false  // <--- True = abrir pestana, False = descargar
            };

            Response.Headers.Append("Content-Disposition", contentDisposition.ToString());

            // 2. Retornar el archivo SIN pasar el nombre como tercer parámetro
            // Al no pasar el nombre aquí, ASP.NET respeta la cabecera que acabamos de crear arriba
            return File(resultado.Contenido, resultado.ContentType);
        }
    }
}