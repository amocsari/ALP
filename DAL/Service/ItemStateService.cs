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
    public class ItemStateService : IItemStateService
    {
        private IAlpContext _context;

        public ItemStateService(IAlpContext context)
        {
            _context = context;
        }

        public async Task<ItemStateDto> AddNewItemState(ItemStateDto itemState)
        {
            try
            {
                var entity = itemState.DtoToEntity();
                await _context.ItemState.AddAsync(entity);
                await _context.SaveChangesAsync();
                return entity.EntityToDto();
            }
            catch (Exception)
            {
                //TODO: logging
                return null;
            }
        }

        public async Task DeleteItemStateById(int itemStateId)
        {
            try
            {
                var entity = await _context.ItemState.FirstOrDefaultAsync(itemState => itemState.ItemStateId == itemStateId);
                _context.Remove(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                //TODO: logging
            }
        }

        public async Task<List<ItemStateDto>> GetAllItemStates()
        {
            try
            {
                var entites = await _context.ItemState.AsNoTracking().ToListAsync();
                return entites.Select(e => e.EntityToDto()).ToList();
            }
            catch (Exception)
            {
                //TODO: logging
                return null;
            }
        }

        public async Task<List<ItemStateDto>> GetAvailableItemStates()
        {
            try
            {
                var entites = await _context.ItemState.AsNoTracking().Where(itemState => !itemState.Locked).ToListAsync();
                return entites.Select(e => e.EntityToDto()).ToList();
            }
            catch (Exception)
            {
                //TODO: logging
                return null;
            }
        }

        public async Task<ItemStateDto> GetItemStateById(int itemStateId)
        {
            try
            {
                var entity = await _context.ItemState.FirstOrDefaultAsync(itemState => itemState.ItemStateId == itemStateId);
                return entity.EntityToDto();
            }
            catch (Exception)
            {
                //TODO: logging
                return null;
            }
        }

        public async Task<ItemStateDto> UpdateItemState(ItemStateDto dto)
        {
            try
            {
                var updatedEntity = await _context.ItemState.FirstOrDefaultAsync(itemState => itemState.ItemStateId == dto.Id);
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

        public async Task ToggleItemStateLockStateById(int itemStateId)
        {
            try
            {
                var itemState = await GetItemStateById(itemStateId);
                itemState.Locked = !itemState.Locked;
                await UpdateItemState(itemState);
            }
            catch (Exception)
            {
                //TODO: logging
            }
        }
    }
}
