using DAL.Entity;
using Model.Dto;

namespace DAL.Extensions
{
    public static partial class Extensions
    {
        public static FloorDto EntityToDto(this Floor entity)
        {
            if (entity == null)
            {
                return null;
            }

            return new FloorDto
            {
                Id = entity.FloorID,
                Building = entity.Building?.EntityToDto(),
                BuildingId = entity.BuildingID,
                Locked = entity.Locked,
                Name = entity.FloorName
            };
        }

        public static Floor DtoToEntity(this FloorDto dto)
        {
            if (dto == null)
            {
                return null;
            }

            return new Floor
            {
                FloorID = dto.Id,
                FloorName = dto.Name,
                Building = dto.Building?.DtoToEntity(),
                BuildingID = dto.BuildingId,
                Locked = dto.Locked
            };
        }

        public static void UpdateEntityByDto(this Floor entity, FloorDto dto)
        {
            if (dto == null || entity == null)
            {
                return;
            }

            entity.FloorName = dto.Name;
            entity.BuildingID = dto.BuildingId;
            entity.Building.UpdateEntityByDto(dto.Building);
            entity.Locked = dto.Locked;
        }
    }
}
