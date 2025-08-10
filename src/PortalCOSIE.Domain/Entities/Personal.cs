namespace PortalCOSIE.Domain.Entities
{
    public class Personal : BaseEntity
    {
        public string IdPersonal { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
