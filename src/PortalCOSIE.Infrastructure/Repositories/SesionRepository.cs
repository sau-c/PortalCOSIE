using Microsoft.EntityFrameworkCore;
using PortalCOSIE.Domain.Entities.Calendario;
using PortalCOSIE.Infrastructure.Data;

namespace PortalCOSIE.Infrastructure.Repositories
{
    public class SesionRepository : BaseRepository<SesionCOSIE>, ISesionRepository
    {
        public SesionRepository(AppDbContext context) : base(context)
        { }
        public async Task<IEnumerable<SesionCOSIE>> ListarSesiones(bool filtrarActivos)
        {
            return await _context.Set<SesionCOSIE>()
                .Where(s => filtrarActivos ? s.IsDeleted == false : true)
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
