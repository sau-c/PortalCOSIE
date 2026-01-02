namespace PortalCOSIE.Application.Features.Tramites.DTO
{
    public class ArchivoDescargaDTO
    {
        public string Nombre { get; set; }
        public string TipoContenido { get; set; } // Ejemplo: "application/pdf"
        public byte[] Contenido { get; set; }
    }
}
