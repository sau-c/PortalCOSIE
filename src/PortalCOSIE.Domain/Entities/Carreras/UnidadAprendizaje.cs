using PortalCOSIE.Domain.Enums;
using System.Text.RegularExpressions;

namespace PortalCOSIE.Domain.Entities.Carreras
{
    /// <summary>
    /// Representa una unidad de aprendizaje dentro de un plan de estudios.
    /// </summary>
    /// <remarks>
    /// El ID de la unidad utiliza el formato: [Caracter carrera][Semestre o Nivel][Secuencia]
    /// Las unidades definen la base para análisis de casos CTCE.
    /// </remarks>
    public class UnidadAprendizaje : BaseEntity<string>
    {
        /// <summary>
        /// Patroon para validar el formato del Id
        /// </summary>
        private static readonly Regex IdPattern = new(@"^[A-Z][0-9]{3}$", RegexOptions.Compiled);

        /// <summary>Nombre completo de la unidad de aprendizaje en mayusculas</summary>
        public string Nombre { get; private set; }

        /// <summary>Identificador de la carrera a la que pertenece</summary>
        public int CarreraId { get; private set; }

        /// <summary>Semestre/Nivel en que se cursa según el plan de estudios</summary>
        public Semestre Semestre { get; private set; }

        // Navegación
        public Carrera Carrera { get; private set; } = null!;

        /// <summary>Constructor protegido para migraciones</summary>
        protected UnidadAprendizaje() { }

        public UnidadAprendizaje(string id, string nombre, int carreraId, Semestre semestre)
        {
            SetId(id);
            SetCarreraId(carreraId);
            SetNombre(nombre);
            SetCarreraId(carreraId);
            SetSemestre(semestre);
        }

        /// <summary>
        /// Valida y establece que el id de la unidad cumpla la nomenclatura
        /// </summary>
        public void SetId(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("El ID de la unidad de aprendizaje no puede estar vacío.", nameof(id));
            id = id.Trim();
            if (!IdPattern.IsMatch(id))
                throw new ArgumentException("Formato inválido. El ID debe comenzar con una letra mayúscula seguida de 3 dígitos (Ej: A123).", nameof(id));
            Id = id;
        }

        /// <summary>
        /// Valida y establece el nombre de la unidad
        /// </summary>
        public void SetNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre de la unidad de aprendizaje no puede estar vacío.", nameof(nombre));
            Nombre = nombre.Trim();
        }

        /// <summary>
        /// Asigna la carrera a la que pertenece la unidad
        /// </summary>
        private void SetCarreraId(int carreraId)
        {
            if (carreraId <= 0)
                throw new ArgumentException("El ID de la carrera debe ser válido.", nameof(carreraId));
            CarreraId = carreraId;
        }

        /// <summary>
        /// Establece el semestre/nivel de la unidad
        /// </summary>
        public void SetSemestre(Semestre semestre)
        {
            if (!Enum.IsDefined(typeof(Semestre), semestre))
                throw new ArgumentException("El semestre especificado no es válido.", nameof(semestre));
            Semestre = semestre;
        }
    }
}