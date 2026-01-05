using PortalCOSIE.Domain.Interfaces;

namespace PortalCOSIE.Domain.Entities.Usuarios
{
    public interface IUsuarioRepository : IBaseRepository<Usuario, int>
    {
        Task<Usuario> BuscarUsuario(string identityUserId);
        Task<Personal> BuscarPersonal(string identityUserId);
        Task<Alumno> BuscarAlumnoConCarrera(string identityUserId);
        Task<Alumno> BuscarAlumnoPorBoleta(string boleta);
    }
}
