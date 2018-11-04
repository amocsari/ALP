using DAL.Entity;
using Common.Model.Dto;

namespace DAL.Extensions
{
    public static partial class Extensions
    {
        public static BuildingDto EntityToDto(this Building entity)
        {
            if (entity == null)
            {
                return null;
            }

            return new BuildingDto
            {
                Id = entity.BuildingID,
                Location = entity.Location?.EntityToDto(),
                LocationId = entity.LocationID,
                Locked = entity.Locked,
                Name = entity.BuildingName
            };
        }

        public static Building DtoToEntity(this BuildingDto dto)
        {
            if(dto == null)
            {
                return null;
            }

            return new Building
            {
                BuildingID = dto.Id,
                BuildingName = dto.Name,
                Location = dto.Location?.DtoToEntity(),
                LocationID = dto.LocationId,
                Locked = dto.Locked
            };
        }

        public static void UpdateEntityByDto(this Building entity, BuildingDto dto)
        {
            if(dto == null || entity == null)
            {
                return;
            }

            entity.BuildingName = dto.Name;
            entity.LocationID = dto.LocationId;
            entity.Location.UpdateEntityByDto(dto.Location);
            entity.Locked = dto.Locked;
        }
    }
}
