namespace PortalCOSIE.Web.Models
{
    public class ConcluirVM
    {
        public int TramiteId { get; set; }
        public IFormFile Acuse { get; set; }
        public IFormFile LlaveKey { get; set; }
        public IFormFile CertificadoCer { get; set; }
        public string PasswordKey { get; set; }
    }
}