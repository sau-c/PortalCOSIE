using PortalCOSIE.Domain.Entities;

namespace PortalCOSIE.Application.Interfaces
{
    public interface ICarreraService
    {
        Task<IEnumerable<Carrera>> ListarCarrerasAsync();
        IEnumerable<object> ListarPeriodos();
        Task<Carrera?> ListarCarreraConUnidadesAsync(string carrera);
        Task<IEnumerable<Carrera?>> ListarUnidadesAsync(string carrera);
        Task EliminarUnidad(int id);

    }
}
