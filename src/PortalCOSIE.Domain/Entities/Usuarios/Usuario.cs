using System.Text.RegularExpressions;

namespace PortalCOSIE.Domain.Entities.Usuarios
{
    public class Usuario : BaseEntity
    {
        public string IdentityUserId { get; private set; }
        public string Nombre { get; private set; }
        public string ApellidoPaterno { get; private set; }
        public string ApellidoMaterno { get; private set; }

        // Relaciones — EF
        public Alumno? Alumno { get; private set; }
        public Personal? Personal { get; private set; }

        // Constantes de validación
        private const int LongitudMaxima = 100;
        private static readonly Regex SoloLetras =
            new Regex(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s\-\(\)]+$", RegexOptions.Compiled);

        // Constructor privado para EF
        private Usuario() { }

        public Usuario(string identityUserId, string nombre, string apellidoPaterno, string apellidoMaterno)
        {
            SetIdentityUserId(identityUserId);
            SetNombre(nombre);
            SetApellidoPaterno(apellidoPaterno);
            SetApellidoMaterno(apellidoMaterno);
        }

        public void SetIdentityUserId(string identityUserId)
        {
            if (string.IsNullOrWhiteSpace(identityUserId))
                throw new DomainException("El identificador de usuario no puede estar vacío.");
            IdentityUserId = identityUserId.Trim();
        }

        public void SetNombre(string nombre)
        {
            ValidarTexto(nombre, nameof(Nombre));
            Nombre = nombre;
        }

        public void SetApellidoPaterno(string apellido)
        {
            ValidarTexto(apellido, nameof(ApellidoPaterno));
            ApellidoPaterno = apellido;
        }

        public void SetApellidoMaterno(string apellido)
        {
            ValidarTexto(apellido, nameof(ApellidoMaterno));
            ApellidoMaterno = apellido;
        }

        // Método privado reutilizable
        private static void ValidarTexto(string valor, string campo)
        {
            valor = valor?.Trim() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(valor))
                throw new DomainException($"El campo '{campo}' no puede estar vacío.");
            if (!SoloLetras.IsMatch(valor))
                throw new DomainException($"El campo '{campo}' solo puede contener letras, espacios o guiones.");
            if (valor.Length > LongitudMaxima)
                throw new DomainException($"El campo '{campo}' no puede exceder los {LongitudMaxima} caracteres.");
        }

        public string NombreCompleto()
        {
            return $"{Nombre} {ApellidoPaterno} {ApellidoMaterno}".Trim();
        }

        // Métodos para asociar entidades relacionadas
        public void SetAlumno(Alumno alumno)
        {
            if (Personal != null)
                throw new DomainException("Usuario no puede ser alumno si ya es personal.");
            Alumno = alumno ?? throw new DomainException("El alumno no puede ser nulo.");
        }

        public void SetPersonal(Personal personal)
        {
            if (Alumno != null)
                throw new DomainException("Usuario no puede ser personal si ya es alumno.");
            Personal = personal ?? throw new DomainException("El personal no puede ser nulo.");
        }
    }
}
