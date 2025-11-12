namespace PortalCOSIE.Application.DTO.Cuenta
{
    public class RestablecerDTO
    {
        public string Token { get; set; }
        public string Correo { get; set; }
        public string NuevaContrasena { get; set; }
        public string ConfirmarContrasena { get; set; }
    }
}
