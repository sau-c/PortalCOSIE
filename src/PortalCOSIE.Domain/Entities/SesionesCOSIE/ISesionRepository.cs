using PortalCOSIE.Domain.Interfaces;

namespace PortalCOSIE.Domain.Entities.SesionesCOSIE
{
    /// <summary>
    /// Define operaciones específicas del repositorio para la entidad SesionCOSIE.
    /// Extiende las operaciones de BaseRepository.
    /// </summary>
    public interface ISesionRepository : IBaseRepository<SesionCOSIE, int>
    {
        /// <summary>
        /// Obtiene una sesión específica con todas sus fechas de recepción.
        /// </summary>
        Task<SesionCOSIE?> ObtenerConFechasRecepcion(int id);

        /// <summary>
        /// Lista sesiones del calendario académico con opción de filtrado para la propiedad IsDeleted.
        /// </summary>
        Task<IEnumerable<SesionCOSIE>> ListarSesiones(bool IncluirEliminados);
    }
}