namespace PortalCOSIE.Domain.Entities
{
    public class Documento : BaseEntity
    {
        public string Nombre { get; set; }
        public string Observaciones { get; set; } = string.Empty;
        public int TramiteId { get; set; }
        public int DocumentoEstadoId { get; set; }
        public int Blob { get; set; }

        public virtual DocumentoEstado DocumentoEstado { get; set; }
        public virtual Tramite Tramite { get; set; }
    }
}
