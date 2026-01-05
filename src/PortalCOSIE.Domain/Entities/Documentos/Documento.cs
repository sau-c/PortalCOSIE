using PortalCOSIE.Domain.Entities.Tramites;
using PortalCOSIE.Domain.SharedKernel;

namespace PortalCOSIE.Domain.Entities.Documentos
{
    /// <summary>
    /// Representa un documento PDF adjunto a un trámite académico.
    /// </summary>
    public class Documento : BaseEntity<int>
    {
        /// <summary>
        /// Representa el tamano maximo
        /// </summary>
        private const int HASH_SIZE_BYTES = 64; // 512 bits

        /// <summary>Nombre original del archivo</summary>
        public string Nombre { get; private set; }
        /// <summary> Ruta del archivo en el Blob container</summary>
        public string BlobPath { get; private set; }
        /// <summary>Comentarios del personal sobre validación o rechazo</summary>
        public string? Observaciones { get; private set; }
        /// <summary>Identificador del trámite asociado</summary>
        public int TramiteId { get; private set; }
        /// <summary> Indica si es identificacion, boleta, etc</summary>
        public int TipoDocumentoId { get; private set; }

        /// <summary>Identificador del estado actual del documento</summary>
        public int EstadoDocumentoId { get; private set; }

        /// <summary>Contenido hash del archivo antes de firmar</summary>
        public byte[] HashOriginal { get; private set; }
        /// <summary>Contenido hash del archivo despues de firmar</summary>

        // Propiedades de navegación
        public EstadoDocumento EstadoDocumento { get; private set; }
        public Tramite Tramite { get; private set; }
        public TipoDocumento TipoDocumento { get; private set; }
        /// <summary>Constructor privado para migraciones</summary>
        private Documento() { }

        public Documento(
            string nombre,
            string blobPath,
            int tramiteId,
            int tipoDocumentoId,
            int estadoDocumentoId,
            byte[] hashOriginal,
            string observaciones = "")
        {
            SetNombre(nombre);
            BlobPath = blobPath ?? throw new DomainException("La ruta del documento no puede ser nula.");
            TramiteId = tramiteId;
            TipoDocumentoId = tipoDocumentoId;
            EstadoDocumentoId = estadoDocumentoId;
            SetObservaciones(observaciones);
            SetHashOriginal(hashOriginal);
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
        /// Establece el contenido hash original del documento
        /// </summary>
        private void SetHashOriginal(byte[] hashOriginal)
        {
            if (hashOriginal == null || hashOriginal.Length == 0)
                throw new DomainException("El contenido hash no puede estar vacío.");

            if (hashOriginal.Length != HASH_SIZE_BYTES)
                throw new DomainException($"El hash debe ser de {HASH_SIZE_BYTES} bytes.");

            HashOriginal = hashOriginal;
        }
    }
}