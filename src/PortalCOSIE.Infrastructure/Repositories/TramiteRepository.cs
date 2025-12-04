using Microsoft.EntityFrameworkCore;
using PortalCOSIE.Domain.Entities.Tramites;
using PortalCOSIE.Infrastructure.Data;

namespace PortalCOSIE.Infrastructure.Repositories
{
    public class TramiteRepository : BaseRepository<Tramite, int>, ITramiteRepository
    {
        public TramiteRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Tramite>> ListarConDatosCompletos()
        {
            return await _context.Set<Tramite>()
                .Include(t => t.TipoTramite)
                .Include(t => t.EstadoTramite)
                .Include(t => t.Personal)
                .AsSplitQuery() // Evita la explosión cartesiana
                .AsNoTracking() // Mejora de rendimiento para solo lectura
                .ToListAsync();
        }
    }
}
