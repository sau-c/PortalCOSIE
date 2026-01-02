using PortalCOSIE.Application.Abstractions;
using PortalCOSIE.Domain.Entities.SesionesCOSIE;

namespace PortalCOSIE.Application.Features.SesionesCOSIE.Commands.Toggle
{
    public sealed record ToggleSesionCOSIECommand(int id) : IRequest<SesionCOSIE>;
}
