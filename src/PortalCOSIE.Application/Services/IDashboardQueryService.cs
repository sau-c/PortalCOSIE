using PortalCOSIE.Application.Features.Dashboard.DTO;

namespace PortalCOSIE.Application.Services
{
    public interface IDashboardQueryService
    {
        Task<ChartDTO> ObtenerEstadoTramitesCTCE(string periodo);
        Task<ChartDTO> ObtenerEstadoDocumentosCTCE(string periodo);
        Task<ChartDTO> ObtenerRolesAlumnos();
        Task<ChartDTO> ObtenerSolicitudesPorCarrera(string periodo);
        Task<ChartDTO> ObtenerUnidadesMasReprobadasPorCarrera(int? carreraId, string periodo);
    }
}