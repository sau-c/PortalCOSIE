using PortalCOSIE.Application.DTO.Cuenta;
using PortalCOSIE.Application.Interfaces;
using PortalCOSIE.Domain.Entities;
using PortalCOSIE.Domain.Interfaces;

namespace PortalCOSIE.Application
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IGenericRepo<Usuario> _usuarioRepository;
        private readonly IGenericRepo<Alumno> _alumnoRepository;

        public UsuarioService(IGenericRepo<Usuario> usuarioRepository, IGenericRepo<Alumno> alumnoRepository)
        {
            _usuarioRepository = usuarioRepository;
            _alumnoRepository = alumnoRepository;
        }

        public Usuario? BuscarUsuarioPorIdentityId(string id)
        {
            return _usuarioRepository.GetAll().Where(u => u.IdentityUserId == id).FirstOrDefault();
            //return _usuarioRepository.Get(u=>u.IdentityUserId == id);
        }
        public AlumnoDTO? BuscarAlumnoPorId(string id)
        {
            //var testQuery = _usuarioRepository.Get(u => u.Alumno != null && u.IdentityUserId == id);
            var query = from usuario in _usuarioRepository.GetAll()
                        join alumno in _alumnoRepository.GetAll()
                        on usuario.Id equals alumno.Id into alumnoJoin
                        from alumno in alumnoJoin.DefaultIfEmpty() // Left join
                        where usuario.IdentityUserId == id
                        select new AlumnoDTO
                        {
                            Nombre = usuario.Nombre,
                            ApellidoPaterno = usuario.ApellidoPaterno,
                            ApellidoMaterno = usuario.ApellidoMaterno,

                            // Datos del alumno (si existe)
                            NumeroBoleta = alumno != null ? alumno.NumeroBoleta : "Dato no encontrado",
                            Carrera = alumno != null ? alumno.Carrera.Nombre : "Dato no encontrado",
                            FechaIngreso = alumno != null ? alumno.FechaIngreso.Date : default(DateTime),
                            PlanEstudio = alumno != null ? alumno.PlanEstudio.Nombre : "Dato no encontrado"
                        };
            return query.FirstOrDefault();
        }
        public Result<string> RegistrarAlumno(RegistrarDTO dto, string userId)
        {
            var usuario = new Usuario
            {
                IdentityUserId = userId,
                Nombre = dto.Nombre,
                ApellidoPaterno = dto.ApellidoPaterno,
                ApellidoMaterno = dto.ApellidoMaterno
            };

            _usuarioRepository.Add(usuario);
            _usuarioRepository.Save();

            var alumno = new Alumno
            {
                Id = usuario.Id,
                NumeroBoleta = dto.NumeroBoleta,
                FechaIngreso = dto.FechaIngreso,
                CarreraId = dto.CarreraId,
                PlanEstudioId = dto.PlanEstudioId
            };
            _alumnoRepository.Add(alumno);
            _alumnoRepository.Save();
            return Result<string>.Success(alumno.NumeroBoleta);
        }
    }
}