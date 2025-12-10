using PortalCOSIE.Application.DTO.Carreras;
using PortalCOSIE.Domain.Entities.Carreras;

namespace PortalCOSIE.Application.Interfaces
{
    public interface ICarreraService
    {
        Task<IEnumerable<Carrera>> ListarAsync();
        Task<IEnumerable<Carrera>> ListarActivasAsync();
        Task<Carrera?> ListarConUnidadesAsync(int carreraId);
        Task CrearCarreraAsync(string nombre);
        Task EditarCarreraAsync(int id, string nombre);
        Task ToggleCarrrera(int id);

        Task<IEnumerable<UnidadAprendizaje>> ListarUnidadesAsync(int carreraId);
        Task CrearUnidadAsync(UnidadAprendizajeDTO dto);
        Task EditarUnidadAsync(UnidadAprendizajeDTO dto);
        Task ToggleUnidad(int carreraId, string id);
    }
}
