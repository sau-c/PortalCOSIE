using Microsoft.EntityFrameworkCore;
using PortalCOSIE.Domain.Entities.Carreras;
using PortalCOSIE.Domain.Interfaces;
using PortalCOSIE.Infrastructure.Data;

namespace PortalCOSIE.Infrastructure.Repositories
{
    public class CarreraRepository : BaseRepository<Carrera>, ICarreraRepository
    {
        public CarreraRepository(AppDbContext context) : base(context)
        { }

        public async Task<Carrera> ObtenerCarreraConUnidadesAsync(string carrera)
        {
            return await _dbSet
                .Include(c => c.UnidadesAprendizaje)
                .FirstOrDefaultAsync(c => c.Nombre == carrera);
        }

        public async Task<IEnumerable<UnidadAprendizaje>> ListarUnidadesPorCarreraAsync(string carrera)
        {
            var carreraEntity = await _dbSet
                .Include(c => c.UnidadesAprendizaje)
                .FirstOrDefaultAsync(c => c.Nombre == carrera);
            return carreraEntity?.UnidadesAprendizaje ?? Enumerable.Empty<UnidadAprendizaje>();
        }
    }
}
