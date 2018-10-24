using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DAL.Context;

namespace DAL.Service
{
    public abstract class BaseService<T> : IBaseService<T> where T : class
    {
        protected IAlpContext _context;

        public async Task<List<T>> GetAll()
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<List<T>> GetByExpression(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] navigationProperties)
        {
            return await _context.Set<T>().AsNoTracking().Include(navigationProperties).Where(expression).ToListAsync();
        }

        public async Task<T> GetSingle(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] navigationProperties)
        {
            return await _context.Set<T>().AsNoTracking().Include(navigationProperties).FirstOrDefaultAsync(expression);
        }

        public async Task<T> InsertNew(T entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task Remove(Expression<Func<T, bool>> expression)
        {
            var entity = await _context.Set<T>().FirstOrDefaultAsync(expression);
            if (entity != null)
            {
                _context.Remove(entity);
            }
        }

        public async Task Update(Expression<Func<T, bool>> expression, T newValue)
        {
            var entities = _context.Set<T>().Where(expression);
            await entities.ForEachAsync(entity => entity = newValue);
            await _context.SaveChangesAsync();
        }
    }
}
