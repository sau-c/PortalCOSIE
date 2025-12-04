using PortalCOSIE.Domain.Interfaces;

namespace PortalCOSIE.Domain.Entities.Usuarios
{
    public interface IUsuarioRepository : IBaseRepository<Usuario, int>
    {
        Task<Usuario> BuscarUsuario(string id);
        Task<Alumno> BuscarAlumnoConCarrera(string id);
        Task<Alumno> BuscarAlumnoPorBoleta(string boleta);
        Task<IEnumerable<Alumno>> ListarAlumnoConCarrera();
        Task<IEnumerable<Personal>> ListarConPersonal();
    }
}
