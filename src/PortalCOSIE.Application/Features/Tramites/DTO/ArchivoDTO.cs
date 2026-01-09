namespace PortalCOSIE.Application.Features.Tramites.DTO
{
    public class ArchivoDTO
    {
        //public int TipoDocumentoId { get; set; }
        public string Nombre { get; set; }
        public Stream Contenido { get; set; }
        public string ContentType { get; set; }
    }
}
