using PortalCOSIE.Domain.Enums;

namespace PortalCOSIE.Domain.Entities
{
    public class UnidadAprendizaje : BaseEntity
    {
        public string Nombre { get; private set; }
        public int CarreraId { get; private set; }
        public Semestre Semestre { get; private set; }

        // Navegación
        public Carrera Carrera { get; private set; } = null!;

        // Constructor EF
        protected UnidadAprendizaje() { }

        // Constructor de dominio
        public UnidadAprendizaje(string nombre, int carreraId, Semestre semestre)
        {
            SetNombre(nombre);
            SetCarreraId(carreraId);
            SetSemestre(semestre);
        }

        public void SetNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre de la unidad de aprendizaje no puede estar vacío.", nameof(nombre));
            Nombre = nombre.Trim();
        }

        public void SetCarreraId(int carreraId)
        {
            if (carreraId <= 0)
                throw new ArgumentException("El ID de la carrera debe ser válido.", nameof(carreraId));
            CarreraId = carreraId;
        }

        public void SetSemestre(Semestre semestre)
        {
            if (!Enum.IsDefined(typeof(Semestre), semestre))
                throw new ArgumentException("El semestre especificado no es válido.", nameof(semestre));
            Semestre = semestre;
        }
    }
}
