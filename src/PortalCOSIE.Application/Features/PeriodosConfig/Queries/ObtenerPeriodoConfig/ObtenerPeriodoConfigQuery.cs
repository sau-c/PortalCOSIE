using PortalCOSIE.Application.Abstractions;
using PortalCOSIE.Domain.Entities.PeriodosConfig;

namespace PortalCOSIE.Application.Features.PeriodosConfig.Queries.ObtenerPeriodoConfig
{
    public sealed record ObtenerPeriodoConfigQuery() : IRequest<PeriodoConfig>;
}
