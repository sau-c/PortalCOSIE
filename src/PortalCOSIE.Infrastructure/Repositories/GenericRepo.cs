using Microsoft.EntityFrameworkCore;
using PortalCOSIE.Domain.Entities;
using PortalCOSIE.Domain.Interfaces;
using PortalCOSIE.Infrastructure.Data;
using System.Linq.Expressions;

namespace PortalCOSIE.Infrastructure.Repositories
{
    public class GenericRepo<T> : IGenericRepo<T> where T : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepo(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet.AsQueryable();
        }

        public T? GetById(int id)
        {
            return _dbSet.Find(id);
        }
        //public T? Get(Expression<Func<T, bool>> predicate)
        //{
        //    return _dbSet.FirstOrDefault(predicate);
        //}
        //public IEnumerable<T> GetList(Expression<Func<T, bool>> predicate)
        //{
        //    return _dbSet.Where<T>(predicate).ToList();
        //}
        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }
        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }
        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
