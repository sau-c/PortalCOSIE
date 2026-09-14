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
        public Result<bool> VerificarFirmaCms(
            Stream documento,
            byte[] firmaCms,
            Stream certificado,
            byte[]? caCertificadoDer = null)
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

                if (caCertificadoDer != null && caCertificadoDer.Length > 0)
                {
                    using var streamCa = new MemoryStream(caCertificadoDer);
                    var emisor = ValidarEmisorCa(new MemoryStream(certificadoBytes), caCertificadoDer);
                    if (!emisor.Succeeded)
                        return emisor;
                }

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

        public Result<bool> VerificarFirmaCmsPaquete(
            byte[] firmaCms,
            Stream certificado,
            byte[]? caCertificadoDer = null)
        {
            if (firmaCms == null || firmaCms.Length == 0)
                return Result<bool>.Failure("La firma CMS es obligatoria.");
            if (certificado == null)
                return Result<bool>.Failure("El certificado público es obligatorio.");

            try
            {
                byte[] certificadoBytes = ReadStreamToBytes(certificado);
                var certificadoEsperado = new X509CertificateParser().ReadCertificate(certificadoBytes);

                if (caCertificadoDer != null && caCertificadoDer.Length > 0)
                {
                    var emisor = ValidarEmisorCa(new MemoryStream(certificadoBytes), caCertificadoDer);
                    if (!emisor.Succeeded)
                        return emisor;
                }

                var signedData = new CmsSignedData(firmaCms);
                var signers = signedData.GetSignerInfos().GetSigners();
                if (signers.Count == 0)
                    return Result<bool>.Failure("La firma CMS no contiene firmantes.");

                if (!IntentarVerificarPaquete(signedData, signers, certificadoEsperado))
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

        public Result<bool> ValidarEmisorCa(Stream certificadoFirmante, byte[] caCertificadoDer)
        {
            if (certificadoFirmante == null)
                return Result<bool>.Failure("El certificado del firmante es obligatorio.");
            if (caCertificadoDer == null || caCertificadoDer.Length == 0)
                return Result<bool>.Failure("El certificado de la CA no está disponible.");

            try
            {
                var parser = new X509CertificateParser();
                var firmante = parser.ReadCertificate(ReadStreamToBytes(certificadoFirmante));
                var ca = parser.ReadCertificate(caCertificadoDer);

                if (!firmante.IssuerDN.Equivalent(ca.SubjectDN))
                {
                    return Result<bool>.Failure(
                        "El certificado no fue emitido por la autoridad certificadora PortalCOSIE.");
                }

                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure($"No se pudo validar el emisor del certificado: {ex.Message}");
            }
        }

        private static bool IntentarVerificarPaquete(
            CmsSignedData signedData,
            System.Collections.IEnumerable signers,
            X509Certificate certificadoEsperado)
        {
            var certStore = signedData.GetCertificates();

            foreach (SignerInformation signer in signers)
            {
                if (IntentarVerificarSigner(signer, certificadoEsperado))
                    return true;

                foreach (X509Certificate certificadoEmbebido in certStore.EnumerateMatches(signer.SignerID))
                {
                    if (!certificadoEmbebido.Equals(certificadoEsperado))
                        continue;

                    if (IntentarVerificarSigner(signer, certificadoEmbebido))
                        return true;
                }
            }

            return false;
        }

        private static bool IntentarVerificarSigner(SignerInformation signer, X509Certificate certificado)
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
                return signer.Verify(certificado.GetPublicKey());
            }
            catch
            {
                return false;
            }
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