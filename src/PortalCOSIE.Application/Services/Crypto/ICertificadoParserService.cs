using PortalCOSIE.Domain.Entities.Usuarios;
using PortalCOSIE.Domain.Enums;

namespace PortalCOSIE.Application.Services.Crypto;

public interface ICertificadoParserService
{
    Result<Certificado> ParseCer(Stream cerStream, TipoCertificado tipo = TipoCertificado.Alumno);
}