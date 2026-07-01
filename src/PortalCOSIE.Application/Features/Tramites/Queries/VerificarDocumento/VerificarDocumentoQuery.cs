using PortalCOSIE.Application.Abstractions;

namespace PortalCOSIE.Application.Features.Tramites.Queries.VerificarDocumento
{
    public sealed record VerificarDocumentoQuery(
        string IdentityUserId,
        string Rol,
        int DocumentoId
    ) : IRequest<Result<string>>;
}