using PortalCOSIE.Application.Abstractions;
using PortalCOSIE.Domain.Entities.SesionesCOSIE;

namespace PortalCOSIE.Application.Features.SesionesCOSIE.Commands.Editar
{
    public sealed record EditarSesionCOSIECommand(
        int id,
        string numeroSesion,
        DateTime fechaSesion,
        List<DateTime> fechasRecepcion
        ) : IRequest<SesionCOSIE>;
}
