using PortalCOSIE.Domain.Entities.Bitacoras;

namespace PortalCOSIE.Application.Interfaces
{
    public interface IBitacoraService
    {
        Task<IEnumerable<EntradaBitacora>> ListarBitacoraAsync();
    }

}