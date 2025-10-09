namespace PortalCOSIE.Domain.Entities
{
    public class Carrera : BaseEntity
    {
        public Carrera() { }

        public string Nombre { get; set; }


        // Para navegar con EF Core
        public virtual ICollection<UnidadAprendizaje> UnidadesAprendizaje { get; set; } = new List<UnidadAprendizaje>();
    }
}
