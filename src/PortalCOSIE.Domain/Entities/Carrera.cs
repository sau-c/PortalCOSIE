namespace PortalCOSIE.Domain.Entities
{
    public class Carrera : BaseEntity
    {
        public string Nombre { get; set; }

        public virtual ICollection<UnidadAprendizaje> UnidadesAprendizaje { get; set; } = new List<UnidadAprendizaje>();
    }
}
