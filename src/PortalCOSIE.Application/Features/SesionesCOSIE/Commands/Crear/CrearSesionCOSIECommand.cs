using PortalCOSIE.Application.Abstractions;
using PortalCOSIE.Domain.Entities.SesionesCOSIE;

namespace PortalCOSIE.Application.Features.SesionesCOSIE.Commands.Crear
{
    public sealed record CrearSesionCOSIECommand(
        string numeroSesion,
        DateTime fechaSesion,
        List<DateTime> fechasRecepcion
        ) : IRequest<SesionCOSIE>;
}
