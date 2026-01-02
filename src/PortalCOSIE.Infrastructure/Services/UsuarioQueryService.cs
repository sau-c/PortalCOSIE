using Microsoft.EntityFrameworkCore;
using PortalCOSIE.Application.Features.Usuarios.DTO;
using PortalCOSIE.Application.Services;
using PortalCOSIE.Domain.Entities.Carreras;
using PortalCOSIE.Domain.Entities.Usuarios;
using PortalCOSIE.Infrastructure.Persistence;

namespace PortalCOSIE.Infrastructure.QueryService
{
    public class UsuarioQueryService : IUsuarioQueryService
    {
        private readonly AppDbContext _context;
        public UsuarioQueryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AlumnoDTO>> ListarAlumnosCompletos()
        {
            var query = from alumno in _context.Set<Alumno>()
                        join user in _context.Users
                            on alumno.IdentityUserId equals user.Id
                        join userRole in _context.UserRoles
                            on user.Id equals userRole.UserId into ur
                        from userRole in ur.DefaultIfEmpty() // left join
                        join role in _context.Roles
                            on userRole.RoleId equals role.Id into r
                        from role in r.DefaultIfEmpty() // left join
                        join carrera in _context.Set<Carrera>()
                            on alumno.CarreraId equals carrera.Id
                        select new AlumnoDTO
                        {
                            IdentityUserId = user.Id,
                            NumeroBoleta = alumno.NumeroBoleta,
                            Nombre = alumno.Nombre,
                            ApellidoPaterno = alumno.ApellidoPaterno,
                            ApellidoMaterno = alumno.ApellidoMaterno,
                            Carrera = carrera,
                            PeriodoIngreso = alumno.PeriodoIngreso,
                            Correo = user.Email,
                            Celular = user.PhoneNumber,
                            Rol = role != null ? role.Name : null // puede ser null
                        };

            return await query.AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<PersonalDTO>> ListarPersonalCompletos()
        {
            var query = from personal in _context.Set<Personal>()
                        join user in _context.Users
                            on personal.IdentityUserId equals user.Id
                        join userRole in _context.UserRoles
                            on user.Id equals userRole.UserId into ur
                        from userRole in ur.DefaultIfEmpty() // left join
                        join role in _context.Roles
                            on userRole.RoleId equals role.Id into r
                        from role in r.DefaultIfEmpty() // left join
                        select new PersonalDTO
                        {
                            IdentityUserId = user.Id,
                            Nombre = personal.Nombre,
                            ApellidoPaterno = personal.ApellidoPaterno,
                            ApellidoMaterno = personal.ApellidoMaterno,
                            Correo = user.Email,
                            //Celular = user.PhoneNumber,
                            Rol = role != null ? role.Name : null // puede ser null
                        };

            return await query.AsNoTracking().ToListAsync();
        }
        public async Task<AlumnoDTO?> ObtenerAlumnoCompletoPorId(string identityUserId)
        {
            var query = from alumno in _context.Set<Alumno>()
                        join user in _context.Users
                            on alumno.IdentityUserId equals user.Id
                        join carrera in _context.Set<Carrera>()
                            on alumno.CarreraId equals carrera.Id
                        where user.Id == identityUserId
                        select new AlumnoDTO
                        {
                            IdentityUserId = user.Id,
                            NumeroBoleta = alumno.NumeroBoleta,
                            Nombre = alumno.Nombre,
                            ApellidoPaterno = alumno.ApellidoPaterno,
                            ApellidoMaterno = alumno.ApellidoMaterno,
                            Carrera = carrera,
                            PeriodoIngreso = alumno.PeriodoIngreso,
                            Correo = user.Email,
                            CorreoConfirmado = user.EmailConfirmed,
                            Celular = user.PhoneNumber
                        };

            return await query.AsNoTracking().FirstOrDefaultAsync();
        }
        public async Task<int> ObtenerCarreraAlumnoPorId(string identityUserId)
        {
            return await _context.Set<Alumno>()
                .Where(a => a.IdentityUserId == identityUserId)
                .Select(a => a.CarreraId)
                .FirstOrDefaultAsync();
        }
    }
}
