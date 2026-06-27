using PortalCOSIE.Application.Features.Usuarios.DTO;
using PortalCOSIE.Domain.Entities.Documentos;

namespace PortalCOSIE.Application.Services.Query
{
    public interface IUsuarioQueryService
    {
        Task<IEnumerable<AlumnoDTO>> ListarAlumnosCompletos();
        Task<IEnumerable<PersonalDTO>> ListarPersonalCompletos();
        Task<int> ObtenerCarreraAlumnoPorId(string identityUserId);
        Task<UsuarioDTO> ObtenerUsuarioCompletoPorId(string identityUserId);
        Task<AlumnoContactoDTO?> ObtenerContactoAlumnoPorId(int alumnoId);
        Task<Documento> ObtenerDatosDocumentoPorId(int id);
    }
}
