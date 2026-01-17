using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.X509;
using PortalCOSIE.Application.Services.Crypto;
using System.Security.Cryptography;

namespace PortalCOSIE.Infrastructure.Services
{
    public class CriptoService : ICriptoService
    {
        /// <summary>
        /// Firma un documento usando certificado y llave privada desde streams
        /// </summary>
        public byte[] FirmarDocumento(Stream contenido, Stream certificadoStream, Stream llavePrivadaStream, string password)
        {
            // 1. Cargar la llave privada desde stream
            AsymmetricKeyParameter privateKey = LoadPrivateKeyFromStream(llavePrivadaStream, password);
            // 2. Cargar el certificado desde stream
            X509Certificate certificate = LoadCertificateFromStream(certificadoStream);
            // 3. Leer el contenido completo del documento
            byte[] contenidoBytes = ReadStreamToBytes(contenido);
            // 4. Crear el firmante con SHA256withRSA
            ISigner signer = SignerUtilities.GetSigner("SHA256withRSA");
            signer.Init(true, privateKey);
            // 5. Procesar el documento
            signer.BlockUpdate(contenidoBytes, 0, contenidoBytes.Length);
            // 6. Generar la firma
            byte[] signature = signer.GenerateSignature();
            return signature;
        }

        /// <summary>
        /// Verifica la firma de un documento usando streams
        /// </summary>
        public bool VerificarFirma(Stream contenido, Stream firma, Stream certificadoStream)
        {
            // 1. Cargar el certificado desde stream
            X509Certificate certificate = LoadCertificateFromStream(certificadoStream);
            // 2. Obtener la llave pública del certificado
            AsymmetricKeyParameter publicKey = certificate.GetPublicKey();
            // 3. Leer contenido y firma
            byte[] contenidoBytes = ReadStreamToBytes(contenido);
            byte[] firmaBytes = ReadStreamToBytes(firma);
            // 4. Crear el verificador
            ISigner verifier = SignerUtilities.GetSigner("SHA256withRSA");
            verifier.Init(false, publicKey);
            // 5. Procesar el documento
            verifier.BlockUpdate(contenidoBytes, 0, contenidoBytes.Length);
            // 6. Verificar la firma
            return verifier.VerifySignature(firmaBytes);
        }

        /// <summary>
        /// Carga la llave privada desde un stream
        /// </summary>
        private AsymmetricKeyParameter LoadPrivateKeyFromStream(Stream keyStream, string password)
        {
            if (keyStream == null)
                throw new ArgumentNullException(nameof(keyStream));

            if (keyStream.CanSeek)
                keyStream.Position = 0;

            byte[] keyBytes;
            using (var ms = new MemoryStream())
            {
                keyStream.CopyTo(ms);
                keyBytes = ms.ToArray();
            }

            try
            {
                // Desencripta la llave privada con la contraseña
                return PrivateKeyFactory.DecryptKey(password.ToCharArray(), keyBytes);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo cargar la llave privada. Verifica el formato y la contraseña.", ex);
            }
        }

        /// <summary>
        /// Carga el certificado desde un stream
        /// </summary>
        private X509Certificate LoadCertificateFromStream(Stream certStream)
        {
            if (certStream == null)
                throw new ArgumentNullException(nameof(certStream));

            // Posicionar al inicio si es posible
            if (certStream.CanSeek)
                certStream.Position = 0;

            var parser = new X509CertificateParser();
            return parser.ReadCertificate(certStream);
        }

        /// <summary>
        /// Lee un stream completo y lo convierte a array de bytes
        /// </summary>
        private byte[] ReadStreamToBytes(Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            // Posicionar al inicio si es posible
            if (stream.CanSeek)
                stream.Position = 0;

            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}