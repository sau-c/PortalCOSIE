using System.ComponentModel.DataAnnotations;

namespace PortalCOSIE.Application.DTO.Cuenta
{
    public class CrearContrasenaDTO
    {
        public string IdentityUserId { get; set; }

        [Required(ErrorMessage = "Campo obligatorio.")]
        [RegularExpression(@"^\d{1,10}$", ErrorMessage = "Ingrese solo números.")]
        public string Celular { get; set; }

        [Required(ErrorMessage = "Campo obligatorio.")]
        [DataType(DataType.Password)]
        public string Contrasena { get; set; }

        [Required(ErrorMessage = "Campo obligatorio.")]
        [DataType(DataType.Password)]
        [Compare("Contrasena", ErrorMessage = "Las contraseñas no coinciden")]
        public string ConfirmarContrasena { get; set; }
    }
}
