using PortalCOSIE.Application.Abstractions;
using PortalCOSIE.Domain.Entities.Carreras;

namespace PortalCOSIE.Application.Features.Carreras.Commands.ToggleUnidad
{
    public sealed record ToggleUnidadCommand(int carreraId, string unidadId) : IRequest<UnidadAprendizaje>;
}
