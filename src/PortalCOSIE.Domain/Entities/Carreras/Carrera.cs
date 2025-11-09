using PortalCOSIE.Domain.Enums;
using System.Text.RegularExpressions;

namespace PortalCOSIE.Domain.Entities.Carreras
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
            if (string.IsNullOrWhiteSpace(nombre))
                throw new DomainException("El nombre no puede estar vacío.");
            nombre = nombre.Trim();
            if (!SoloLetras.IsMatch(nombre))
                throw new DomainException("El nombre solo puede contener letras y espacios.");
            if (nombre.Length > LongitudMaxima)
                throw new DomainException($"El nombre no puede tener más de {LongitudMaxima} caracteres.");
            Nombre = nombre;
        }

        public void AgregarUnidad(string nombre, Semestre semestre)
        {
            if (_unidadesAprendizaje.Any(u => u.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase)))
                throw new DomainException($"Ya existe una unidad con el nombre '{nombre}'.");

            var unidad = new UnidadAprendizaje(nombre, Id, semestre);
            _unidadesAprendizaje.Add(unidad);
        }

        //public void RemoverUnidad(int id)
        //{
        //    var unidad = _unidadesAprendizaje.FirstOrDefault(u => u.Id == id)
        //    ?? throw new DomainException("Unidad no encontrada.");
        //    _unidadesAprendizaje.Remove(unidad);
        //}
    }
}