namespace PortalCOSIE.Application.DTO.Cuenta
{
    public class RegistrarAlumnoDTO
    {   
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string NumeroBoleta { get; set; }
        public string PeriodoIngreso { get; set; }
        public int CarreraId { get; set; }
        public string Correo { get; set; }
    }
}