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
                Id = entity.BuildingId,
                Location = entity.Location?.EntityToDto(),
                LocationId = entity.LocationId,
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
                BuildingId = dto.Id,
                BuildingName = dto.Name,
                Location = dto.Location?.DtoToEntity(),
                LocationId = dto.LocationId,
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
            entity.LocationId = dto.LocationId;
            entity.Location.UpdateEntityByDto(dto.Location);
            entity.Locked = dto.Locked;
        }
    }
}
