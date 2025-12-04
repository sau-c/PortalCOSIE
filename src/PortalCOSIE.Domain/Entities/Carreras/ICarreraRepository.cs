using PortalCOSIE.Domain.Interfaces;

namespace PortalCOSIE.Domain.Entities.Carreras
{
    /// <summary>
    /// Define operaciones específicas del repositorio para la entidad Carrera.
    /// Extiende las operaciones de BaseRepository.
    /// </summary>
    public interface ICarreraRepository : IBaseRepository<Carrera, int>
    {
        /// <summary>
        /// Obtiene una carrera completa con todas sus unidades de aprendizaje cargadas.
        /// </summary>
        Task<Carrera> ObtenerCarreraConUnidadesAsync(string carrera);

        /// <summary>
        /// Lista todas las unidades de aprendizaje pertenecientes a una carrera específica.
        /// </summary>
        Task<IEnumerable<UnidadAprendizaje>> ListarUnidadesPorCarreraAsync(string carrera);
    }
}