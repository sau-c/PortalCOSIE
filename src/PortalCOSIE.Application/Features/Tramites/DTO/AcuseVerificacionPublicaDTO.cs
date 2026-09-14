namespace PortalCOSIE.Application.Features.Tramites.DTO;

public sealed record AcuseVerificacionPublicaDTO(
    bool FirmaValida,
    string Mensaje,
    int TramiteId,
    string AlumnoNombre,
    string NumeroBoleta,
    string Carrera,
    string Firmante,
    DateTime FechaFirmaUtc,
    DateTime VigenteHasta,
    string PeriodoSolicitud,
    DateTime FechaSolicitud);