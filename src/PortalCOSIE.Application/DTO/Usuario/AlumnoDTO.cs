using PortalCOSIE.Domain.Entities.Carreras;

namespace PortalCOSIE.Application.DTO.Usuario
{
    public class AlumnoDTO : UsuarioDTO
    {
        public string Celular { get; set; }
        public string PeriodoIngreso { get; set; }
        public string NumeroBoleta { get; set; }
        public Carrera Carrera { get; set; }
    }
}
