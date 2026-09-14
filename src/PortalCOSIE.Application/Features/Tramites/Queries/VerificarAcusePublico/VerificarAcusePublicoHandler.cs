using PortalCOSIE.Application.Features.Tramites.DTO;
using PortalCOSIE.Application.Services;
using PortalCOSIE.Application.Services.Crypto;
using PortalCOSIE.Application.Services.Query;
using PortalCOSIE.Application.Services.Storage;

namespace PortalCOSIE.Application.Features.Tramites.Queries.VerificarAcusePublico;

public class VerificarAcusePublicoHandler : IRequestHandler<VerificarAcusePublicoQuery, AcuseVerificacionPublicaDTO?>
{
    private readonly IUsuarioQueryService _usuarioQuery;
    private readonly IStorageService _storage;
    private readonly IFirmaVerificacionService _firmaVerificacion;
    private readonly ICertificadoCaService _certificadoCa;

    public VerificarAcusePublicoHandler(
        IUsuarioQueryService usuarioQuery,
        IStorageService storage,
        IFirmaVerificacionService firmaVerificacion,
        ICertificadoCaService certificadoCa)
    {
        _usuarioQuery = usuarioQuery;
        _storage = storage;
        _firmaVerificacion = firmaVerificacion;
        _certificadoCa = certificadoCa;
    }

    public async Task<AcuseVerificacionPublicaDTO?> Handle(VerificarAcusePublicoQuery query)
    {
        if (string.IsNullOrWhiteSpace(query.Token))
            return null;

        var firma = await _usuarioQuery.ObtenerFirmaAcusePorTokenAsync(query.Token.Trim());
        if (firma?.Documento?.Tramite?.Alumno == null || firma.Certificado == null)
            return null;

        var vigencia = _firmaVerificacion.ValidarVigenciaCertificado(firma.Certificado);
        var caDer = await _certificadoCa.ObtenerCaCertificadoDerAsync();

        bool firmaValida = false;
        string mensaje;

        if (!vigencia.Succeeded)
        {
            mensaje = vigencia.Errors.FirstOrDefault() ?? "El certificado no está vigente.";
        }
        else if (caDer == null)
        {
            mensaje = "No se encontró la autoridad certificadora PortalCOSIE.";
        }
        else if (string.IsNullOrWhiteSpace(firma.Documento.Ruta))
        {
            mensaje = "No se encontró el documento firmado asociado al acuse.";
        }
        else
        {
            await using var contenido = await _storage.DownloadAsync(firma.Documento.Ruta);
            using var certificadoStream = new MemoryStream(firma.Certificado.CertificadoDer);
            var verificacion = _firmaVerificacion.VerificarFirmaCms(
                contenido,
                firma.FirmaCms,
                certificadoStream,
                caDer);

            firmaValida = verificacion.Succeeded;
            mensaje = firmaValida
                ? "La firma electrónica del acuse es válida y fue emitida por PortalCOSIE."
                : verificacion.Errors.FirstOrDefault() ?? "La firma electrónica no es válida.";
        }

        var tramite = firma.Documento.Tramite;
        var alumno = tramite.Alumno;

        return new AcuseVerificacionPublicaDTO(
            firmaValida,
            mensaje,
            tramite.Id,
            $"{alumno.Nombre} {alumno.ApellidoPaterno} {alumno.ApellidoMaterno}".Trim(),
            alumno.NumeroBoleta,
            alumno.Carrera.Nombre,
            firma.Certificado.Sujeto,
            firma.FechaFirmaUtc,
            firma.Certificado.VigenteHasta,
            tramite.PeriodoSolicitud,
            tramite.FechaSolicitud);
    }
}