using PortalCOSIE.Domain.Enums;

namespace PortalCOSIE.Application.DTO.Carreras
{
    public record UnidadAprendizajeDTO(string unidadId, string nombre, int carreraId, Semestre semestre);
}
