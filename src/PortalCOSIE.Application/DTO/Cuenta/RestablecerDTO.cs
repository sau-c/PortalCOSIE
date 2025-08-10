using System.ComponentModel.DataAnnotations;

namespace PortalCOSIE.Application.DTO.Cuenta
{
    public class RestablecerDTO
    {
        [Required]
        public string Token { get; set; }

        [Required(ErrorMessage = "Campo obligatorio.")]
        [EmailAddress(ErrorMessage = "Formato incorrecto.")]
        public string Correo { get; set; }

        [Required(ErrorMessage = "Campo obligatorio.")]
        [DataType(DataType.Password)]
        public string NuevaContrasena { get; set; }

        [Required(ErrorMessage = "Campo obligatorio.")]
        [DataType(DataType.Password)]
        [Compare("NuevaContrasena", ErrorMessage = "Las contraseñas no coinciden")]
        public string ConfirmarContrasena { get; set; }
    }
}
