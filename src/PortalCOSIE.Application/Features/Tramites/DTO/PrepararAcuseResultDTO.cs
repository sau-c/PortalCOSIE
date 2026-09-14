namespace PortalCOSIE.Application.Features.Tramites.DTO;

public sealed record PrepararAcuseResultDTO(
    string Token,
    string NombreArchivo,
    string PdfBase64);