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
        public string FirmaCartaExposicionMotivos { get; set; }
        public string FirmaIdentificacion { get; set; }
        public string FirmaBoletaGlobal { get; set; }
        public string FirmaProbatorios { get; set; }
    }
}