using DAL.Entity;
using Common.Model.Dto;

namespace DAL.Extensions
{
    /// <summary>
    /// Exension methods for the Location entity and dto
    /// </summary>
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
                Id = entity.LocationId,
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
                LocationId = dto.Id,
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
