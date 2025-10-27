using System.Text.RegularExpressions;

namespace PortalCOSIE.Domain.Entities
{
    public class Carrera : BaseEntity
    {
        public string Nombre { get; private set; } = string.Empty;
        private readonly List<UnidadAprendizaje> _unidadesAprendizaje = new();

        private const int LongitudMaxima = 100;
        private static readonly Regex SoloLetras =
            new Regex(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s\-\(\)]+$", RegexOptions.Compiled);

        public IReadOnlyCollection<UnidadAprendizaje> UnidadesAprendizaje => _unidadesAprendizaje;
        private Carrera() { }

        public Carrera(string nombre)
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

        public void AgregarUnidadAprendizaje(UnidadAprendizaje unidad)
        {
            if (unidad == null)
                throw new DomainException("La unidad de aprendizaje no puede ser nula");
            _unidadesAprendizaje.Add(unidad);
        }

        public void RemoverUnidadAprendizaje(UnidadAprendizaje unidad)
        {
            _unidadesAprendizaje.Remove(unidad);
        }
    }
}