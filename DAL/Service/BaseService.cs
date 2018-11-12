using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Context;
using DAL.Extensions;
using Microsoft.EntityFrameworkCore;

namespace DAL.Service
{
    public abstract class BaseService<T> : IBaseService<T> where T : class
    {
        protected IAlpContext _context;

        public async Task<List<T>> GetAll(params System.Linq.Expressions.Expression<Func<T, object>>[] navigationProperties)
        {
            var set = _context.Set<T>();

            var query = set.AsNoTracking().Include(navigationProperties);

            var sql = query.ToSql();

            return await query.ToListAsync();
        }

        public async Task<List<T>> GetByExpression(System.Linq.Expressions.Expression<Func<T, bool>> expression, params System.Linq.Expressions.Expression<Func<T, object>>[] navigationProperties)
        {
            var set = _context.Set<T>();

            return await set.AsNoTracking().Include(navigationProperties).Where(expression).ToListAsync();
        }

        public async Task<T> GetSingle(System.Linq.Expressions.Expression<Func<T, bool>> expression, params System.Linq.Expressions.Expression<Func<T, object>>[] navigationProperties)
        {
            var set = _context.Set<T>();

            var query = set.Include(navigationProperties);

            var sql = query.ToSql();

            return await query.FirstOrDefaultAsync(expression);
        }

        public async Task<T> InsertNew(T entity)
        {
            await _context.AddAsync(entity);
            return entity;
        }

        public async Task Remove(System.Linq.Expressions.Expression<Func<T, bool>> expression)
        {
            var entity = await GetSingle(expression);
            if (entity != null)
            {
                _context.Remove(entity);
            }
        }

        public async Task<T> Update(T entity)
        {
            return await _context.Update(entity);
        }
    }
}
