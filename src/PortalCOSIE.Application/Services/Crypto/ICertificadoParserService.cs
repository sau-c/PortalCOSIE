using PortalCOSIE.Domain.Entities.Usuarios;

namespace PortalCOSIE.Application.Services.Crypto
{
    public interface ICertificadoParserService
    {
        Result<Certificado> ParseCer(Stream cerStream);
    }
}