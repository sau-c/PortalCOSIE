using Microsoft.EntityFrameworkCore;
using PortalCOSIE.Domain.Entities;
using PortalCOSIE.Domain.Interfaces;
using PortalCOSIE.Infrastructure.Data;
using System.Linq.Expressions;

namespace PortalCOSIE.Infrastructure.Repositories
{
    public class UnidadAprendizajeRepo : IGenericRepo<UnidadAprendizaje>
    {
        private readonly AppDbContext _context;
        private readonly DbSet<UnidadAprendizaje> _dbSet;

        public UnidadAprendizajeRepo(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<UnidadAprendizaje>();
        }

        public async Task<UnidadAprendizaje> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<UnidadAprendizaje>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public IQueryable<UnidadAprendizaje> Query()
        {
            return _dbSet.AsQueryable();
        }

        public async Task<UnidadAprendizaje> AddAsync(UnidadAprendizaje entity)
        {
            await _dbSet.AddAsync(entity);
            return entity;
        }

        public async Task UpdateAsync(UnidadAprendizaje entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public async Task DeleteAsync(UnidadAprendizaje entity)
        {
            _dbSet.Remove(entity);
        }

        public Task<UnidadAprendizaje?> GetFirstOrDefaultAsync(Expression<Func<UnidadAprendizaje, bool>> filter, params Expression<Func<UnidadAprendizaje, object>>[] includes)
        {
            throw new NotImplementedException();
        }
    }
}
