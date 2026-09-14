using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Operators;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.X509;
using PortalCOSIE.Application;
using PortalCOSIE.Application.Services.Crypto;
using PortalCOSIE.Domain.Entities.Usuarios;
using PortalCOSIE.Domain.Enums;
using PortalCOSIE.Infrastructure.Persistence;

namespace PortalCOSIE.Infrastructure.Services;

public class CaEmisionService : ICaEmisionService
{
    private readonly AppDbContext _context;
    private readonly string? _privateKeyPem;
    private readonly string? _privateKeyPassword;

    public CaEmisionService(AppDbContext context, IConfiguration configuration)
    {
        _context = context;
        _privateKeyPem = configuration["Ca:PrivateKeyPem"];
        _privateKeyPassword = configuration["Ca:PrivateKeyPassword"];
    }

    public async Task<bool> PuedeEmitirAsync()
    {
        var ca = await ObtenerCaActivaAsync();
        if (ca == null || !TieneLlaveConfigurada())
            return false;

        return CoincideLlaveConCertificado(ca.CertificadoDer);
    }

    private bool TieneLlaveConfigurada()
        => !string.IsNullOrWhiteSpace(_privateKeyPem)
        && !string.IsNullOrWhiteSpace(_privateKeyPassword);

    public async Task<Result<Certificado>> EmitirDesdeCsrAsync(
        byte[] csrDer,
        TipoCertificado tipo,
        string? sujetoDn = null,
        int vigenciaAnios = 2)
    {
        if (tipo == TipoCertificado.Ca)
            return Result<Certificado>.Failure("La CA no emite certificados de tipo CA.");

        if (!await PuedeEmitirAsync())
            return Result<Certificado>.Failure(
                "La CA no está lista para emitir. Registre el certificado de la CA y configure la llave privada en el servidor.");

        var ca = await ObtenerCaActivaAsync();
        if (ca == null)
            return Result<Certificado>.Failure("No hay certificado de CA registrado.");

        try
        {
            var csr = new Pkcs10CertificationRequest(csrDer);
            if (!csr.Verify())
                return Result<Certificado>.Failure("La solicitud CSR no es válida o está corrupta.");

            var caCert = new X509CertificateParser().ReadCertificate(ca.CertificadoDer);
            var caKey = LeerLlavePrivada(_privateKeyPem!, _privateKeyPassword!);
            var random = new SecureRandom();

            var generator = new X509V3CertificateGenerator();
            generator.SetSerialNumber(new BigInteger(128, random));
            generator.SetIssuerDN(caCert.SubjectDN);
            generator.SetSubjectDN(
                string.IsNullOrWhiteSpace(sujetoDn)
                    ? csr.GetCertificationRequestInfo().Subject
                    : new X509Name(sujetoDn));
            generator.SetNotBefore(DateTime.Now.Date);
            generator.SetNotAfter(DateTime.Now.Date.AddYears(vigenciaAnios));
            generator.SetPublicKey(csr.GetPublicKey());

            generator.AddExtension(
                X509Extensions.BasicConstraints,
                true,
                new BasicConstraints(false));

            generator.AddExtension(
                X509Extensions.KeyUsage,
                true,
                new KeyUsage(KeyUsage.DigitalSignature | KeyUsage.NonRepudiation));

            generator.AddExtension(
                X509Extensions.ExtendedKeyUsage,
                false,
                new ExtendedKeyUsage(KeyPurposeID.IdKPClientAuth));

            var signatureFactory = new Asn1SignatureFactory("SHA256WithRSA", caKey, random);
            var cert = generator.Generate(signatureFactory);
            var certDer = cert.GetEncoded();

            var thumbprint = BitConverter
                .ToString(DigestUtilities.CalculateDigest("SHA-1", certDer))
                .Replace("-", string.Empty);

            var emitido = new Certificado(
                thumbprint,
                tipo,
                cert.SubjectDN.ToString(),
                cert.SerialNumber.ToString(),
                cert.NotBefore,
                cert.NotAfter,
                certDer);

            return Result<Certificado>.Success(emitido);
        }
        catch (Exception ex)
        {
            return Result<Certificado>.Failure($"No se pudo emitir el certificado: {ex.Message}");
        }
    }

    private Task<Certificado?> ObtenerCaActivaAsync()
        => _context.Set<Certificado>()
            .FirstOrDefaultAsync(c => c.Tipo == TipoCertificado.Ca && !c.IsDeleted);

    private AsymmetricKeyParameter LeerLlavePrivada(string pem, string password)
    {
        using var reader = new StringReader(pem);
        var pemReader = new PemReader(reader, new LlavePasswordFinder(password));
        var keyObject = pemReader.ReadObject()
            ?? throw new InvalidOperationException(
                "No se pudo leer la llave privada. Verifique el PEM cifrado y la contraseña.");

        return keyObject switch
        {
            AsymmetricCipherKeyPair pair => pair.Private,
            AsymmetricKeyParameter key => key,
            _ => throw new InvalidOperationException("Formato de llave privada no soportado.")
        };
    }

    private bool CoincideLlaveConCertificado(byte[] certificadoDer)
    {
        try
        {
            var privateKey = LeerLlavePrivada(_privateKeyPem!, _privateKeyPassword!);
            var cert = new X509CertificateParser().ReadCertificate(certificadoDer);
            return cert.GetPublicKey().Equals(ExtraerPublicaDesdePrivada(privateKey));
        }
        catch (Exception)
        {
            return false;
        }
    }

    private sealed class LlavePasswordFinder(string password) : IPasswordFinder
    {
        public char[] GetPassword() => password.ToCharArray();
    }

    private static AsymmetricKeyParameter ExtraerPublicaDesdePrivada(AsymmetricKeyParameter privateKey)
    {
        if (privateKey is Org.BouncyCastle.Crypto.Parameters.RsaPrivateCrtKeyParameters rsaPrivate)
            return new Org.BouncyCastle.Crypto.Parameters.RsaKeyParameters(false, rsaPrivate.Modulus, rsaPrivate.PublicExponent);

        throw new InvalidOperationException("Solo se soportan llaves RSA para la CA.");
    }
}