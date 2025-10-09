using PortalCOSIE.Domain.Entities;

namespace PortalCOSIE.Application.Interfaces
{
    public interface ICarreraService
    {
        Task<IEnumerable<Carrera>> ListarCarrerasAsync();
        Task CrearCarreraAsync(string nombre);
        Task EditarCarreraAsync(int id, string nombre);
        Task EliminarCarrrera(int id);

        Task<Carrera?> ListarCarreraConUnidadesAsync(string carrera);
        Task<IEnumerable<Carrera?>> ListarUnidadesAsync(string carrera);
        Task EliminarUnidad(int id);
        IEnumerable<object> ListarPeriodos();

    }
}
