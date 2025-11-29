namespace PortalCOSIE.Web.Models
{
    public class SolicitudCtceVM
    {
        public string Situacion { get; set; }
        public bool TieneDictamenesAnteriores { get; set; }
        public List<UnidadReprobadaVM> UnidadesReprobadas { get; set; } = new List<UnidadReprobadaVM>();
        public IFormFile CartaExposicionMotivos { get; set; }
        public IFormFile Identificacion { get; set; }
        public IFormFile BoletaGlobal { get; set; }
        public IFormFile Probatorios { get; set; }
    }

    public class UnidadReprobadaVM
    {
        public int UnidadId { get; set; }
        public string PeriodoCursado { get; set; }
        public string PeriodoRecursado { get; set; }
    }
}