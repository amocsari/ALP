using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DAL.Service
{
    public interface IBaseService<T> where T : class
    {
        void InsertNew(T entity);
        void Remove(Expression<Func<T, bool>> expression);
        T GetSingle(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] navigationProperties);
        List<T> GetAll();
        List<T> GetByExpression(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] navigationProperties);
    }
}
