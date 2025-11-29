using PortalCOSIE.Domain.Interfaces;

namespace PortalCOSIE.Domain.Entities.Tramites
{
    public interface ITramiteRepository : IBaseRepository<Tramite>
    {
        Task<IEnumerable<Tramite>> ListarConDatosCompletos();
    }
}
