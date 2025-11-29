using PortalCOSIE.Domain.Entities.Tramites;

namespace PortalCOSIE.Domain.Entities.Documentos
{
    public class Documento : BaseEntity
    {
        public string Nombre { get; private set; }
        public string Observaciones { get; private set; } = string.Empty;
        public int TramiteId { get; private set; }
        public int EstadoDocumentoId { get; private set; }
        public byte[] Contenido { get; private set; }

        // Propiedades de navegación
        public EstadoDocumento EstadoDocumento { get; private set; }
        public Tramite Tramite { get; private set; }

        // Constructor privado para EF Core
        private Documento() { }

        public Documento(
            string nombre,
            int tramiteId,
            int estadoDocumentoId,
            byte[] contenido,
            string observaciones = "")
        {
            SetNombre(nombre);
            SetObservaciones(observaciones);
            TramiteId = tramiteId;
            EstadoDocumentoId = estadoDocumentoId;
            Contenido = contenido;
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

        public void SetObservaciones(string observaciones)
        {
            observaciones = observaciones?.Trim() ?? string.Empty;
            if (observaciones.Length > 1000)
                throw new DomainException("Las observaciones no pueden exceder 1000 caracteres.");
            Observaciones = observaciones;
        }

        public void SetEstado(EstadoDocumento nuevoEstado)
        {
            if (nuevoEstado == null)
                throw new DomainException("El estado del documento no puede ser nulo.");
            EstadoDocumentoId = nuevoEstado.Id;
        }

        public void SetContenido(byte[] contenido)
        {
            Contenido = contenido;
        }
    }
}
