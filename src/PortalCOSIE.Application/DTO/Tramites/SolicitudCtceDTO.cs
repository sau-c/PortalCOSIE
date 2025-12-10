using PortalCOSIE.Domain.Entities.Documentos;

namespace PortalCOSIE.Application.DTO.Tramites
{
    public class SolicitudCtceDTO
    {
        public string Peticion { get; set; }
        public bool TieneDictamenesAnteriores { get; set; }
        public List<UnidadReprobadaDto> UnidadesReprobadas { get; set; } = new List<UnidadReprobadaDto>();
        public DocumentoDto CartaExposicionMotivos { get; set; }
        public DocumentoDto Identificacion { get; set; }
        public DocumentoDto BoletaGlobal { get; set; }
        public DocumentoDto Probatorios { get; set; }
    }

    public class UnidadReprobadaDto
    {
        public string UnidadId { get; set; }
        public string PeriodoCursado { get; set; }
        public string PeriodoRecursado { get; set; }
    }

    public class DocumentoDto
    {
        public string Nombre { get; set; }
        public Stream Contenido { get; set; }
    }
}