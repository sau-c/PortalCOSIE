using Microsoft.EntityFrameworkCore;
using PortalCOSIE.Domain.Entities.Usuarios;
using PortalCOSIE.Domain.Interfaces;
using PortalCOSIE.Infrastructure.Data;

namespace PortalCOSIE.Infrastructure.Repositories
{
    public class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(AppDbContext context) : base(context)
        { }

        public async Task<Usuario> BuscarPorIdentityId(string identityUserId)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.IdentityUserId == identityUserId);
        }
        public async Task<Usuario> BuscarConAlumno(string identityUserId)
        {
            return await _dbSet
                .Include(u => u.Alumno!)
                .FirstOrDefaultAsync(u => u.IdentityUserId == identityUserId);
        }
        public async Task<Usuario> BuscarConAlumnoYCarrera(string identityUserId)
        {
            return await _dbSet
                .Include(u => u.Alumno!)
                .ThenInclude(a => a.Carrera!)
                .FirstOrDefaultAsync(u => u.IdentityUserId == identityUserId);
        }
        public async Task<Usuario> BuscarAlumnoPorBoleta(string boleta)
        {
            return await _dbSet
                .Include(u => u.Alumno!)
                .FirstOrDefaultAsync(u => u.Alumno!.NumeroBoleta == boleta);
        }
        public async Task<IEnumerable<Usuario>> ListarConAlumnoYCarrera()
        {
            return await _dbSet
                .Where(u => u.Alumno != null && u.Personal == null)
                .Include(u => u.Alumno!)
                .ThenInclude(a => a.Carrera!)
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<IEnumerable<Usuario>> ListarConPersonal()
        {
            return await _dbSet
                .Where(u => u.Personal != null && u.Alumno == null)
                .Include(u => u.Personal)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
