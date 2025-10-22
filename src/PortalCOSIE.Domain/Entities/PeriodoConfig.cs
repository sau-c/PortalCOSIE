namespace PortalCOSIE.Domain.Entities
{
    public class PeriodoConfig : BaseEntity
    {
        public string PeriodoInicio { get; set; }
        public string PeriodoFin { get; set; }
        public int PeriodosPorAnio { get; set; }
    }
}
