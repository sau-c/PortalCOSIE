using PortalCOSIE.Domain.Entities;

namespace PortalCOSIE.Application.Interfaces
{
    public interface IBitacoraService
    {
        Task<IEnumerable<Bitacora>> ListarBitacoraAsync();
        Task RegistrarAsync(string identityUserId, string accion, string entidad, string? entidadId, string ip, string userAgent);
    }

}