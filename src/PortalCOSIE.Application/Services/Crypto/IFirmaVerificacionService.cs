using PortalCOSIE.Domain.Entities.Usuarios;

namespace PortalCOSIE.Application.Services.Crypto
{
    public interface IFirmaVerificacionService
    {
        const string AlgoritmoCmsPkcs7 = "CMS/PKCS#7";
        const string AlgoritmoPadesBes = "PAdES-BES";

        Result<bool> VerificarFirmaCms(
            Stream documento,
            byte[] firmaCms,
            Stream certificado,
            byte[]? caCertificadoDer = null);
        Result<bool> VerificarFirmaCmsPaquete(
            byte[] firmaCms,
            Stream certificado,
            byte[]? caCertificadoDer = null);
        Result<bool> ValidarVigenciaCertificado(Certificado certificado, DateTime? instanteUtc = null);
        Result<bool> ValidarEmisorCa(Stream certificadoFirmante, byte[] caCertificadoDer);
    }
}