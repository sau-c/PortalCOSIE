using PortalCOSIE.Domain.Entities.Carreras;
using PortalCOSIE.Domain.Enums;

namespace PortalCOSIE.Application.Interfaces
{
    public interface ICarreraService
    {
        Task<IEnumerable<Carrera>> ListarAsync();
        Task<IEnumerable<Carrera>> ListarActivasAsync();
        Task<Carrera?> ListarConUnidadesAsync(string carrera);
        Task CrearCarreraAsync(string nombre);
        Task EditarCarreraAsync(int id, string nombre);
        Task ToggleCarrrera(int id);

        Task<IEnumerable<UnidadAprendizaje>> ListarUnidadesAsync(string carrera);
        Task EliminarUnidad(int unidadId);
        Task CrearUnidadAsync(string nombre, int carreraId, Semestre semestre);
        Task EditarUnidadAsync(int id, string nombre, Semestre semestre);
        Task ToggleUnidad(int unidadId);
    }
}
