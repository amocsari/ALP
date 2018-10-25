using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DAL.Service
{
    public interface IBaseService<T> where T : class
    {
        Task<T> InsertNew(T entity);
        Task Remove(Expression<Func<T, bool>> expression);
        Task<T> GetSingle(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] navigationProperties);
        Task<List<T>> GetAll();
        Task<List<T>> GetByExpression(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] navigationProperties);
        Task<T> Update(T entity);
    }
}
