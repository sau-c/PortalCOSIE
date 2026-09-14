using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using PortalCOSIE.Web.Models;
using PortalCOSIE.Application.Features.Carreras.Queries.ListarUnidades;
using PortalCOSIE.Application.Features.PeriodosConfig.Queries.ListarPeriodos;
using PortalCOSIE.Application.Features.Tramites.Commands.AsignarPersonal;
using PortalCOSIE.Application.Features.Tramites.Commands.SolicitarCTCE;
using PortalCOSIE.Application.Features.Tramites.DTO;
using PortalCOSIE.Application.Features.Tramites.Queries.ListarEstadosTramite;
using PortalCOSIE.Application.Features.Tramites.Queries.ListarTramites;
using PortalCOSIE.Application.Features.Tramites.Queries.DescargarDocumento;
using PortalCOSIE.Application.Features.Tramites.Queries.ObtenerTramiteCTCEPorId;
using PortalCOSIE.Application.Features.Tramites.Commands.Cancelar;
using PortalCOSIE.Application.Features.Tramites.Queries.DescargarDocumentosPorTramite;
using PortalCOSIE.Application.Features.Tramites.Commands.Revision;
using PortalCOSIE.Application.Features.Tramites.Commands.Corregir;
using PortalCOSIE.Application.Features.Tramites.Commands.Concluir;
using PortalCOSIE.Application.Features.Tramites.Commands.PrepararAcuse;
using PortalCOSIE.Application.Features.Tramites.Queries.VerificarDocumento;
using PortalCOSIE.Application.Features.Usuarios.Queries.ObtenerCertificadoFirma;
using PortalCOSIE.Web.Extensions;

namespace PortalCOSIE.Web.Controllers
{
    public class TramiteController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IConfiguration _configuration;

        public TramiteController(IMediator mediator, IConfiguration configuration)
        {
            _mediator = mediator;
            _configuration = configuration;
        }

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
            await CargarCertificadoFirmaAsync(userId);
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Alumno")]
        public async Task<IActionResult> SolicitarCTCE(SolicitudCtceVM model)
        {
            var userId = User?.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _mediator.Send(new SolicitarCTCECommand(
                userId,
                model.TieneDictamenesAnteriores,
                model.Peticion,
                model.UnidadesReprobadas,
                model.CartaExposicionMotivos.ToDocumentoFirmado(model.FirmaCartaExposicionMotivos)!,
                model.Identificacion.ToDocumentoFirmado(model.FirmaIdentificacion)!,
                model.BoletaGlobal.ToDocumentoFirmado(model.FirmaBoletaGlobal)!,
                model.Probatorios.ToDocumentoFirmado(model.FirmaProbatorios)!
            ));

            if (!result.Succeeded)
                return Json(new { success = false, message = result.Errors.FirstOrDefault() });

            return Json(new { success = true, message = "Solicitud enviada con �xito." });
        }

        [HttpPost]
        [Authorize(Roles = "Alumno")]
        public async Task<IActionResult> Corregir(CorregirCtceVM model)
        {
            string userId = User?.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _mediator.Send(
                new CorregirTramiteCommand(
                    userId,
                    model.Id,
                    model.CartaExposicionMotivos.ToDocumentoFirmado(model.FirmaCartaExposicionMotivos),
                    model.Identificacion.ToDocumentoFirmado(model.FirmaIdentificacion),
                    model.BoletaGlobal.ToDocumentoFirmado(model.FirmaBoletaGlobal),
                    model.Probatorios.ToDocumentoFirmado(model.FirmaProbatorios)
                ));

            if (result.Succeeded)
                return Json(new { success = true, message = result.Value });
            return Json(new { success = false, message = result.Errors.FirstOrDefault() });
        }

