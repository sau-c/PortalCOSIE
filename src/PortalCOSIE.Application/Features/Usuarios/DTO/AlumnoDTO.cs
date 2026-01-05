using PortalCOSIE.Domain.Entities.Carreras;

namespace PortalCOSIE.Application.Features.Usuarios.DTO
{
    public class AlumnoDTO : UsuarioDTO
    {
        public string PeriodoIngreso { get; set; }
        public string NumeroBoleta { get; set; }
        public Carrera Carrera { get; set; }
    }
}
