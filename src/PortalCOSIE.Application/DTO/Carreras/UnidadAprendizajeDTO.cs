using PortalCOSIE.Domain.Enums;

namespace PortalCOSIE.Application.DTO.Carreras
{
    public record UnidadAprendizajeDTO(string id, string nombre, int carreraId, Semestre semestre);
}
