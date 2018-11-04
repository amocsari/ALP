using DAL.Entity;
using Common.Model.Dto;

namespace DAL.Extensions
{
    public static partial class Extensions
    {
        public static ItemNatureDto EntityToDto(this ItemNature entity)
        {
            if (entity == null)
            {
                return null;
            }

            return new ItemNatureDto
            {
                Name = entity.ItemNatureName,
                Id = entity.ItemNatureID,
                Locked = entity.Locked
            };
        }

        public static ItemNature DtoToEntity(this ItemNatureDto dto)
        {
            if (dto == null)
            {
                return null;
            }

            return new ItemNature
            {
                ItemNatureName = dto.Name,
                ItemNatureID = dto.Id,
                Locked = dto.Locked,
            };
        }

        public static void UpdateEntityByDto(this ItemNature entity, ItemNatureDto dto)
        {
            if (dto == null || entity == null)
            {
                return;
            }

            entity.ItemNatureName = dto.Name;
            entity.Locked = dto.Locked;
        }
    }
}
