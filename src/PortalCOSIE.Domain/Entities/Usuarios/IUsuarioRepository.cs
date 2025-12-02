using PortalCOSIE.Domain.Interfaces;

namespace PortalCOSIE.Domain.Entities.Usuarios
{
    public interface IUsuarioRepository : IBaseRepository<Usuario>
    {
        Task<Usuario> BuscarUsuario(string identityUserId);
        Task<Alumno> BuscarAlumnoConCarrera(string identityUserId);
        Task<Alumno> BuscarAlumnoPorBoleta(string boleta);
        Task<IEnumerable<Alumno>> ListarAlumnoConCarrera();
        Task<IEnumerable<Personal>> ListarConPersonal();
    }
}
