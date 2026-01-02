using PortalCOSIE.Domain.Entities.Tramites;
using PortalCOSIE.Domain.SharedKernel;

namespace PortalCOSIE.Domain.Entities.Documentos
{
    /// <summary>
    /// Representa un documento adjunto a un trámite académico.
    /// </summary>
    /// <remarks>
    /// El contenido se almacena como binario y debe ser PDF.
    /// </remarks>
    public class Documento : BaseEntity<int>
    {
        /// <summary>
        /// Representa el tamano maximo
        /// </summary>
        private const int MAX_FILE_SIZE = 3 * 1024 * 1024;

        /// <summary>Nombre original del archivo</summary>
        public string Nombre { get; private set; }

        /// <summary>Comentarios del personal sobre validación o rechazo</summary>
        public string? Observaciones { get; private set; }

        /// <summary>Identificador del trámite asociado</summary>
        public int TramiteId { get; private set; }
        public int TipoDocumentoId { get; private set; }

        /// <summary>Identificador del estado actual del documento</summary>
        public int EstadoDocumentoId { get; private set; }

        /// <summary>Contenido binario del archivo (blob)</summary>
        public byte[] Contenido { get; private set; }

        // Propiedades de navegación
        public EstadoDocumento EstadoDocumento { get; private set; }
        public Tramite Tramite { get; private set; }
        public TipoDocumento TipoDocumento { get; private set; }
        /// <summary>Constructor privado para migraciones</summary>
        private Documento() { }

        public Documento(
            string nombre,
            int tramiteId,
            int tipoDocumentoId,
            int estadoDocumentoId,
            byte[] contenido,
            string observaciones = "")
        {
            SetNombre(nombre);
            SetObservaciones(observaciones);
            TramiteId = tramiteId;
            TipoDocumentoId = tipoDocumentoId;
            EstadoDocumentoId = estadoDocumentoId;
            SetContenido(contenido);
        }

        public void SetNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new DomainException("El nombre del documento no puede estar vacío.");
            nombre = nombre?.Trim() ?? string.Empty;
            if (nombre.Length > 100)
                throw new DomainException("El nombre del documento no puede exceder 100 caracteres.");
            Nombre = nombre;
        }

        /// <summary>
        /// El revisor establece observaciones sobre el estado del documento
        /// </summary>
        public void SetObservaciones(string observaciones)
        {
            observaciones = observaciones?.Trim() ?? string.Empty;
            if (observaciones.Length > 1000)
                throw new DomainException("Las observaciones no pueden exceder 1000 caracteres.");
            Observaciones = observaciones;
        }

        /// <summary>
        /// Actualiza el estado del documento durante el proceso de validación
        /// </summary>
        public void SetEstado(EstadoDocumento nuevoEstado)
        {
            if (nuevoEstado == null)
                throw new DomainException("El estado del documento no puede ser nulo.");
            EstadoDocumentoId = nuevoEstado.Id;
        }

        /// <summary>
        /// Reemplaza el contenido del documento (solo para correcciones o actualizaciones)
        /// </summary>
        private void SetContenido(byte[] contenido)
        {
            if (contenido == null || contenido.Length == 0)
                throw new DomainException("El contenido del documento no puede estar vacío.");

            if (contenido.Length > MAX_FILE_SIZE)
                throw new DomainException($"El archivo no puede superar los {MAX_FILE_SIZE / (1024.0 * 1024.0)} MB.");

            Contenido = contenido;
        }
    }
}