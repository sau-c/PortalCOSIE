using PortalCOSIE.Application.Abstractions;
using PortalCOSIE.Domain.Entities.PeriodosConfig;

namespace PortalCOSIE.Application.Features.PeriodosConfig.Commands.EditarPeriodoConfig
{
    public sealed record EditarPeriodoConfigCommand(
        int AnioInicio,
        int PeriodoInicio,
        int AnioActual,
        int PeriodoActual
        ) : IRequest<PeriodoConfig>;
}
