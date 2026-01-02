using PortalCOSIE.Application.Abstractions;

namespace PortalCOSIE.Application.Features.PeriodosConfig.Queries.ListarPeriodos
{
    public sealed record ListarPeriodosQuery : IRequest<IEnumerable<string>>;
}
