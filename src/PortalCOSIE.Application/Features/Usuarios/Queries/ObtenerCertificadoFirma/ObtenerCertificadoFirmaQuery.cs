using PortalCOSIE.Application.Abstractions;
using PortalCOSIE.Application.Features.Usuarios.DTO;

namespace PortalCOSIE.Application.Features.Usuarios.Queries.ObtenerCertificadoFirma
{
    public sealed record ObtenerCertificadoFirmaQuery(string IdentityUserId) : IRequest<CertificadoFirmaDTO?>;
}