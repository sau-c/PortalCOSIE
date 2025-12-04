using PortalCOSIE.Domain.Entities.Documentos;
using PortalCOSIE.Domain.Entities.Usuarios;

namespace PortalCOSIE.Domain.Entities.Tramites
{
    /// <summary>
    /// Clase base abstracta que representa un trámite académico en el sistema.
    /// Define el comportamiento común para todos los tipos de trámites.
    /// </summary>
    public abstract class Tramite : BaseEntity<int>
    {
        /// <summary>Identificador del estado actual del trámite</summary>
        public int EstadoTramiteId { get; private set; }

        /// <summary>Identificador del alumno solicitante</summary>
        public int AlumnoId { get; private set; }

        /// <summary>Identificador del personal asignado</summary>
        public int? PersonalId { get; private set; }

        /// <summary>Identificador del tipo de trámite</summary>
        public int TipoTramiteId { get; private set; }

        /// <summary>Fecha de solicitud del trámite</summary>
        public DateTime FechaSolicitud { get; private set; }

        /// <summary>Fecha de conclusión del trámite (cuando aplica)</summary>
        public DateTime? FechaConclusion { get; private set; }

        // Propiedades de navegación
        public EstadoTramite EstadoTramite { get; private set; }
        public TipoTramite TipoTramite { get; private set; }
        public Alumno Alumno { get; private set; }
        public Personal Personal { get; private set; }

        private readonly List<Documento> _documentos = new();
        public IReadOnlyCollection<Documento> Documentos => _documentos.AsReadOnly();

        /// <summary>Constructor privado para migraciones</summary>
        protected Tramite() { }

        /// <summary>
        /// Constructor principal para crear un nuevo trámite
        /// </summary>
        /// <param name="alumnoId">ID del alumno solicitante</param>
        /// <param name="tipoId">ID del tipo de trámite</param>
        /// <exception cref="DomainException">Cuando los parámetros son inválidos</exception>
        protected Tramite(int alumnoId, int tipoId)
        {
            if (alumnoId <= 0) throw new DomainException("El AlumnoId debe ser válido.");
            if (tipoId <= 0) throw new DomainException("El TipoId debe ser válido.");

            AlumnoId = alumnoId;
            TipoTramiteId = tipoId;
            EstadoTramiteId = EstadoTramite.Solicitado.Id;
            FechaSolicitud = DateTime.UtcNow;
        }

        /// <summary>Asigna personal responsable para la revisión del trámite</summary>
        public void AsignarPersonal(int personalId)
        {
            if (personalId <= 0) throw new DomainException("Personal inválido.");
            PersonalId = personalId;
            EstadoTramiteId = EstadoTramite.EnRevision.Id;
        }

        /// <summary>Agrega un documento al trámite</summary>
        public void AgregarDocumento(Documento documento)
        {
            if (documento == null) throw new DomainException("El documento no puede ser nulo.");
            _documentos.Add(documento);
        }

        /// <summary>Actualiza el estado del trámite</summary>
        public void CambiarEstado(EstadoTramite nuevoEstado)
        {
            if (nuevoEstado == null) throw new DomainException("El nuevo estado no puede ser nulo.");
            EstadoTramite = nuevoEstado;
            EstadoTramiteId = nuevoEstado.Id;
        }

        /// <summary>Marca el trámite como concluido y registra la fecha de conclusión</summary>
        public void Concluir()
        {
            if (FechaConclusion != null) throw new DomainException("El trámite ya fue concluido.");
            FechaConclusion = DateTime.UtcNow;
            EstadoTramiteId = EstadoTramite.Concluido.Id;
        }
    }
}