using PortalCOSIE.Application.Features.Certificados.DTO;

namespace PortalCOSIE.Application.Services;

public interface ICertificadoCaService
{
    Task<Result<string>> RegistrarCaAsync(Stream cerStream);
    Task<CertificadoResumenDTO?> ObtenerCaAsync();
    Task<byte[]?> ObtenerCaCertificadoDerAsync();
}