using PortalCOSIE.Application.Features.Usuarios.DTO;
using PortalCOSIE.Domain.Entities.Documentos;

namespace PortalCOSIE.Application.Services
{
    public interface IUsuarioQueryService
    {
        Task<IEnumerable<AlumnoDTO>> ListarAlumnosCompletos();
        Task<IEnumerable<PersonalDTO>> ListarPersonalCompletos();
        Task<int> ObtenerCarreraAlumnoPorId(string identityUserId);
        Task<UsuarioDTO> ObtenerUsuarioCompletoPorId(string identityUserId);
        Task<Documento> ObtenerDatosDocumentoPorId(int id);
    }
}
