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
    public class ItemStateService : BaseService<ItemState>, IItemStateService
    {
        public ItemStateService(IAlpContext context)
        {
            _context = context;
        }

        public async Task<ItemStateDto> AddNewItemState(ItemStateDto itemState)
        {
            try
            {
                var entity = await InsertNew(itemState.DtoToEntity());
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
                await Remove(itemState => itemState.ItemStateId == itemStateId);
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
                var entites = await GetAll();
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
                var entites = await GetByExpression(itemState => !itemState.Locked);
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
                var entity = await GetSingle(itemState => itemState.ItemStateId == itemStateId);
                return entity.EntityToDto();
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
    }
}
