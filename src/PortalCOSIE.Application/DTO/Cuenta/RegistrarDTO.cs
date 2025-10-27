using System.ComponentModel.DataAnnotations;

namespace PortalCOSIE.Application.DTO.Cuenta
{
    public class RegistrarDTO
    {   
        [Required(ErrorMessage = "Campo obligatorio.")]
        [StringLength(50, ErrorMessage = "Máximo 50 caracteres")]
        [RegularExpression("^[a-zA-ZáéíóúÁÉÍÓÚñÑ ]+$", ErrorMessage = "Solo se permiten letras.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Campo obligatorio.")]
        [StringLength(50, ErrorMessage = "Máximo 50 caracteres")]
        [RegularExpression("^[a-zA-ZáéíóúÁÉÍÓÚñÑ ]+$", ErrorMessage = "Solo se permiten letras.")]
        public string ApellidoPaterno { get; set; }

        [Required(ErrorMessage = "Campo obligatorio.")]
        [StringLength(50, ErrorMessage = "Máximo 50 caracteres")]
        [RegularExpression("^[a-zA-ZáéíóúÁÉÍÓÚñÑ ]+$", ErrorMessage = "Solo se permiten letras.")]
        public string ApellidoMaterno { get; set; }

        [Required(ErrorMessage = "Campo obligatorio.")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Ingrese 10 numeros")]
        [RegularExpression(@"^\d{1,10}$", ErrorMessage = "Ingrese solo números.")]
        public string NumeroBoleta { get; set; }

        [Required(ErrorMessage = "Campo obligatorio.")]
        public string PeriodoIngreso { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Campo obligatorio.")]
        public int CarreraId { get; set; }
    }
}