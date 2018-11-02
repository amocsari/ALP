﻿using DAL.Entity;
using Model.Dto;

namespace DAL.Extensions
{
    public static partial class Extensions
    {
        public static ItemStateDto EntityToDto(this ItemState entity)
        {
            if (entity == null)
            {
                return null;
            }

            return new ItemStateDto
            {
                Name = entity.ItemStateName,
                Id = entity.ItemStateID,
                Locked = entity.Locked
            };
        }

        public static ItemState DtoToEntity(this ItemStateDto dto)
        {
            if (dto == null)
            {
                return null;
            }

            return new ItemState
            {
                ItemStateName = dto.Name,
                ItemStateID = dto.Id,
                Locked = dto.Locked,
            };
        }

        public static void UpdateEntityByDto(this ItemState entity, ItemStateDto dto)
        {
            if (dto == null || entity == null)
            {
                return;
            }

            entity.ItemStateName = dto.Name;
            entity.Locked = dto.Locked;
        }
    }
}
