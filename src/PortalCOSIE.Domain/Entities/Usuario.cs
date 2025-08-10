namespace PortalCOSIE.Domain.Entities
{
    public class Usuario : BaseEntity
    {
        //UserId para desacoplar de Identity
        public string IdentityUserId { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }

        //Necesario para relacion 1:1 con EF
        public virtual Alumno Alumno { get; set; }
        public virtual Personal Personal { get; set; }
    }
}
