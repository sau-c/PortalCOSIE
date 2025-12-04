using PortalCOSIE.Domain.Enums;
using System.Text.RegularExpressions;

namespace PortalCOSIE.Domain.Entities.Carreras
{
    /// <summary>
    /// Representa una carrera dentro del sistema.
    /// </summary>
    /// <remarks>
    /// Las carreras definen programas de estudio completos y contienen
    /// el catálogo de unidades de aprendizaje organizadas por semestres.
    /// </remarks>
    public class Carrera : BaseEntity<int>
    {
        /// <summary>Nombre corto de la carrera</summary>
        public string Nombre { get; private set; }

        private readonly List<UnidadAprendizaje> _unidadesAprendizaje = new();

        private const int LongitudMaxima = 100;
        private static readonly Regex SoloLetras =
            new Regex(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s\-\(\)]+$", RegexOptions.Compiled);

        /// <summary>Colección de unidades de aprendizaje que componen la carrera</summary>
        public IReadOnlyCollection<UnidadAprendizaje> UnidadesAprendizaje => _unidadesAprendizaje.AsReadOnly();

        /// <summary>Constructor privado para migraciones</summary>
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