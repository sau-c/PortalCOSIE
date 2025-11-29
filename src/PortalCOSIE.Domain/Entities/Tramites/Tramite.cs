using PortalCOSIE.Domain.Entities.Documentos;
using PortalCOSIE.Domain.Entities.Usuarios;

namespace PortalCOSIE.Domain.Entities.Tramites
{
    public abstract class Tramite : BaseEntity
    {
        public int EstadoTramiteId { get; private set; }
        public int AlumnoId { get; private set; }
        public int? PersonalId { get; private set; }
        public int TipoTramiteId { get; private set; }
        
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
        protected Tramite() { }

        // Constructor de dominio
        protected Tramite(int alumnoId, int tipoId)
        {
            if (alumnoId <= 0) throw new DomainException("El AlumnoId debe ser válido.");
            if (tipoId <= 0)
                throw new DomainException("El TipoId debe ser válido.");
            
            AlumnoId = alumnoId;
            TipoTramiteId = tipoId;
            EstadoTramiteId = EstadoTramite.Solicitado.Id; // podria usar enum: EstadosTramite.Solicitado
            FechaSolicitud = DateTime.UtcNow;
        }

        public void AsignarPersonal(int personalId)
        {
            if (personalId <= 0) throw new DomainException("Personal inválido.");
            PersonalId = personalId;
            EstadoTramiteId = 2;
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
            EstadoTramiteId = nuevoEstado.Id;
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
