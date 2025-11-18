using PortalCOSIE.Domain.Entities.Usuarios;

namespace PortalCOSIE.Domain.Interfaces
{
    public interface IUsuarioRepository : IBaseRepository<Usuario>
    {
        Task<Usuario> BuscarPorIdentityId(string identityUserId);
        Task<Usuario> BuscarConAlumno(string identityUserId);
        Task<Usuario> BuscarConAlumnoYCarrera(string identityUserId);
        Task<Usuario> BuscarAlumnoPorBoleta(string boleta);
        Task<IEnumerable<Usuario>> ListarConAlumnoYCarrera();
        Task<IEnumerable<Usuario>> ListarConPersonal();
    }
}
