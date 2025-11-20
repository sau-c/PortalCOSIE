using PortalCOSIE.Domain.Entities;

namespace PortalCOSIE.Application.Interfaces
{
    public interface IBitacoraService
    {
        Task<IEnumerable<EntradaBitacora>> ListarBitacoraAsync();
    }

}