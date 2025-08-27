namespace PortalCOSIE.Domain.Entities
{
    public class Alumno : BaseEntity
    {
        public string NumeroBoleta { get; set; }
        public DateTime FechaIngreso { get; set; }
        public int? CarreraId { get; set; }
        
        public virtual Carrera Carrera { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
