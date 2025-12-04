using PortalCOSIE.Domain.Interfaces;

namespace PortalCOSIE.Domain.Entities.Tramites
{
    /// <summary>
    /// Define las operaciones específicas del repositorio para la entidad Tramite.
    /// Extiende las operaciones CRUD base permitidas con consultas especializadas.
    /// </summary>
    public interface ITramiteRepository : IBaseRepository<Tramite, int>
    {
        /// <summary>
        /// Obtiene todos los trámites con sus datos relacionados completos.
        /// Incluye navegaciones a alumno, personal y estado.
        /// </summary>
        /// <returns>Colección de trámites con toda su información relacionada</returns>
        Task<IEnumerable<Tramite>> ListarConDatosCompletos();
    }
}