
namespace PortalCOSIE.Domain.Entities
{
    public class Tramite : BaseEntity
    {
        public int AlumnoId { get; set; }
        public int PersonalId { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public DateTime FechaConclusion { get; set; }

        public virtual Alumno Alumno { get; set; }
        public virtual Personal Personal { get; set; }
    }
}
