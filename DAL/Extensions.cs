using DAL.Entity;
using Model;
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

        public static LocationDto EntityToDto(this Location location)
        {
            return new LocationDto
            {
                Name = location.LocationName,
                Id = location.LocationID
            };
        }

        public static Location DtoToEntity(this LocationDto dto)
        {
            return new Location
            {
                LocationName = dto.Name,
                LocationID = dto.Id
            };
        }
    }
}
