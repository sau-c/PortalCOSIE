
namespace PortalCOSIE.Domain.Entities
{
    public class Tramite : BaseEntity
    {
        public int EstadoId { get; set; }
        public int AlumnoId { get; set; }
        public int PersonalId { get; set; }
        public int TipoTramiteId { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public DateTime FechaConclusion { get; set; }

        public virtual TramiteEstado TramiteEstado { get; set; }
        public virtual ICollection<Documento> Documentos { get; set; } = new List<Documento>();
        public virtual Alumno Alumno { get; set; }
        public virtual Personal Personal { get; set; }
    }
}
