using PortalCOSIE.Application.DTO.Dashboard;

namespace PortalCOSIE.Application.Interfaces
{
    public interface IDashboardService
    {
        Task<TarjetasDTO> ObtenerTarjetasContables();
        Task<ChartDTO> ObtenerSolicitudesPorCarrera(string periodo);
        Task<ChartDTO> ObtenerUnidadesMasReprobadasPorCarrera(int? carreraId, string periodo);
    }
}