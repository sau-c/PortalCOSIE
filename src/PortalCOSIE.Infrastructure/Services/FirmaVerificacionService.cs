using Org.BouncyCastle.Cms;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.X509;
using PortalCOSIE.Application;
using PortalCOSIE.Application.Services.Crypto;
using PortalCOSIE.Domain.Entities.Usuarios;

namespace PortalCOSIE.Infrastructure.Services
{
    public class FirmaVerificacionService : IFirmaVerificacionService
    {
        public Result<bool> VerificarFirmaCms(Stream documento, byte[] firmaCms, Stream certificado)
        {
            if (documento == null)
                return Result<bool>.Failure("El documento a verificar es obligatorio.");
            if (firmaCms == null || firmaCms.Length == 0)
                return Result<bool>.Failure("La firma CMS es obligatoria.");
            if (certificado == null)
                return Result<bool>.Failure("El certificado público es obligatorio.");

            try
            {
                byte[] documentoBytes = ReadStreamToBytes(documento);
                byte[] certificadoBytes = ReadStreamToBytes(certificado);

                var parser = new X509CertificateParser();
                var certificadoEsperado = parser.ReadCertificate(certificadoBytes);

                var signedData = new CmsSignedData(new CmsProcessableByteArray(documentoBytes), firmaCms);
                var signers = signedData.GetSignerInfos().GetSigners();

                if (signers.Count == 0)
                    return Result<bool>.Failure("La firma CMS no contiene firmantes.");

                if (!IntentarVerificarFirmantes(signedData, signers, certificadoEsperado, documentoBytes))
                {
                    return Result<bool>.Failure(
                        "La firma CMS no es válida o no corresponde al certificado del firmante.");
                }

                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure($"No se pudo verificar la firma CMS: {ex.Message}");
            }
        }

        public Result<bool> ValidarVigenciaCertificado(Certificado certificado, DateTime? instanteUtc = null)
        {
            var instante = instanteUtc ?? DateTime.UtcNow;
            if (instante < certificado.VigenteDesde.ToUniversalTime())
                return Result<bool>.Failure("El certificado aún no es vigente.");
            if (instante > certificado.VigenteHasta.ToUniversalTime())
                return Result<bool>.Failure("El certificado está vencido.");

            return Result<bool>.Success(true);
        }

        private static bool IntentarVerificarFirmantes(
            CmsSignedData signedData,
            System.Collections.IEnumerable signers,
            X509Certificate certificadoEsperado,
            byte[] documentoBytes)
        {
            var certStore = signedData.GetCertificates();

            foreach (SignerInformation signer in signers)
            {
                if (VerificarSigner(signer, certificadoEsperado, documentoBytes))
                    return true;

                foreach (X509Certificate certificadoEmbebido in certStore.EnumerateMatches(signer.SignerID))
                {
                    if (!certificadoEmbebido.Equals(certificadoEsperado))
                        continue;

                    if (VerificarSigner(signer, certificadoEmbebido, documentoBytes))
                        return true;
                }
            }

            return false;
        }

        private static bool VerificarSigner(
            SignerInformation signer,
            X509Certificate certificado,
            byte[] documentoBytes)
        {
            try
            {
                if (signer.Verify(certificado))
                    return true;
            }
            catch
            {
            }

            try
            {
                if (signer.Verify(certificado.GetPublicKey()))
                    return true;
            }
            catch
            {
            }

            return VerificarFirmaPkcs1Directa(signer, certificado.GetPublicKey(), documentoBytes);
        }

        private static bool VerificarFirmaPkcs1Directa(
            SignerInformation signer,
            AsymmetricKeyParameter publicKey,
            byte[] documentoBytes)
        {
            if (signer.SignedAttributes != null && signer.SignedAttributes.Count > 0)
                return false;

            try
            {
                ISigner verifier = SignerUtilities.GetSigner("SHA256withRSA");
                verifier.Init(false, publicKey);
                verifier.BlockUpdate(documentoBytes, 0, documentoBytes.Length);
                return verifier.VerifySignature(signer.GetSignature());
            }
            catch
            {
                return false;
            }
        }

        private static byte[] ReadStreamToBytes(Stream stream)
        {
            if (stream.CanSeek)
                stream.Position = 0;

            using var memoryStream = new MemoryStream();
            stream.CopyTo(memoryStream);
            return memoryStream.ToArray();
        }
    }
}