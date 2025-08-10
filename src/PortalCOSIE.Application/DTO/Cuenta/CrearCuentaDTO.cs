using System.ComponentModel.DataAnnotations;

namespace PortalCOSIE.Application.DTO.Cuenta
{
    public class CrearCuentaDTO
    {
        [Required(ErrorMessage = "Campo obligatorio.")]
        [EmailAddress(ErrorMessage = "Formato incorrecto.")]
        public string Correo { get; set; }

        [Required(ErrorMessage = "Campo obligatorio.")]
        [DataType(DataType.Password)]
        public string Contrasena { get; set; }

        [Required(ErrorMessage = "Campo obligatorio.")]
        [DataType(DataType.Password)]
        [Compare("Contrasena", ErrorMessage = "Las contraseñas no coinciden")]
        public string ConfirmarContrasena { get; set; }
    }
}
