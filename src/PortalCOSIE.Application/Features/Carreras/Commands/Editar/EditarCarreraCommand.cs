using PortalCOSIE.Application.Abstractions;
using PortalCOSIE.Domain.Entities.Carreras;

namespace PortalCOSIE.Application.Features.Carreras.Commands.Editar
{
    public sealed record EditarCarreraCommand(int carreraId, string nombre) : IRequest<Carrera>;
}
