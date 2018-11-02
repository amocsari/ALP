using DAL.Entity;
using Model.Dto;

namespace DAL.Extensions
{
    public static partial class Extensions
    {
        public static LocationDto EntityToDto(this Location entity)
        {
            if(entity == null)
            {
                return null;
            }

            return new LocationDto
            {
                Name = entity.LocationName,
                Id = entity.LocationID,
                Locked = entity.Locked
            };
        }

        public static Location DtoToEntity(this LocationDto dto)
        {
            if(dto == null)
            {
                return null;
            }

            return new Location
            {
                LocationName = dto.Name,
                LocationID = dto.Id,
                Locked = dto.Locked,
            };
        }

        public static void UpdateEntityByDto(this Location entity, LocationDto dto)
        {
            if(dto == null || entity == null)
            {
                return;
            }

            entity.LocationName = dto.Name;
            entity.Locked = dto.Locked;
        }
    }
}
