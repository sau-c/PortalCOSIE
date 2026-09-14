using PortalCOSIE.Application.Services;
using PortalCOSIE.Application.Services.Crypto;
using PortalCOSIE.Domain.Entities.Documentos;
using PortalCOSIE.Domain.Entities.Tramites;

namespace PortalCOSIE.Application.Features.Tramites.Services;

public class AcusePdfPublicacionService
{
    private readonly IPdfAcuseService _pdfAcuse;
    private readonly ICertificadoCaService _certificadoCa;

    public AcusePdfPublicacionService(
        IPdfAcuseService pdfAcuse,
        ICertificadoCaService certificadoCa)
    {
        _pdfAcuse = pdfAcuse;
        _certificadoCa = certificadoCa;
    }

    public static bool RequierePanelPublico(Documento documento)
        => documento.TipoDocumentoId == TipoDocumento.DictamenCTCE.Id
           && documento.FirmaElectronica != null
           && !string.IsNullOrWhiteSpace(documento.FirmaElectronica.TokenVerificacion);

    public async Task<byte[]> AplicarPanelPublicoAsync(
        Documento documento,
        byte[] pdfOriginal,
        string baseUrl,
        Tramite? tramite = null)
    {
        if (!RequierePanelPublico(documento))
            return pdfOriginal;

        if (string.IsNullOrWhiteSpace(baseUrl))
            throw new InvalidOperationException("La URL base de la aplicación no está configurada.");

        var firma = documento.FirmaElectronica!;
        var certificado = firma.Certificado
            ?? throw new InvalidOperationException("El certificado del firmante no está disponible.");
        var tramiteDocumento = tramite ?? documento.Tramite
            ?? throw new InvalidOperationException("El trámite del documento no está disponible.");

        var ca = await _certificadoCa.ObtenerCaAsync()
            ?? throw new InvalidOperationException("No hay certificado de la CA PortalCOSIE registrado.");

        var metadatos = AcuseMetadatosFactory.Crear(
            tramiteDocumento,
            certificado,
            ca.Sujeto,
            baseUrl,
            firma.TokenVerificacion!.Trim(),
            firma.FechaFirmaUtc);

        var firmaBase64 = Convert.ToBase64String(firma.FirmaCms);
        return _pdfAcuse.GenerarPanelAcusePublico(pdfOriginal, metadatos, firmaBase64);
    }
}