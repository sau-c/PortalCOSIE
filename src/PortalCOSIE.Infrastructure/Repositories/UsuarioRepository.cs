using Microsoft.EntityFrameworkCore;
using PortalCOSIE.Domain.Entities.Usuarios;
using PortalCOSIE.Infrastructure.Data;

namespace PortalCOSIE.Infrastructure.Repositories
{
    public class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(AppDbContext context) : base(context)
        { }

        public async Task<Usuario> BuscarPorIdentityId(string identityUserId)
        {
            return await _context.Set<Usuario>().FirstOrDefaultAsync(u => u.IdentityUserId == identityUserId);
        }
        public async Task<Alumno> BuscarAlumnoConCarrera(string identityUserId)
        {
            return await _context.Set<Alumno>()
                .Include(a => a.Carrera!)
                .FirstOrDefaultAsync(u => u.IdentityUserId == identityUserId);
        }
        public async Task<Alumno> BuscarAlumnoPorBoleta(string boleta)
        {
            return await _context.Set<Alumno>()
                .FirstOrDefaultAsync(u => u.NumeroBoleta == boleta);
        }
        public async Task<IEnumerable<Alumno>> ListarAlumnoConCarrera()
        {
            return await _context.Set<Alumno>()
                .Include(a => a.Carrera!)
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<IEnumerable<Personal>> ListarConPersonal()
        {
            return await _context.Set<Personal>()
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
