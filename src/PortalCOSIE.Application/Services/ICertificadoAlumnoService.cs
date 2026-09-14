using PortalCOSIE.Application.Features.Certificados.DTO;

namespace PortalCOSIE.Application.Services;

public interface ICertificadoAlumnoService
{
    Task<Result<string>> EmitirDesdeCsrAsync(string identityUserId, byte[] csrDer);
    Task<CertificadoResumenDTO?> ObtenerResumenAsync(string identityUserId);
    Task<Result<string>> RemoverCertificadoAsync(string identityUserId);
}