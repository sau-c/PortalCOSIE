using Microsoft.EntityFrameworkCore;
using PortalCOSIE.Domain.Entities;
using PortalCOSIE.Domain.Interfaces;
using PortalCOSIE.Infrastructure.Data;
using System.Linq.Expressions;

namespace PortalCOSIE.Infrastructure.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public BaseRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id), "El Id no puede ser menor o igual a 0");
            return await _dbSet.FindAsync(id);
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync(bool filtrarActivos)
        {
            return await _dbSet
                .Where(e => filtrarActivos ? e.IsDeleted == false : true)
                .ToListAsync();
        }
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            return entity;
        }
        public TEntity Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
            return entity;
        }
    }
}
