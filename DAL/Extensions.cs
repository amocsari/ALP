using System;
using System.Linq;
using System.Linq.Expressions;

namespace DAL
{
    public static class Extensions
    {
        public static IQueryable<T> Include<T>(this IQueryable<T> query,
            params Expression<Func<T, object>>[] navigationProperties) where T : class
        {
            foreach (var navigationProperty in navigationProperties)
            {
                query.Include(navigationProperty);
            }

            return query;
        }
    }
}
