using PortalCOSIE.Application.Features.Certificados.DTO;
using PortalCOSIE.Domain.Entities.Usuarios;

namespace PortalCOSIE.Application.Services;

public interface ICertificadoAdminService
{
    Task<Result<string>> EmitirAcuseDesdeCsrAsync(string identityUserId, byte[] csrDer);
    Task<CertificadoResumenDTO?> ObtenerCertificadoAcuseAsync();
    Task<Certificado?> ObtenerCertificadoAcuseParaFirmaAsync();
}