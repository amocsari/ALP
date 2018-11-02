using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Context;
using DAL.Entity;
using DAL.Extensions;
using Microsoft.EntityFrameworkCore;
using Model.Dto;

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
            catch (Exception e)
            {
                //TODO: logging
                return null;
            }
        }

        public async Task DeleteItemStateById(int itemStateId)
        {
            try
            {
                await Remove(itemState => itemState.ItemStateID == itemStateId);
            }
            catch (Exception e)
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
            catch (Exception e)
            {
                //TODO: logging
                return null;
            }
        }

        public async Task<ItemStateDto> GetItemStateById(int itemStateId)
        {
            try
            {
                var entity = await GetSingle(itemState => itemState.ItemStateID == itemStateId);
                return entity.EntityToDto();
            }
            catch (Exception e)
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
            catch (Exception e)
            {
                //TODO: logging
            }
        }

        public async Task<ItemStateDto> UpdateItemState(ItemStateDto dto)
        {
            try
            {
                var updatedEntity = await _context.ItemState.FirstOrDefaultAsync(itemState => itemState.ItemStateID == dto.Id);
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
