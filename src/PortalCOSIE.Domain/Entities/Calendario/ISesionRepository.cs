using PortalCOSIE.Domain.Interfaces;

namespace PortalCOSIE.Domain.Entities.Calendario
{
    public interface ISesionRepository : IBaseRepository<SesionCOSIE>
    {
        Task<SesionCOSIE?> ObtenerConFechasRecepcion(int id);
        Task<IEnumerable<SesionCOSIE>> ListarSesiones(bool filtrarActivos);
    }
}
