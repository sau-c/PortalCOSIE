using PortalCOSIE.Domain.Entities.Tramites;

namespace PortalCOSIE.Application.Interfaces
{
    public interface IEstadoDocumentoService
    {
        Task<IEnumerable<EstadoDocumento>> ListarEstadosDocumentoActivos();
        Task<IEnumerable<EstadoDocumento>> ListarEstadosDocumento();
        Task CrearEstadoDocumento(string nombre);
        Task EditarEstadoDocumento(int id, string nombre);
        Task ToggleEstadoDocumento(int id);
    }
}
