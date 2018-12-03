using DAL.Entity;
using DAL.Context;
using Common.Model.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using DAL.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using Model.Model;

namespace DAL.Service
{
    public class ItemTypeService : IItemTypeService
    {
        private IAlpContext _context;

        public ItemTypeService(IAlpContext context)
        {
            _context = context;
        }

        public async Task<AlpApiResponse> DeleteItemTypeById(int itemTypeId)
        {
            var response = new AlpApiResponse();
            try
            {
                var entity = await _context.ItemType.FirstOrDefaultAsync(itemType => itemType.ItemNatureId == itemTypeId);
                _context.ItemType.Remove(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                //TODO: logging
                response.Message = e.Message;
                response.Success = false;
            }

            return response;
        }

        public async Task<AlpApiResponse<List<ItemTypeDto>>> GetAllItemTypes()
        {
            var response = new AlpApiResponse<List<ItemTypeDto>>();
            try
            {
                var itemTypes = await _context.ItemType.AsNoTracking().Include(itemType => itemType.ItemNature).ToListAsync();
                response.Value = itemTypes.Select(itemType => itemType.EntityToDto()).ToList();
            }
            catch (Exception e)
            {
                //TODO: logging
                response.Message = e.Message;
                response.Success = false;
            }

            return response;
        }

        public async Task<AlpApiResponse<List<ItemTypeDto>>> GetAvailableItemTypes()
        {
            var response = new AlpApiResponse<List<ItemTypeDto>>();
            try
            {
                var itemTypes = await _context.ItemType.AsNoTracking().Include(itemType => itemType.ItemNature).Where(itemType => !itemType.Locked).ToListAsync();
                response.Value = itemTypes.Select(itemType => itemType.EntityToDto()).ToList();
            }
            catch (Exception e)
            {
                //TODO: logging
                response.Message = e.Message;
                response.Success = false;
            }

            return response;
        }

        public async Task<AlpApiResponse<ItemTypeDto>> GetItemTypeById(int itemTypeId)
        {
            var response = new AlpApiResponse<ItemTypeDto>();
            try
            {
                var entity = await _context.ItemType.FirstOrDefaultAsync(itemType => itemType.ItemTypeId == itemTypeId);
                response.Value = entity.EntityToDto();
            }
            catch (Exception e)
            {
                //TODO: logging
                response.Message = e.Message;
                response.Success = false;
            }

            return response;
        }

        public async Task<AlpApiResponse<ItemTypeDto>> InsertNewItemType(ItemTypeDto itemType)
        {
            var response = new AlpApiResponse<ItemTypeDto>();
            try
            {
                var entity = itemType.DtoToEntity();
                entity.ItemNature = null;
                await _context.ItemType.AddAsync(entity);
                await _context.SaveChangesAsync();
                response.Value = entity.EntityToDto();
            }
            catch (Exception e)
            {
                //TODO: logging
                response.Message = e.Message;
                response.Success = false;
            }

            return response;
        }

        public async Task<AlpApiResponse> UpdateItemType(ItemTypeDto dto)
        {
            var response = new AlpApiResponse();
            try
            {
                var updatedEntity = await _context.ItemType.FirstOrDefaultAsync(itemType => itemType.ItemTypeId == dto.Id);
                updatedEntity.UpdateEntityByDto(dto);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                //TODO: logging
                response.Message = e.Message;
                response.Success = false;
            }

            return response;
        }

        public async Task<AlpApiResponse> ToggleItemTypeLockStateById(int itemTypeId)
        {
            var response = new AlpApiResponse();
            try
            {
                var getByIdReply = await GetItemTypeById(itemTypeId);
                if (!getByIdReply.Success)
                {
                    response.Success = getByIdReply.Success;
                    response.Message = getByIdReply.Message;
                    return response;
                }

                var itemType = getByIdReply.Value;

                itemType.Locked = !itemType.Locked;
                var updateReply = await UpdateItemType(itemType);
                if (!updateReply.Success)
                {
                    response.Success = updateReply.Success;
                    response.Message = updateReply.Message;
                    return response;
                }
            }
            catch (Exception e)
            {
                //TODO: logging
                response.Message = e.Message;
                response.Success = false;
            }

            return response;
        }
    }
}
