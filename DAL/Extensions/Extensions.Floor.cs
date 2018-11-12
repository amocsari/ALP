using DAL.Entity;
using Common.Model.Dto;

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
                Id = entity.FloorId,
                Building = entity.Building?.EntityToDto(),
                BuildingId = entity.BuildingId,
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
                FloorId = dto.Id,
                FloorName = dto.Name,
                Building = dto.Building?.DtoToEntity(),
                BuildingId = dto.BuildingId,
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
            entity.BuildingId = dto.BuildingId;
            entity.Building.UpdateEntityByDto(dto.Building);
            entity.Locked = dto.Locked;
        }
    }
}
