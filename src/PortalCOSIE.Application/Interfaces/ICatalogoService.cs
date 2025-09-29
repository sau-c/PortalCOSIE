using PortalCOSIE.Domain.Entities;

namespace PortalCOSIE.Application.Interfaces
{
    public interface ICatalogoService
    {
        Task<IEnumerable<Carrera>> ListarCarrerasAsync();
        IEnumerable<object> ListarPeriodos();
        Task<IEnumerable<UnidadAprendizaje?>> ListarUnidadesAprendizajeAsync(int id);
    }
}
