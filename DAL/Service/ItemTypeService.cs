using DAL.Entity;
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
    public class ItemTypeService : IItemTypeService
    {
        private IAlpContext _context;

        public ItemTypeService(IAlpContext context)
        {
            _context = context;
        }

        public async Task DeleteItemTypeById(int itemTypeId)
        {
            try
            {
                var entity = await _context.ItemType.FirstOrDefaultAsync(itemType => itemType.ItemNatureId == itemTypeId);
                _context.ItemType.Remove(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                //TODO: logging
            }
        }

        public async Task<List<ItemTypeDto>> GetAllItemTypes()
        {
            try
            {
                var itemTypes = await _context.ItemType.AsNoTracking().Include(itemType => itemType.ItemNature).ToListAsync();
                return itemTypes.Select(itemType => itemType.EntityToDto()).ToList();
            }
            catch (Exception)
            {
                //TODO: logging
                return null;
            }
        }

        public async Task<List<ItemTypeDto>> GetAvailableItemTypes()
        {
            try
            {
                var itemTypes = await _context.ItemType.AsNoTracking().Include(itemType => itemType.ItemNature).Where(itemType => !itemType.Locked).ToListAsync();
                return itemTypes.Select(itemType => itemType.EntityToDto()).ToList();
            }
            catch (Exception)
            {
                //TODO: logging
                return null;
            }
        }

        public async Task<ItemTypeDto> GetItemTypeById(int itemTypeId)
        {
            try
            {
                var entity = await _context.ItemType.FirstOrDefaultAsync(itemType => itemType.ItemTypeId == itemTypeId);
                return entity.EntityToDto();
            }
            catch (Exception)
            {
                //TODO: logging
                return null;
            }
        }

        public async Task<ItemTypeDto> InsertNewItemType(ItemTypeDto itemType)
        {
            try
            {
                var entity = itemType.DtoToEntity();
                await _context.ItemType.AddAsync(entity);
                await _context.SaveChangesAsync();
                return entity.EntityToDto();
            }
            catch (Exception)
            {
                //TODO: logging
                return null;
            }
        }

        public async Task<ItemTypeDto> UpdateItemType(ItemTypeDto dto)
        {
            try
            {
                var updatedEntity = await _context.ItemType.FirstOrDefaultAsync(itemType => itemType.ItemTypeId == dto.Id);
                updatedEntity.UpdateEntityByDto(dto);
                await _context.SaveChangesAsync();
                return updatedEntity.EntityToDto();
            }
            catch (Exception)
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
            catch (Exception)
            {
                //TODO: logging
            }
        }
    }
}
