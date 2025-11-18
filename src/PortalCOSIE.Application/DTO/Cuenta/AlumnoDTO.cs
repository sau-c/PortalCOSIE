namespace PortalCOSIE.Application.DTO.Cuenta
{
    public class AlumnoDTO
    {
        public string IdentityUserId { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string PeriodoIngreso { get; set; }
        public string NumeroBoleta { get; set; }
        public int CarreraId { get; set; }
        //public string? CarreraNombre { get; set; }
    }
}
