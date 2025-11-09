using PortalCOSIE.Application.DTO.Usuario;
using PortalCOSIE.Domain.Entities.Usuarios;

namespace PortalCOSIE.Application.Interfaces
{
    public interface IUsuarioService
    {
        Task<Usuario> BuscarUsuarioPorIdentityId(string id);
        Task<AlumnoDTO?> BuscarAlumno(string identityUserId);
        Task<PersonalDTO?> BuscarPersonal(string identityUserId);
        Task<Result<string>> EditarAlumno(AlumnoDTO dto);
    }
}
