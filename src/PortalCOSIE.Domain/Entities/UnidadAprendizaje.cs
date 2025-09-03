using PortalCOSIE.Domain.Enums;

namespace PortalCOSIE.Domain.Entities
{
    public class UnidadAprendizaje : BaseEntity
    {
        public UnidadAprendizaje() { }

        public UnidadAprendizaje(int id, string nombre, int carreraId, Semestre semestre)
        {
            Id = id;
            Nombre = nombre;
            CarreraId = carreraId;
            Semestre = semestre;
        }

        public string Nombre { get; set; }
        public int CarreraId { get; set; }
        public Semestre Semestre { get; set; }

        public virtual Carrera Carrera { get; set; } = null!;
    }
}
