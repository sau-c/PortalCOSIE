using PortalCOSIE.Application.Features.Certificados.DTO;

namespace PortalCOSIE.Application.Features.Certificados.Queries.ObtenerCertificadoAcuse;

public sealed record ObtenerCertificadoAcuseQuery() : IRequest<CertificadoResumenDTO?>;