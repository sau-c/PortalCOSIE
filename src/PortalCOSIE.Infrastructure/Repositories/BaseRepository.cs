using Microsoft.EntityFrameworkCore;
using PortalCOSIE.Domain.Entities;
using PortalCOSIE.Domain.Interfaces;
using PortalCOSIE.Infrastructure.Data;
using System.Linq.Expressions;

namespace PortalCOSIE.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext _context;

        public BaseRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var query = _context.Set<T>().AsQueryable();

            return await query.FirstOrDefaultAsync(e => e.Id == id);
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var query = _context.Set<T>().AsQueryable();

            return await query.ToListAsync();
        }
        public async Task<T> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            return entity;
        }
        public T Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            return entity;
        }
        
        // Para eager loading
        public IQueryable<T> Query()
        {
            return _context.Set<T>().AsQueryable();
        }
        public async Task<T?> GetFirstOrDefaultWhereAsync(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes)
        {
            var query = _context.Set<T>().AsQueryable();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(filter);
        }
        public async Task<IEnumerable<T?>> GetAllWhereAsync(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes)
        {
            var query = _context.Set<T>().AsQueryable();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.Where(filter).AsNoTracking().ToListAsync();
        }
    }
}
