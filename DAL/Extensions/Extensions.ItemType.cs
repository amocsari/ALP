using DAL.Entity;
using Common.Model.Dto;

namespace DAL.Extensions
{
    public static partial class Extensions
    {
        public static ItemTypeDto EntityToDto(this ItemType entity)
        {
            if (entity == null)
            {
                return null;
            }

            return new ItemTypeDto
            {
                Id = entity.ItemTypeID,
                ItemNature = entity.ItemNature?.EntityToDto(),
                ItemNatureId = entity.ItemNatureID,
                Locked = entity.Locked,
                Name = entity.ItemTypeName
            };
        }

        public static ItemType DtoToEntity(this ItemTypeDto dto)
        {
            if (dto == null)
            {
                return null;
            }

            return new ItemType
            {
                ItemTypeID = dto.Id,
                ItemTypeName = dto.Name,
                ItemNature = dto.ItemNature?.DtoToEntity(),
                ItemNatureID = dto.ItemNatureId,
                Locked = dto.Locked
            };
        }

        public static void UpdateEntityByDto(this ItemType entity, ItemTypeDto dto)
        {
            if (dto == null || entity == null)
            {
                return;
            }

            entity.ItemTypeName = dto.Name;
            entity.ItemNatureID = dto.ItemNatureId;
            entity.ItemNature.UpdateEntityByDto(dto.ItemNature);
            entity.Locked = dto.Locked;
        }
    }
}
