using Microsoft.EntityFrameworkCore;
using PortalCOSIE.Domain.Entities.Tramites;
using PortalCOSIE.Infrastructure.Data;

namespace PortalCOSIE.Infrastructure.Repositories
{
    public class TramiteRepository : BaseRepository<Tramite, int>, ITramiteRepository
    {
        public TramiteRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Tramite>> ListarConDatosCompletos(int? alumnoId = null, int? personalId = null)
        {
            // 1. Iniciamos la consulta sin ejecutarla (IQueryable)
            var query = _context.Set<Tramite>()
                .Include(t => t.TipoTramite)
                .Include(t => t.EstadoTramite)
                .Include(t => t.Alumno)
                .Include(t => t.Personal)
                .AsQueryable();

            // 2. Filtro: Si es ALUMNO, filtramos solo sus trámites
            if (alumnoId.HasValue)
            {
                query = query.Where(t => t.AlumnoId == alumnoId.Value);
            }

            // 3. Filtro: Si es PERSONAL
            if (personalId.HasValue)
            {
                // Lógica: Traer los míos (personalId) y los que no tienen nadie (null)
                query = query.Where(t => t.PersonalId == personalId.Value || t.PersonalId == null);
            }

            // 4. Ordenamiento y Optimización final
            return await query
                .OrderByDescending(t => t.FechaSolicitud)
                .AsSplitQuery()
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<DetalleCTCE> BuscarDetalleCTCEPorId(int tramiteId)
        {
            return await _context.Set<DetalleCTCE>()
                .Include(d => d.Alumno)
                .ThenInclude(a => a.Carrera)
                .Include(d => d.UnidadesReprobadas)
                .ThenInclude(d => d.UnidadAprendizaje)
                .AsSplitQuery()
                .FirstOrDefaultAsync(d => d.Id == tramiteId);
        }
    }
}
