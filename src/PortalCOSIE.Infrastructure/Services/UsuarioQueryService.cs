using Microsoft.EntityFrameworkCore;
using PortalCOSIE.Application.Features.Usuarios.DTO;
using PortalCOSIE.Application.Services;
using PortalCOSIE.Domain.Entities.Carreras;
using PortalCOSIE.Domain.Entities.Documentos;
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
        public async Task<UsuarioDTO?> ObtenerUsuarioCompletoPorId(string identityUserId)
        {
            // 1. Consulta LINQ
            var query = from user in _context.Users
                        
                        // Left Join Alumnos
                        join alumno in _context.Set<Alumno>()
                            on user.Id equals alumno.IdentityUserId into alumnoGroup
                        from al in alumnoGroup.DefaultIfEmpty()

                        // Left Join Carrera (anidado al alumno)
                        join carrera in _context.Set<Carrera>()
                            on al.CarreraId equals carrera.Id into carreraGroup
                        from car in carreraGroup.DefaultIfEmpty()

                        // Left Join Personal
                        join personal in _context.Set<Personal>()
                            on user.Id equals personal.IdentityUserId into personalGroup
                        from per in personalGroup.DefaultIfEmpty()

                        where user.Id == identityUserId

                        // Proyeccion a objeto anonimo
                        select new
                        {
                            Identity = user,
                            Alumno = al,
                            Carrera = car,
                            Personal = per
                        };

            var data = await query.AsNoTracking().FirstOrDefaultAsync();

            if (data == null) return null;

            // 2. Decidir qué DTO instanciar según los datos encontrados

            // CASO A: Es Alumno
            if (data.Alumno != null)
            {
                return new AlumnoDTO
                {
                    // Propiedades Base
                    IdentityUserId = data.Identity.Id,
                    Nombre = data.Alumno.Nombre,
                    ApellidoPaterno = data.Alumno.ApellidoPaterno,
                    ApellidoMaterno = data.Alumno.ApellidoMaterno,
                    Correo = data.Identity.Email,
                    CorreoConfirmado = data.Identity.EmailConfirmed,
                    Celular = data.Identity.PhoneNumber,
                    Rol = "Alumno",

                    // Propiedades específicas
                    NumeroBoleta = data.Alumno.NumeroBoleta,
                    PeriodoIngreso = data.Alumno.PeriodoIngreso,
                    Carrera = data.Carrera
                };
            }

            // CASO B: Es Personal
            if (data.Personal != null)
            {
                return new PersonalDTO
                {
                    // Propiedades Base
                    IdentityUserId = data.Identity.Id,
                    Nombre = data.Personal.Nombre,
                    ApellidoPaterno = data.Personal.ApellidoPaterno,
                    ApellidoMaterno = data.Personal.ApellidoMaterno,
                    Correo = data.Identity.Email,
                    CorreoConfirmado = data.Identity.EmailConfirmed,
                    Celular = data.Identity.PhoneNumber,
                    Rol = "Personal",

                    // Propiedades Específicas
                    IdEmpleado = data.Personal.IdEmpleado,
                    Area = data.Personal.Area
                };
            }

            // CASO C: Usuario Genérico (Admin)
            return new UsuarioDTO
            {
                IdentityUserId = data.Identity.Id,
                Nombre = "Administrador",
                Correo = data.Identity.Email,
                CorreoConfirmado = data.Identity.EmailConfirmed,
                Celular = data.Identity.PhoneNumber,
                Rol = "Administrador"
            };
        }
        public async Task<int> ObtenerCarreraAlumnoPorId(string identityUserId)
        {
            return await _context.Set<Alumno>()
                .Where(a => a.IdentityUserId == identityUserId)
                .Select(a => a.CarreraId)
                .FirstOrDefaultAsync();
        }
        public async Task<Documento> ObtenerDatosDocumentoPorId(int id)
        {
            return await _context.Set<Documento>()
                .Include(d => d.Tramite)
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.Id == id);
        }
    }
}
