using PortalCOSIE.Domain.SharedKernel;
using System.Text.RegularExpressions;

namespace PortalCOSIE.Domain.Entities.Usuarios
{
    public abstract class Usuario : BaseEntity<int>
    {
        public string IdentityUserId { get; set; }
        public string Nombre { get; private set; }
        public string ApellidoPaterno { get; private set; }
        public string ApellidoMaterno { get; private set; }
        public string? CertificadoId { get; private set; }

        public Certificado Certificado { get; private set; }

        // Constantes de validación
        private const int LongitudMaxima = 100;
        private static readonly Regex SoloLetras =
            new Regex(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s\-\(\)]+$", RegexOptions.Compiled);

        // Constructor privado para EF
        protected Usuario() { }

        protected Usuario(
            string identityUserId,
            string nombre,
            string apellidoPaterno,
            string apellidoMaterno)
        {
            EstablecerIdentityUserId(identityUserId);
            EstablecerNombre(nombre);
            EstablecerApellidoPaterno(apellidoPaterno);
            EstablecerApellidoMaterno(apellidoMaterno);
        }

        public void EstablecerIdentityUserId(string identityUserId)
        {
            if (string.IsNullOrWhiteSpace(identityUserId))
                throw new DomainException("El identificador de usuario no puede estar vacío.");
            IdentityUserId = identityUserId.Trim();
        }

        public void EstablecerNombre(string nombre)
        {
            ValidarTexto(nombre, nameof(Nombre));
            Nombre = nombre;
        }

        public void EstablecerApellidoPaterno(string apellido)
        {
            ValidarTexto(apellido, nameof(ApellidoPaterno));
            ApellidoPaterno = apellido;
        }

        public void EstablecerApellidoMaterno(string apellido)
        {
            ValidarTexto(apellido, nameof(ApellidoMaterno));
            ApellidoMaterno = apellido;
        }

        public void AsignarCertificado(Certificado certificado)
        {
            if (certificado == null)
                throw new DomainException("El certificado no puede ser nulo.");
            Certificado = certificado;
            CertificadoId = certificado.Id;
        }

        // Método privado reutilizable
        private static void ValidarTexto(string valor, string campo)
        {
            if (string.IsNullOrWhiteSpace(valor))
                throw new DomainException($"El campo '{campo}' no puede estar vacío.");
            valor = valor?.Trim() ?? string.Empty;
            if (!SoloLetras.IsMatch(valor))
                throw new DomainException($"El campo '{campo}' solo puede contener letras, espacios o guiones.");
            if (valor.Length > LongitudMaxima)
                throw new DomainException($"El campo '{campo}' no puede exceder los {LongitudMaxima} caracteres.");
        }

        public string NombreCompleto()
        {
            return $"{Nombre} {ApellidoPaterno} {ApellidoMaterno}".Trim();
        }
    }
}
