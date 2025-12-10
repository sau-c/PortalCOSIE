using Microsoft.EntityFrameworkCore;
using PortalCOSIE.Domain.Entities.Carreras;
using PortalCOSIE.Infrastructure.Data;

namespace PortalCOSIE.Infrastructure.Repositories
{
    public class CarreraRepository : BaseRepository<Carrera, int>, ICarreraRepository
    {
        public CarreraRepository(AppDbContext context) : base(context)
        { }

        public async Task<Carrera> ObtenerCarreraConUnidadesAsync(int carreraId)
        {
            return await _context.Set<Carrera>()
                .Include(c => c.UnidadesAprendizaje)
                .FirstOrDefaultAsync(c => c.Id == carreraId);
        }

        public async Task<IEnumerable<UnidadAprendizaje>> ListarUnidadesPorCarreraAsync(int carreraId)
        {
            return await _context.Set<Carrera>()
                .Where(c => c.Id == carreraId)
                .SelectMany(c => c.UnidadesAprendizaje)
                .Where(u => !u.IsDeleted)
                .Select(u => new UnidadAprendizaje(
                    u.Id, u.Nombre, u.CarreraId, u.Semestre
                    ))
                .ToListAsync();
            //return carreraEntity?.UnidadesAprendizaje ?? Enumerable.Empty<UnidadAprendizaje>();
        }
    }
}
