using Microsoft.EntityFrameworkCore;
using PortalCOSIE.Domain.Entities.SesionesCOSIE;
using PortalCOSIE.Infrastructure.Persistence;

namespace PortalCOSIE.Infrastructure.Repositories
{
    public class SesionRepository : BaseRepository<SesionCOSIE, int>, ISesionRepository
    {
        public SesionRepository(AppDbContext context) : base(context)
        { }
        public async Task<IEnumerable<SesionCOSIE>> ListarSesiones(bool IncluirEliminados = false)
        {
            var query = _context.Set<SesionCOSIE>().AsQueryable();
            if (!IncluirEliminados)
                query = query.Where(e => !e.IsDeleted);

            return await query
                .Include(s => s.FechasRecepcion)
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<SesionCOSIE?> ObtenerConFechasRecepcion(int id)
        {
            return await _context.Set<SesionCOSIE>()
                .Where(s => s.Id == id)
                .Include(s => s.FechasRecepcion)
                .FirstOrDefaultAsync();
        }
    }
}
