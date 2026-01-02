using PortalCOSIE.Application.Features.Usuarios.DTO;

namespace PortalCOSIE.Application.Services
{
    public interface IUsuarioQueryService
    {
        Task<IEnumerable<AlumnoDTO>> ListarAlumnosCompletos();
        Task<IEnumerable<PersonalDTO>> ListarPersonalCompletos();
        Task<int> ObtenerCarreraAlumnoPorId(string identityUserId);
        Task<AlumnoDTO> ObtenerAlumnoCompletoPorId(string identityUserId);
    }
}
