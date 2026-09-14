namespace PortalCOSIE.Application.Features.Tramites.DTO;

public sealed record AcusePdfMetadatosDTO(
    string UrlVerificacion,
    string FirmanteSujeto,
    string EmisorCaSujeto,
    string NumeroSerie,
    DateTime VigenteDesde,
    DateTime VigenteHasta,
    int TramiteId,
    string AlumnoNombre,
    string NumeroBoleta,
    DateTime FechaFirmaLocal);