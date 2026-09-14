using PortalCOSIE.Application.Features.Tramites.DTO;

namespace PortalCOSIE.Application.Services.Crypto;

public interface IPdfAcuseService
{
    byte[] GenerarPanelAcusePublico(
        byte[] pdfFirmado,
        AcusePdfMetadatosDTO metadatos,
        string firmaCmsBase64);
}