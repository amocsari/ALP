using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Context;
using DAL.Entity;
using DAL.Extensions;
using Microsoft.EntityFrameworkCore;
using Common.Model.Dto;

namespace DAL.Service
{
    public class ItemNatureService : IItemNatureService
    {
        private IAlpContext _context;

        public ItemNatureService(IAlpContext context)
        {
            _context = context;
        }

        public async Task<ItemNatureDto> AddNewItemNature(ItemNatureDto itemNature)
        {
            try
            {
                var entity = itemNature.DtoToEntity();
                await _context.ItemNature.AddAsync(entity);
                await _context.SaveChangesAsync();
                return entity.EntityToDto();
            }
            catch (Exception)
            {
                //TODO: logging
                return null;
            }
        }

        public async Task DeleteItemNatureById(int itemNatureId)
        {
            try
            {
                var entity = await _context.ItemNature.FirstOrDefaultAsync(itemNature => itemNature.ItemNatureId == itemNatureId);
                _context.ItemNature.Remove(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                //TODO: logging
            }
        }

        public async Task<List<ItemNatureDto>> GetAllItemNatures()
        {
            try
            {
                var entites = await _context.ItemNature.AsNoTracking().ToListAsync();
                return entites.Select(e => e.EntityToDto()).ToList();
            }
            catch (Exception)
            {
                //TODO: logging
                return null;
            }
        }

        public async Task<List<ItemNatureDto>> GetAvailableItemNatures()
        {
            try
            {
                var entites = await _context.ItemNature.AsNoTracking().Where(itemNature => !itemNature.Locked).ToListAsync();
                return entites.Select(e => e.EntityToDto()).ToList();

            }
            catch (Exception)
            {
                //TODO: logging
                return null;
            }
        }

        public async Task<ItemNatureDto> GetItemNatureById(int itemNatureId)
        {
            try
            {
                var entity = await _context.ItemNature.FirstOrDefaultAsync(itemNature => itemNature.ItemNatureId == itemNatureId);
                return entity.EntityToDto();
            }
            catch (Exception)
            {
                //TODO: logging
                return null;
            }
        }

        public async Task<ItemNatureDto> UpdateItemNature(ItemNatureDto dto)
        {
            try
            {
                var updatedEntity = await _context.ItemNature.FirstOrDefaultAsync(itemNature => itemNature.ItemNatureId == dto.Id);
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

        public async Task ToggleItemNatureLockStateById(int itemNatureId)
        {
            try
            {
                var itemNature = await GetItemNatureById(itemNatureId);
                itemNature.Locked = !itemNature.Locked;
                await UpdateItemNature(itemNature);
            }
            catch (Exception)
            {
                //TODO: logging
            }
        }
    }
}
