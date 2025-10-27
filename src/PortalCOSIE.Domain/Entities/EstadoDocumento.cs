using System.Text.RegularExpressions;

namespace PortalCOSIE.Domain.Entities
{
    public class EstadoDocumento : BaseEntity
    {
        public string Nombre { get; private set; } = string.Empty;
        
        private const int LongitudMaxima = 100;
        private static readonly Regex SoloLetras =
            new Regex(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s\-\(\)]+$", RegexOptions.Compiled);

        private EstadoDocumento() { }

        public EstadoDocumento(string nombre)
        {
            ActualizarNombre(nombre);
        }

        public void ActualizarNombre(string nombre)
        {
            nombre = nombre.Trim();
            if (string.IsNullOrWhiteSpace(nombre))
                throw new DomainException("El nombre no puede estar vacío.");
            if (!SoloLetras.IsMatch(nombre))
                throw new DomainException("El nombre solo puede contener letras y espacios.");
            if (nombre.Length > LongitudMaxima)
                throw new DomainException($"El nombre no puede tener más de {LongitudMaxima} caracteres.");
            Nombre = nombre;
        }
    }
}