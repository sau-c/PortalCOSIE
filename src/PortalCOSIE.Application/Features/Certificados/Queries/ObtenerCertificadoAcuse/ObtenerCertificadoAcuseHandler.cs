using PortalCOSIE.Application.Features.Certificados.DTO;
using PortalCOSIE.Application.Services;

namespace PortalCOSIE.Application.Features.Certificados.Queries.ObtenerCertificadoAcuse;

public class ObtenerCertificadoAcuseHandler : IRequestHandler<ObtenerCertificadoAcuseQuery, CertificadoResumenDTO?>
{
    private readonly ICertificadoAdminService _certificados;

    public ObtenerCertificadoAcuseHandler(ICertificadoAdminService certificados)
        => _certificados = certificados;

    public Task<CertificadoResumenDTO?> Handle(ObtenerCertificadoAcuseQuery query)
        => _certificados.ObtenerCertificadoAcuseAsync();
}