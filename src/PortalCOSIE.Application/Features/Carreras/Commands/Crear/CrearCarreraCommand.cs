using PortalCOSIE.Application.Abstractions;
using PortalCOSIE.Domain.Entities.Carreras;

namespace PortalCOSIE.Application.Features.Carreras.Commands.Crear
{
    public sealed record CrearCarreraCommand(string nombre) : IRequest<Carrera>;
}
