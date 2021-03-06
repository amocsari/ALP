﻿using DAL.Entity;
using Common.Model.Dto;

namespace DAL.Extensions
{
    /// <summary>
    /// Exension methods for the ItemNature entity and dto
    /// </summary>
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
                Id = entity.ItemNatureId,
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
                ItemNatureId = dto.Id,
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
