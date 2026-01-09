using PortalCOSIE.Domain.Entities.Documentos;
using PortalCOSIE.Domain.Entities.Usuarios;
using PortalCOSIE.Domain.SharedKernel;

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
        /// <summary>Periodo de solicitud del trámite (2020/1, 2023/2, etc...)</summary>
        public string PeriodoSolicitud { get; private set; }

        /// <summary>Fecha de conclusión del trámite (cuando aplica)</summary>
        public DateTime? FechaConclusion { get; private set; }
        /// <summary>Comentario generico del trámite</summary>
        public string? Observaciones { get; private set; }
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
        protected Tramite(int alumnoId, int tipoId, string periodoSolicitud)
        {
            if (alumnoId <= 0) throw new DomainException("El AlumnoId debe ser válido.");
            if (tipoId <= 0) throw new DomainException("El TipoId debe ser válido.");
            if (string.IsNullOrWhiteSpace(periodoSolicitud)) throw new DomainException("El periodo de solicitud no puede estar vacío.");

            AlumnoId = alumnoId;
            TipoTramiteId = tipoId;
            EstadoTramiteId = EstadoTramite.Solicitado.Id;
            FechaSolicitud = DateTime.Now;
            PeriodoSolicitud = periodoSolicitud;
        }

        /// <summary>Asigna personal responsable para la revisión del trámite</summary>
        public void AsignarPersonal(int personalId)
        {
            if (personalId <= 0) throw new DomainException("Personal inválido.");
            PersonalId = personalId;
            EstadoTramiteId = EstadoTramite.EnRevision.Id;
        }

        public bool PerteneceAAlumno(int alumnoId)
            => AlumnoId == alumnoId;

        public bool PuedeSerAtendidoPor(int personalId)
            => PersonalId == personalId;

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

            if (!EstadoTramite.PuedeTransicionarA(nuevoEstado))
                throw new DomainException(
                    $"Trámite no puede cambiar el estado de '{EstadoTramite.Nombre}' a '{nuevoEstado.Nombre}'.");

            EstadoTramiteId = nuevoEstado.Id;

            if (nuevoEstado.EsFinal())
                FechaConclusion = DateTime.Now;
        }

        public void AgregarObservaciones(string observaciones)
        {
            Observaciones = observaciones;
        }
    }
}