using PortalCOSIE.Application.DTO.Cuenta;
using PortalCOSIE.Application.DTO.Usuario;
using PortalCOSIE.Domain.Entities.Usuarios;

namespace PortalCOSIE.Application.Interfaces
{
    public interface IUsuarioService
    {
        Task<Result<string>> RegistrarAlumno(RegistrarDTO dto, string userId);
        Task<Usuario> BuscarUsuarioPorIdentityId(string id);
        Task<Personal> BuscarPersonal(string identityUserId);
        Task<Result<string>> EditarAlumno(EditarAlumnoDTO dto);
        Task<Result<string>> EditarPersonal(EditarPersonalDTO dto);
    }
}
