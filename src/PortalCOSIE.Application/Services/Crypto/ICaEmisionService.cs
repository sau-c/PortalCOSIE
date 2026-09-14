using PortalCOSIE.Domain.Entities.Usuarios;
using PortalCOSIE.Domain.Enums;

namespace PortalCOSIE.Application.Services.Crypto;

public interface ICaEmisionService
{
    Task<bool> PuedeEmitirAsync();
    Task<Result<Certificado>> EmitirDesdeCsrAsync(
        byte[] csrDer,
        TipoCertificado tipo,
        string? sujetoDn = null,
        int vigenciaAnios = 2);
}