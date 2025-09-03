using PortalCOSIE.Domain.Entities;

namespace PortalCOSIE.Application.Interfaces
{
    public interface ICatalogoService
    {
        Task<IEnumerable<Carrera>> ListarCarrerasAsync();
        Task<IEnumerable<UnidadAprendizaje>> ListarUnidadesAprendizajeAsync();
    }
}
