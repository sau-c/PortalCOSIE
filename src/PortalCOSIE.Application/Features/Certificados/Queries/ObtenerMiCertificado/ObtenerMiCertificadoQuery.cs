using PortalCOSIE.Application.Features.Certificados.DTO;

namespace PortalCOSIE.Application.Features.Certificados.Queries.ObtenerMiCertificado;

public sealed record ObtenerMiCertificadoQuery(string IdentityUserId) : IRequest<CertificadoResumenDTO?>;