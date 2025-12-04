using PortalCOSIE.Domain.Entities.Carreras;

namespace PortalCOSIE.Domain.Entities.Tramites
{
    /// <summary>
    /// Representa una unidad de aprendizaje que ha sido reprobada por un alumno en un tramite CTCE.
    /// </summary>
    public class UnidadReprobada : BaseEntity<int>
    {
        /// <summary>
        /// Identificador del detalle CTCE asociado a esta unidad reprobada
        /// </summary>
        public int DetalleCTCEId { get; set; }

        /// <summary>
        /// Identificador de la unidad de aprendizaje reprobada
        /// </summary>
        public string UnidadAprendizajeId { get; set; }

        /// <summary>
        /// Período académico en que se cursó originalmente la unidad
        /// </summary>
        public string PeriodoCurso { get; set; }

        /// <summary>
        /// Período académico en que se intentó recursar la unidad
        /// </summary>
        public string PeriodoRecurse { get; set; }

        /// <summary>
        /// Navegación al detalle CTCE que contiene esta unidad reprobada
        /// </summary>
        public DetalleCTCE DetalleCTCE { get; private set; }

        /// <summary>
        /// Navegación a la unidad de aprendizaje reprobada
        /// </summary>
        public UnidadAprendizaje UnidadAprendizaje { get; private set; }

        /// <summary>
        /// Constructor privado requerido para migraciones
        /// </summary>
        private UnidadReprobada() { }

        public UnidadReprobada(string unidadAprendizajeId, string periodoCurso, string periodoRecurse)
        {
            if (string.IsNullOrEmpty(unidadAprendizajeId)) throw new ArgumentException("ID de unidad inválido");
            if (string.IsNullOrEmpty(periodoCurso)) throw new ArgumentException("El periodo de cursado es requerido");
            if (string.IsNullOrEmpty(periodoRecurse)) throw new ArgumentException("El periodo de recurse es requerido");

            this.UnidadAprendizajeId = unidadAprendizajeId;
            PeriodoCurso = periodoCurso;
            PeriodoRecurse = periodoRecurse;
        }
    }
}