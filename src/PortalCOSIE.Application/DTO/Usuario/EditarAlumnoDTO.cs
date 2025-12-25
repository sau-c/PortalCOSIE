namespace PortalCOSIE.Application.DTO.Usuario
{
    public class EditarAlumnoDTO
    {
        public string IdentityUserId { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string PeriodoIngreso { get; set; }
        public string NumeroBoleta { get; set; }
        public int CarreraId { get; set; }
    }
}
