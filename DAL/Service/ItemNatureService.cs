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
    public class ItemNatureService : BaseService<ItemNature>, IItemNatureService
    {
        public ItemNatureService(IAlpContext context)
        {
            _context = context;
        }

        public async Task<ItemNatureDto> AddNewItemNature(ItemNatureDto itemNature)
        {
            try
            {
                var entity = await InsertNew(itemNature.DtoToEntity());
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
                await Remove(itemNature => itemNature.ItemNatureId == itemNatureId);
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
                var entites = await GetAll();
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
                var entites = await GetByExpression(itemNature => !itemNature.Locked);
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
                var entity = await GetSingle(itemNature => itemNature.ItemNatureId == itemNatureId);
                return entity.EntityToDto();
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
    }
}
