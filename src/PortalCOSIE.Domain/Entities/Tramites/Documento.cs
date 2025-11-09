namespace PortalCOSIE.Domain.Entities.Tramites
{
    public class Documento : BaseEntity
    {
        public string Nombre { get; private set; }
        public string Observaciones { get; private set; } = string.Empty;
        public int TramiteId { get; private set; }
        public int DocumentoEstadoId { get; private set; }
        public byte Blob { get; private set; }

        // Propiedades de navegación
        public EstadoDocumento EstadoDocumento { get; private set; }
        public Tramite Tramite { get; private set; }

        // Constructor privado para EF Core
        private Documento() { }

        // Constructor público seguro
        public Documento(
            string nombre,
            int tramiteId,
            int documentoEstadoId,
            byte blob,
            string observaciones = "")
        {
            SetNombre(nombre);
            SetObservaciones(observaciones);
            TramiteId = tramiteId;
            DocumentoEstadoId = documentoEstadoId;
            Blob = blob;
        }

        // Método de negocio para actualizar nombre
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
            DocumentoEstadoId = nuevoEstado.Id;
        }

        //public void AsociarTramite(Tramite tramite)
        //{
        //    if (tramite == null)
        //        throw new DomainException("El trámite no puede ser nulo.");

        //    Tramite = tramite;
        //    TramiteId = tramite.Id;
        //}

        public void SetBlob(byte blob)
        {
            Blob = blob;
        }
    }
}
