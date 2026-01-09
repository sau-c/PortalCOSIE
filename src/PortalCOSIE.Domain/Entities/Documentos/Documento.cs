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
        /// <summary> Ruta fisica del archivo </summary>
        public string Ruta { get; private set; }
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
            string ruta,
            int tramiteId,
            int tipoDocumentoId,
            byte[] hashOriginal)
        {
            EstablecerNombre(nombre);
            EstablecerRuta(ruta);
            TramiteId = tramiteId;
            TipoDocumentoId = tipoDocumentoId;
            EstadoDocumentoId = EstadoDocumento.EnRevision.Id;
            EstablecerHashOriginal(hashOriginal);
        }

        /// <summary>
        /// Establece el nombre original del documento
        /// </summary>
        public void EstablecerNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new DomainException("El nombre del documento no puede estar vacío.");
            nombre = nombre?.Trim() ?? string.Empty;
            if (nombre.Length > 100)
                throw new DomainException("El nombre del documento no puede exceder 100 caracteres.");
            Nombre = nombre;
        }
        /// <summary>
        /// Establece la ruta física del documento
        /// </summary>
        /// <param name="ruta"></param>
        /// <exception cref="DomainException"></exception>
        public void EstablecerRuta(string ruta)
        {
            if (string.IsNullOrWhiteSpace(ruta))
                throw new DomainException("La ruta del documento no puede estar vacía.");
            Ruta = ruta.Trim();
        }
        /// <summary>
        /// Establece el contenido hash original del documento
        /// </summary>
        private void EstablecerHashOriginal(byte[] hashOriginal)
        {
            if (hashOriginal == null || hashOriginal.Length == 0)
                throw new DomainException("El contenido hash no puede estar vacío.");
            if (hashOriginal.Length != HASH_SIZE_BYTES)
                throw new DomainException($"El hash debe ser de {HASH_SIZE_BYTES} bytes.");
            HashOriginal = hashOriginal;
        }
        /// <summary>
        /// El revisor establece observaciones sobre el estado del documento
        /// </summary>
        public void AgregarObservaciones(string? observaciones)
        {
            if (observaciones != null && observaciones.Length > 1000)
                throw new DomainException("Las observaciones no pueden exceder 1000 caracteres.");
            Observaciones = observaciones;
        }

        /// <summary>
        /// Actualiza el estado del documento durante el proceso de validación
        /// </summary>
        public void CambiarEstado(EstadoDocumento nuevoEstado)
        {
            if (nuevoEstado == null) throw new DomainException("El nuevo estado no puede ser nulo.");
            if (!EstadoDocumento.PuedeTransicionarA(nuevoEstado))
                throw new DomainException(
                    $"Documento no puede cambiar el estado de '{EstadoDocumento.Nombre}' a '{nuevoEstado.Nombre}'.");
            EstadoDocumentoId = nuevoEstado.Id;
        }

        public bool PermiteCorreccion()
            => EstadoDocumentoId == EstadoDocumento.ConErrores.Id || EstadoDocumentoId == EstadoDocumento.Incorrecto.Id;

        /// <summary>
        /// Actualiza el documento con un nuevo archivo para corrección
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="ruta"></param>
        /// <param name="hashOriginal"></param>
        /// <exception cref="DomainException"></exception>
        public void ActualizarDocumento(string nombre, string ruta, byte[] hashOriginal)
        {
            if (!PermiteCorreccion())
                throw new DomainException($"El documento '{Nombre}' no se puede corregir porque su estado es '{EstadoDocumento.Nombre}'. Solo se corrigen documentos incorrectos o con errores.");

            EstablecerNombre(nombre);
            EstablecerRuta(ruta);
            EstablecerHashOriginal(hashOriginal);
            CambiarEstado(EstadoDocumento.EnRevision);
            AgregarObservaciones(null);
        }
    }
}