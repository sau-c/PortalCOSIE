using PortalCOSIE.Domain.Entities.Carreras;

namespace PortalCOSIE.Domain.Entities.Tramites
{
    public class UnidadReprobada : BaseEntity
    {
        public int DetalleCTCEId { get; set; }
        public int UnidadAprendizajeId { get; set; }
        public string PeriodoCurso { get; set; }
        public string PeriodoRecurse { get; set; }

        // Navegación (EF Core)
        public DetalleCTCE DetalleCTCE { get; private set; }
        public UnidadAprendizaje UnidadAprendizaje { get; private set; }

        // Constructor para EF Core
        private UnidadReprobada() { }

        // Constructor público para tu lógica de negocio
        public UnidadReprobada(int unidadId, string periodoCurso, string periodoRecurse)
        {
            if (unidadId <= 0) throw new ArgumentException("ID de unidad inválido");
            if (string.IsNullOrEmpty(periodoCurso)) throw new ArgumentException("El periodo de cursado es requerido");

            UnidadAprendizajeId = unidadId;
            PeriodoCurso = periodoCurso;
            PeriodoRecurse = periodoRecurse ?? string.Empty; // Puede ser opcional dependiendo de tu regla
        }
    }
}
