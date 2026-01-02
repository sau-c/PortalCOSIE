using PortalCOSIE.Application.Abstractions;
using PortalCOSIE.Application.Features.Usuarios.DTO;

namespace PortalCOSIE.Application.Features.Usuarios.Queries.ListarPersonal
{
    public sealed record ListarPersonalQuery : IRequest<IEnumerable<PersonalDTO>>;
}
