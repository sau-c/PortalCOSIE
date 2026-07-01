namespace PortalCOSIE.Web.Models
{
    public class CorregirCtceVM
    {
        public int Id { get; set; }
        public IFormFile? CartaExposicionMotivos { get; set; }
        public IFormFile? Identificacion { get; set; }
        public IFormFile? BoletaGlobal { get; set; }
        public IFormFile? Probatorios { get; set; }
        public string? FirmaCartaExposicionMotivos { get; set; }
        public string? FirmaIdentificacion { get; set; }
        public string? FirmaBoletaGlobal { get; set; }
        public string? FirmaProbatorios { get; set; }
    }
}