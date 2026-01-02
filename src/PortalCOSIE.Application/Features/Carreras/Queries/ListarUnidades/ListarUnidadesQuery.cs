using PortalCOSIE.Application.Abstractions;
using PortalCOSIE.Domain.Entities.Carreras;

namespace PortalCOSIE.Application.Features.Carreras.Queries.ListarUnidades
{
    public sealed record ListarUnidadesQuery(string identityUserId) : IRequest<IEnumerable<UnidadAprendizaje>>;
}