        // === Helper: convierte IFormFile a ArchivoDTO asociado a un TipoDocumento ===
        private ArchivoDTO ObtenerArchivoDTO(IFormFile archivo)
        {
            if (archivo == null || archivo.Length == 0)
                return null;

            if (archivo.Length > 3 * 1024 * 1024) // 3 MB
                throw new ArgumentException("El archivo excede el tama�o m�ximo permitido de 3 MB.");

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
            await CargarCertificadoFirmaAsync(userId);
            if (userRole == "Alumno")
                return View("SeguimientoCTCE_Alumno", tramite);
            return View("SeguimientoCTCE_Personal", tramite);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Documento(int Id)
        {
            var userId = User?.FindFirstValue(ClaimTypes.NameIdentifier);
            var rol = User?.FindFirstValue(ClaimTypes.Role);
            var baseUrl = _configuration["App:BaseUrl"] ?? $"{Request.Scheme}://{Request.Host}";
            var resultado = await _mediator.Send(new DescargarDocumentoQuery(userId, rol, Id, baseUrl));
            // 1. Usamos System.Net.Mime.ContentDisposition para formatear correctamente la cabecera
            var contentDisposition = new System.Net.Mime.ContentDisposition
            {
                FileName = resultado.Nombre,
                Inline = true  // <--- True = abrir pestana, False = descargar
            };

            Response.Headers.Append("Content-Disposition", contentDisposition.ToString());

            // Al no pasar el nombre aqu�, ASP.NET respeta la cabecera que acabamos de crear arriba
            return File(resultado.Contenido, resultado.ContentType);
        }

        [HttpPost]
        [Authorize(Roles = "Personal")]
        public async Task<IActionResult> Tomar(int tramiteId)
        {
            var userId = User?.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _mediator.Send(new AsignarPersonalCommand(userId, tramiteId));
            if (result.Succeeded)
                return Json(new { success = true, message = result.Value });
            return Json(new { success = false, message = result.Errors.FirstOrDefault() });
        }

        [HttpPost]
        [Authorize(Roles = "Personal")]
        public async Task<IActionResult> Revisar(int id, List<DocumentoDTO> documentosList, string observaciones)
        {
            var userId = User?.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _mediator.Send(new RevisarTramiteCommand(userId, id, documentosList, observaciones));
            if (result.Succeeded)
                return Json(new { success = true, message = result.Value });
            return Json(new { success = false, message = result.Errors.FirstOrDefault() });
        }

        [HttpPost]
        [Authorize(Roles = "Personal")]
        public async Task<IActionResult> Cancelar(int tramiteId, string observaciones)
        {
            var userId = User?.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _mediator.Send(new CancelarTramiteCommand(userId, tramiteId, observaciones));
            if (result.Succeeded)
                return Json(new { success = true, message = result.Value });
            return Json(new { success = false, message = result.Errors.FirstOrDefault() });
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> PrepararAcuse(ConcluirVM model)
        {
            var userId = User?.FindFirstValue(ClaimTypes.NameIdentifier);
            var archivo = model.Acuse?.ToDocumentoFirmado(null, requiereFirma: false);
            if (archivo == null)
                return Json(new { success = false, message = "El dictamen es obligatorio." });

            var baseUrl = _configuration["App:BaseUrl"] ?? $"{Request.Scheme}://{Request.Host}";
            var result = await _mediator.Send(new PrepararAcuseCommand(userId, model.TramiteId, archivo, baseUrl));
            if (!result.Succeeded)
                return Json(new { success = false, message = result.Errors.FirstOrDefault() });

            return Json(new
            {
                success = true,
                token = result.Value.Token,
                nombreArchivo = result.Value.NombreArchivo,
                pdfBase64 = result.Value.PdfBase64
            });
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Concluir(ConcluirVM model)
        {
            var userId = User?.FindFirstValue(ClaimTypes.NameIdentifier);
            var command = new ConcluirTramiteCommand(
                userId,
                model.TramiteId,
                model.TokenAcuse,
                model.Acuse.ToDocumentoFirmado(model.FirmaAcuse)!
            );

            var result = await _mediator.Send(command);
            if (result.Succeeded)
                return Json(new { success = true, message = result.Value });
            return Json(new { success = false, message = result.Errors.FirstOrDefault() });
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> DescargarDocumentos(int tramiteId)
        {
            var userId = User?.FindFirstValue(ClaimTypes.NameIdentifier);
            var rol = User?.FindFirstValue(ClaimTypes.Role);
            var baseUrl = _configuration["App:BaseUrl"] ?? $"{Request.Scheme}://{Request.Host}";
            var resultado = await _mediator.Send(new DescargarDocumentosPorTramiteQuery(userId, rol, tramiteId, baseUrl));
            // 1. Usamos System.Net.Mime.ContentDisposition para formatear correctamente la cabecera
            var contentDisposition = new System.Net.Mime.ContentDisposition
            {
                FileName = resultado.Nombre,
                Inline = false  // <--- True = abrir pestana, False = descargar
            };

            Response.Headers.Append("Content-Disposition", contentDisposition.ToString());

            // 2. Retornar el archivo SIN pasar el nombre como tercer par�metro
            // Al no pasar el nombre aqu�, ASP.NET respeta la cabecera que acabamos de crear arriba
            return File(resultado.Contenido, resultado.ContentType);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> CertificadoFirma()
        {
            var userId = User?.FindFirstValue(ClaimTypes.NameIdentifier);
            var cert = await _mediator.Send(new ObtenerCertificadoFirmaQuery(userId));
            if (cert == null)
                return Json(new { success = false, message = "No tienes un certificado registrado." });
            if (!cert.EsValidoParaFirma)
                return Json(new { success = false, message = "El certificado registrado no es válido. El administrador debe cargar el archivo .cer correcto en la base de datos." });

            return Json(new { success = true, certificadoDerBase64 = cert.CertificadoDerBase64 });
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> VerificarDocumento(int id)
        {
            var userId = User?.FindFirstValue(ClaimTypes.NameIdentifier);
            var rol = User?.FindFirstValue(ClaimTypes.Role);
            var result = await _mediator.Send(new VerificarDocumentoQuery(userId, rol, id));

            if (result.Succeeded)
                return Json(new { success = true, message = result.Value });
            return Json(new { success = false, message = result.Errors.FirstOrDefault() });
        }

        private async Task CargarCertificadoFirmaAsync(string? userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                ViewBag.CertificadoFirma = null;
                return;
            }

            ViewBag.CertificadoFirma = await _mediator.Send(new ObtenerCertificadoFirmaQuery(userId));
        }
    }
}