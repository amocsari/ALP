using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace DAL.Extensions
{
    public static partial class Extensions
    {
        public static IQueryable<T> Include<T>(this IQueryable<T> query,
            params Expression<Func<T, object>>[] navigationProperties) where T : class
        {
            foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
            {
                query.Include<T,object>(navigationProperty);
            }

            return query;
        }
    }
}
