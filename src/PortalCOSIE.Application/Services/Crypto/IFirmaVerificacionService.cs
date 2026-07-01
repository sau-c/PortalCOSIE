using PortalCOSIE.Domain.Entities.Usuarios;

namespace PortalCOSIE.Application.Services.Crypto
{
    public interface IFirmaVerificacionService
    {
        const string AlgoritmoCmsPkcs7 = "CMS/PKCS#7";

        Result<bool> VerificarFirmaCms(Stream documento, byte[] firmaCms, Stream certificado);
        Result<bool> ValidarVigenciaCertificado(Certificado certificado, DateTime? instanteUtc = null);
    }
}