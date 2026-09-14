using PortalCOSIE.Application.Features.Certificados.DTO;
using PortalCOSIE.Application.Services;

namespace PortalCOSIE.Application.Features.Certificados.Queries.ObtenerCa;

public class ObtenerCaHandler : IRequestHandler<ObtenerCaQuery, CertificadoResumenDTO?>
{
    private readonly ICertificadoCaService _certificadoCa;

    public ObtenerCaHandler(ICertificadoCaService certificadoCa)
        => _certificadoCa = certificadoCa;

    public Task<CertificadoResumenDTO?> Handle(ObtenerCaQuery query)
        => _certificadoCa.ObtenerCaAsync();
}