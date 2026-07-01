namespace PortalCOSIE.Application.Features.Tramites.DTO
{
    public class DocumentoFirmadoDTO
    {
        public string Nombre { get; set; }
        public Stream Contenido { get; set; }
        public byte[] FirmaCms { get; set; }
    }
}