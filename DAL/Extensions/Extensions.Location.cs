using DAL.Entity;
using Model;

namespace DAL.Extensions
{
    public static partial class Extensions
    {
        public static LocationDto EntityToDto(this Location location)
        {
            return new LocationDto
            {
                Name = location.LocationName,
                Id = location.LocationID,
                Locked = location.Locked
            };
        }

        public static Location DtoToEntity(this LocationDto dto)
        {
            return new Location
            {
                LocationName = dto.Name,
                LocationID = dto.Id,
                Locked = dto.Locked,
            };
        }

        public static Location UpdateEntityByDto(this Location entity, LocationDto dto)
        {
            entity.LocationName = dto.Name;
            entity.Locked = dto.Locked;
            return entity;
        }
    }
}
