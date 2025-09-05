using System.ComponentModel.DataAnnotations;

namespace PortalCOSIE.Application.DTO.Usuario
{
    public class AlumnoDTO
    {
        public string IdentityUserId { get; set; }
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
        public string PeriodoIngreso { get; set; }

        [Required(ErrorMessage = "Campo obligatorio.")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Ingrese solo 10 numeros")]
        public string NumeroBoleta { get; set; }

        [Required(ErrorMessage = "Campo obligatorio.")]
        public int? CarreraId { get; set; }

        public string? CarreraNombre { get; set; }
    }
}
