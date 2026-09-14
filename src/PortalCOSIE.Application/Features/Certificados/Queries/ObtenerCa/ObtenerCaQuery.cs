using PortalCOSIE.Application.Features.Certificados.DTO;

namespace PortalCOSIE.Application.Features.Certificados.Queries.ObtenerCa;

public sealed record ObtenerCaQuery() : IRequest<CertificadoResumenDTO?>;