using PortalCOSIE.Application.DTO.Cuenta;
using PortalCOSIE.Application.DTO.Usuario;
using PortalCOSIE.Domain.Entities;

namespace PortalCOSIE.Application.Interfaces
{
    public interface IUsuarioService
    {
        Usuario? BuscarUsuarioPorIdentityId(string id);
        Task<Result<string>> RegistrarAlumno(RegistrarDTO dto, string userId);
        Task<AlumnoDTO?> BuscarAlumno(string id);
        Task<Result<string>> EditarAlumno(AlumnoDTO dto);
    }
}
