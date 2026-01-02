using PortalCOSIE.Application.Abstractions;
using PortalCOSIE.Domain.Entities.Carreras;

namespace PortalCOSIE.Application.Features.Carreras.Commands.Toggle
{
    public sealed record ToggleCarreraCommand(int id) : IRequest<Carrera>;
}
