using PortalCOSIE.Domain.Entities.Carreras;

namespace PortalCOSIE.Domain.Interfaces
{
    public interface ICarreraRepository : IBaseRepository<Carrera>
    {
        Task<Carrera> ObtenerCarreraConUnidadesAsync(string carrera);
        Task<IEnumerable<UnidadAprendizaje>> ListarUnidadesPorCarreraAsync(string carrera);
    }
}
