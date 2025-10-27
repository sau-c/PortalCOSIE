namespace PortalCOSIE.Domain.Entities
{
    public class Tramite : BaseEntity
    {
        public int EstadoId { get; private set; }
        public int AlumnoId { get; private set; }
        public int PersonalId { get; private set; }
        public int TipoId { get; private set; }
        public DateTime FechaSolicitud { get; private set; }
        public DateTime? FechaConclusion { get; private set; }

        // Propiedades de navegación
        public EstadoTramite EstadoTramite { get; private set; }
        public TipoTramite TipoTramite { get; private set; }
        public Alumno Alumno { get; private set; }
        public Personal Personal { get; private set; }

        private readonly List<Documento> _documentos = new();
        public IReadOnlyCollection<Documento> Documentos => _documentos.AsReadOnly();

        // Constructor privado (EF Core)
        private Tramite() { }

        // Constructor de dominio
        public Tramite(int alumnoId, int personalId, int tipoId, int estadoId)
        {
            if (alumnoId <= 0)
                throw new DomainException("El AlumnoId debe ser válido.");
            if (personalId <= 0)
                throw new DomainException("El PersonalId debe ser válido.");
            if (tipoId <= 0)
                throw new DomainException("El TipoId debe ser válido.");
            if (estadoId <= 0)
                throw new DomainException("El EstadoId debe ser válido.");

            AlumnoId = alumnoId;
            PersonalId = personalId;
            TipoId = tipoId;
            EstadoId = estadoId;
            FechaSolicitud = DateTime.UtcNow;
        }
        
        public void AgregarDocumento(Documento documento)
        {
            if (documento == null)
                throw new DomainException("El documento no puede ser nulo.");
            _documentos.Add(documento);
        }

        /// <summary>
        /// Cambia el estado del trámite (por ejemplo: En Proceso → Concluido)
        /// </summary>
        public void CambiarEstado(EstadoTramite nuevoEstado)
        {
            if (nuevoEstado == null)
                throw new DomainException("El nuevo estado no puede ser nulo.");

            EstadoTramite = nuevoEstado;
            EstadoId = nuevoEstado.Id;
        }

        /// <summary>
        /// Marca el trámite como concluido.
        /// </summary>
        public void Concluir()
        {
            if (FechaConclusion != null)
                throw new DomainException("El trámite ya fue concluido.");

            FechaConclusion = DateTime.UtcNow;
        }
    }
}
