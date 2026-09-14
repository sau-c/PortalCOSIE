using PortalCOSIE.Application.Features.Certificados.DTO;
using PortalCOSIE.Application.Services;

namespace PortalCOSIE.Application.Features.Certificados.Queries.ObtenerMiCertificado;

public class ObtenerMiCertificadoHandler : IRequestHandler<ObtenerMiCertificadoQuery, CertificadoResumenDTO?>
{
    private readonly ICertificadoAlumnoService _certificados;

    public ObtenerMiCertificadoHandler(ICertificadoAlumnoService certificados)
        => _certificados = certificados;

    public Task<CertificadoResumenDTO?> Handle(ObtenerMiCertificadoQuery query)
        => _certificados.ObtenerResumenAsync(query.IdentityUserId);
}