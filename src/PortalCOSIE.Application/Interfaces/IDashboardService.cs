using PortalCOSIE.Application.DTO.Dashboard;

namespace PortalCOSIE.Application.Interfaces
{
    public interface IDashboardService
    {
        Task<ChartDTO> ObtenerEstadoTramitesCTCE(string periodo);
        Task<ChartDTO> ObtenerEstadoDocumentosCTCE(string periodo);
        Task<ChartDTO> ObtenerRolesAlumnos();
        Task<ChartDTO> ObtenerSolicitudesPorCarrera(string periodo);
        Task<ChartDTO> ObtenerUnidadesMasReprobadasPorCarrera(int? carreraId, string periodo);
    }
}