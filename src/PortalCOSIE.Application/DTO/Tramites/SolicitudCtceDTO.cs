namespace PortalCOSIE.Application.DTO.Tramites
{
    public class SolicitudCtceDTO
    {
        public string Peticion { get; set; }
        public bool TieneDictamenesAnteriores { get; set; }
        public List<UnidadReprobadaDto> UnidadesReprobadas { get; set; } = new List<UnidadReprobadaDto>();
        public ArchivoDescargaDTO CartaExposicionMotivos { get; set; }
        public ArchivoDescargaDTO Identificacion { get; set; }
        public ArchivoDescargaDTO BoletaGlobal { get; set; }
        public ArchivoDescargaDTO Probatorios { get; set; }
    }

    public class UnidadReprobadaDto
    {
        public string UnidadId { get; set; }
        public string PeriodoCursado { get; set; }
        public string PeriodoRecursado { get; set; }
    }
}