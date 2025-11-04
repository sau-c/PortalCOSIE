using PortalCOSIE.Domain.Entities.Calendario;

namespace PortalCOSIE.Domain.Interfaces
{
    public interface ISesionRepository : IBaseRepository<SesionCOSIE>
    {
        Task<SesionCOSIE?> ObtenerConFechasRecepcion(int id);
        Task<IEnumerable<SesionCOSIE>> ListarSesiones(bool filtrarActivos);
    }
}
