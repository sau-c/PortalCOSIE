using PortalCOSIE.Application.Abstractions;
using PortalCOSIE.Domain.Entities.Carreras;
using PortalCOSIE.Domain.Enums;

namespace PortalCOSIE.Application.Features.Carreras.Commands.EditarUnidad
{
    public sealed record EditarUnidadCommand(
        string unidadId,
        string nombre,
        int carreraId,
        Semestre semestre
        ) : IRequest<UnidadAprendizaje>;
}
