using Microsoft.EntityFrameworkCore;
using PortalCOSIE.Domain.Interfaces;
using PortalCOSIE.Domain.SharedKernel;
using PortalCOSIE.Infrastructure.Persistence;

namespace PortalCOSIE.Infrastructure.Repositories
{
    public class BaseRepository<TEntity, TId> : IBaseRepository<TEntity,TId> where TEntity : BaseEntity<TId>
    {
        protected readonly AppDbContext _context;

        public BaseRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<TEntity> GetByIdAsync(TId id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync(bool IncluirEliminados = false)
        {
            var query = _context.Set<TEntity>().AsQueryable();
            if (!IncluirEliminados)
                query = query.Where(e => !e.IsDeleted);

            return await query.AsNoTracking().ToListAsync();
        }
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            return entity;
        }
        public TEntity Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            return entity;
        }
    }
}
