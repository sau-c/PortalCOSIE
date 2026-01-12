using PortalCOSIE.Application.Features.Tramites.DTO;

namespace PortalCOSIE.Web.Models
{
    public class SolicitudCtceVM
    {
        public string Peticion { get; set; }
        public bool TieneDictamenesAnteriores { get; set; }
        public List<UnidadReprobadaDTO> UnidadesReprobadas { get; set; } = new List<UnidadReprobadaDTO>();
        public IFormFile CartaExposicionMotivos { get; set; }
        public IFormFile Identificacion { get; set; }
        public IFormFile BoletaGlobal { get; set; }
        public IFormFile Probatorios { get; set; }
        public IFormFile LlaveKey { get; set; }
        public IFormFile CertificadoCer { get; set; }
        public string PasswordKey { get; set; }
    }
}