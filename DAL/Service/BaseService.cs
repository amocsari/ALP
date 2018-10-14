using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using DAL.Context;

namespace DAL.Service
{
    public abstract class BaseService<T> : IBaseService<T> where T : class
    {
        protected IAlpContext _context;

        public List<T> GetAll()
        {
            return _context.Set<T>().AsNoTracking().ToList();
        }

        public List<T> GetByExpression(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] navigationProperties)
        {
            return _context.Set<T>().AsNoTracking().Include(navigationProperties).Where(expression).ToList();
        }

        public T GetSingle(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] navigationProperties)
        {
            return _context.Set<T>().AsNoTracking().Include(navigationProperties).FirstOrDefault(expression);
        }

        public void InsertNew(T entity)
        {
            _context.Add(entity);
        }

        public void Remove(Expression<Func<T, bool>> expression)
        {
            var entity = _context.Set<T>().FirstOrDefault(expression);
            if (entity != null)
            {
                _context.Remove(entity);
            }
        }
    }
}
