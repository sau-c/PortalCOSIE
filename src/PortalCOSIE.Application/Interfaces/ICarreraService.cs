using PortalCOSIE.Domain.Entities;

namespace PortalCOSIE.Application.Interfaces
{
    public interface ICarreraService
    {
        Task<IEnumerable<Carrera>> ListarCarrerasAsync();
        Task<IEnumerable<Carrera>> ListarTodoCarrerasAsync();
        Task CrearCarreraAsync(string nombre);
        Task EditarCarreraAsync(int id, string nombre);
        Task ToggleCarrrera(int id);

        Task<Carrera?> ListarCarreraConUnidadesAsync(string carrera);
        Task<IEnumerable<Carrera?>> ListarUnidadesAsync(string carrera);
        Task EliminarUnidad(int id);
    }
}
