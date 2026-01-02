using PortalCOSIE.Application.Abstractions;
using PortalCOSIE.Domain.Entities.Carreras;
using PortalCOSIE.Domain.Enums;

namespace PortalCOSIE.Application.Features.Carreras.Commands.AgregarUnidad
{
    public sealed record AgregarUnidadCommand(
        string unidadId,
        string nombre,
        int carreraId,
        Semestre semestre
        ) : IRequest<UnidadAprendizaje>;
}
