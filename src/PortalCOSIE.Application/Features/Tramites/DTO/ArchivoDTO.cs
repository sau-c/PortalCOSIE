namespace PortalCOSIE.Application.Features.Tramites.DTO
{
    public class ArchivoDTO
    {
        public string Nombre { get; set; }
        //public byte[] Contenido { get; set; }
        public Stream Contenido { get; set; }
        public string ContentType { get; set; }
    }
}
