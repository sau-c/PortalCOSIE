namespace PortalCOSIE.Web.Models
{
    public class CorregirCtceVM
    {
        public int Id { get; set; }
        //public Dictionary<int, IFormFile> Archivos { get; set; } = new();
        public IFormFile? CartaExposicionMotivos { get; set; }
        public IFormFile? Identificacion { get; set; }
        public IFormFile? BoletaGlobal { get; set; }
        public IFormFile? Probatorios { get; set; }
        public IFormFile LlaveKey { get; set; }
        public IFormFile CertificadoCer { get; set; }
        public string PasswordKey { get; set; }
    }
}