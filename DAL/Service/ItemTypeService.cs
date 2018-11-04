﻿using DAL.Entity;
using DAL.Context;
using Common.Model.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using DAL.Extensions;
using Microsoft.EntityFrameworkCore;
using System;

namespace DAL.Service
{
    public class ItemTypeService : BaseService<ItemType>, IItemTypeService
    {
        public ItemTypeService(IAlpContext context)
        {
            _context = context;
        }

        public async Task DeleteItemTypeById(int itemTypeId)
        {
            try
            {
                await Remove(itemType => itemType.ItemTypeID == itemTypeId);
            }
            catch (Exception e)
            {
                //TODO: logging
            }
        }

        public async Task<List<ItemTypeDto>> GetAllItemTypes()
        {
            try
            {
                //var ItemTypes = await _context.ItemType.Include(ItemType => ItemType.ItemNature).ToListAsync();
                //return ItemTypes.Select(ItemType => ItemType.EntityToDto()).ToList();

                var itemTypes = await GetAll(itemType => itemType.ItemNature);
                return itemTypes.Select(itemType => itemType.EntityToDto()).ToList();
            }
            catch (Exception e)
            {
                //TODO: logging
                return null;
            }
        }

        public async Task<ItemTypeDto> GetItemTypeById(int itemTypeId)
        {
            try
            {
                var entity = await GetSingle(itemType => itemType.ItemTypeID == itemTypeId);
                return entity.EntityToDto();
            }
            catch (Exception e)
            {
                //TODO: logging
                return null;
            }
        }

        public async Task<ItemTypeDto> InsertNewItemType(ItemTypeDto itemType)
        {
            try
            {
                var entity = await InsertNew(itemType.DtoToEntity());
                return entity.EntityToDto();
            }
            catch (Exception e)
            {
                //TODO: logging
                return null;
            }
        }

        public async Task ToggleItemTypeLockStateById(int itemTypeId)
        {
            try
            {
                var itemType = await GetItemTypeById(itemTypeId);
                itemType.Locked = !itemType.Locked;
                await UpdateItemType(itemType);
            }
            catch (Exception e)
            {
                //TODO: logging
            }
        }

        public async Task<ItemTypeDto> UpdateItemType(ItemTypeDto dto)
        {
            try
            {
                var updatedEntity = await _context.ItemType.FirstOrDefaultAsync(itemType => itemType.ItemTypeID == dto.Id);
                updatedEntity.UpdateEntityByDto(dto);
                await _context.SaveChangesAsync();
                return updatedEntity.EntityToDto();
            }
            catch (Exception e)
            {
                //TODO: logging
                return null;
            }
        }
    }
}
