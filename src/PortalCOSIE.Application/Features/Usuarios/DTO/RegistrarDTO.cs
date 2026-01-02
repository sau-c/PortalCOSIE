namespace PortalCOSIE.Application.Features.Usuarios.DTO
{
    public class RegistrarDTO
    {
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string NumeroBoleta { get; set; }
        public string PeriodoIngreso { get; set; }
        public int CarreraId { get; set; }
        //public int PlanEstudioId { get; set; }
    }
}