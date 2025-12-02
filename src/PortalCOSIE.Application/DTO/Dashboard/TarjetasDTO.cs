namespace PortalCOSIE.Application.DTO.Dashboard
{
    public record TarjetasDTO(
        int TotalAlumnos,
        int TotalAlumnosActivos,
        int TramitesSolicitados,
        int TramitesEnRevision,
        int TramitesConcluidos,
        int DocumentosConErrores,
        int DocumentosValidados,
        int DocumentosEnRevision
        );
}