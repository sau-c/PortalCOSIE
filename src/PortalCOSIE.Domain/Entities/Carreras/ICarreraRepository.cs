using PortalCOSIE.Domain.Interfaces;

namespace PortalCOSIE.Domain.Entities.Carreras
{
    public interface ICarreraRepository : IBaseRepository<Carrera>
    {
        Task<Carrera> ObtenerCarreraConUnidadesAsync(string carrera);
        Task<IEnumerable<UnidadAprendizaje>> ListarUnidadesPorCarreraAsync(string carrera);
    }
}
