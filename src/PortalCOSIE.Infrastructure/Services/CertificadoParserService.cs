using Org.BouncyCastle.Security;
using Org.BouncyCastle.X509;
using PortalCOSIE.Application;
using PortalCOSIE.Application.Services.Crypto;
using PortalCOSIE.Domain.Entities.Usuarios;
using PortalCOSIE.Domain.Enums;

namespace PortalCOSIE.Infrastructure.Services
{
    public class CertificadoParserService : ICertificadoParserService
    {
        public Result<Certificado> ParseCer(Stream cerStream, TipoCertificado tipo = TipoCertificado.Alumno)
        {
            if (cerStream == null)
                return Result<Certificado>.Failure("El archivo .cer es obligatorio.");

            try
            {
                var bytes = LeerBytes(cerStream);
                if (bytes.Length < 300)
                    return Result<Certificado>.Failure("El archivo .cer está incompleto o no es válido.");

                var x509 = new X509CertificateParser().ReadCertificate(bytes);
                var certificadoDer = x509.GetEncoded();
                var thumbprint = BitConverter
                    .ToString(DigestUtilities.CalculateDigest("SHA-1", certificadoDer))
                    .Replace("-", string.Empty);

                var certificado = new Certificado(
                    thumbprint,
                    tipo,
                    x509.SubjectDN.ToString(),
                    x509.SerialNumber.ToString(),
                    x509.NotBefore,
                    x509.NotAfter,
                    certificadoDer);

                return Result<Certificado>.Success(certificado);
            }
            catch (Exception ex)
            {
                return Result<Certificado>.Failure($"No se pudo leer el archivo .cer: {ex.Message}");
            }
        }

        private static byte[] LeerBytes(Stream stream)
        {
            if (stream.CanSeek)
                stream.Position = 0;

            using var memoryStream = new MemoryStream();
            stream.CopyTo(memoryStream);
            return memoryStream.ToArray();
        }
    }
}