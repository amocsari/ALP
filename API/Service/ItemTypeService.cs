using DAL.Context;
using Common.Model.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using DAL.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using Model.Model;
using Microsoft.Extensions.Logging;

namespace API.Service
{
    /// <summary>
    /// handles itemtype related 
    /// </summary>
    public class ItemTypeService : IItemTypeService
    {
        private readonly IAlpContext _context;
        private readonly ILogger<ItemTypeService> _logger;

        public ItemTypeService(IAlpContext context, ILogger<ItemTypeService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<AlpApiResponse> DeleteItemTypeById(int itemTypeId)
        {
            var response = new AlpApiResponse();
            try
            {
                _logger.LogDebug(new
                {
                    action = nameof(DeleteItemTypeById),
                    itemTypeId
                }.ToString());

                var entity = await _context.ItemType.FirstOrDefaultAsync(itemType => itemType.ItemNatureId == itemTypeId);
                _context.ItemType.Remove(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(new
                {
                    exception = e,
                    message = e.Message,
                    innerException = e,
                    innerExceptionMessage = e.InnerException?.Message
                }.ToString());
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
                _logger.LogDebug(new
                {
                    action = nameof(GetAllItemTypes)
                }.ToString());

                var itemTypes = await _context.ItemType.AsNoTracking().Include(itemType => itemType.ItemNature).ToListAsync();
                response.Value = itemTypes.Select(itemType => itemType.EntityToDto()).ToList();
            }
            catch (Exception e)
            {
                _logger.LogError(new
                {
                    exception = e,
                    message = e.Message,
                    innerException = e,
                    innerExceptionMessage = e.InnerException?.Message
                }.ToString());
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
                _logger.LogDebug(new
                {
                    action = nameof(GetAvailableItemTypes)
                }.ToString());

                var itemTypes = await _context.ItemType.AsNoTracking().Include(itemType => itemType.ItemNature).Where(itemType => !itemType.Locked).ToListAsync();
                response.Value = itemTypes.Select(itemType => itemType.EntityToDto()).ToList();
            }
            catch (Exception e)
            {
                _logger.LogError(new
                {
                    exception = e,
                    message = e.Message,
                    innerException = e,
                    innerExceptionMessage = e.InnerException?.Message
                }.ToString());
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
                _logger.LogDebug(new
                {
                    action = nameof(GetItemTypeById),
                    itemTypeId
                }.ToString());

                var entity = await _context.ItemType.FirstOrDefaultAsync(itemType => itemType.ItemTypeId == itemTypeId);
                response.Value = entity.EntityToDto();
            }
            catch (Exception e)
            {
                _logger.LogError(new
                {
                    exception = e,
                    message = e.Message,
                    innerException = e,
                    innerExceptionMessage = e.InnerException?.Message
                }.ToString());
                response.Message = e.Message;
                response.Success = false;
            }

            return response;
        }

        public async Task<AlpApiResponse<ItemTypeDto>> InsertNewItemType(ItemTypeDto dto)
        {
            var response = new AlpApiResponse<ItemTypeDto>();
            try
            {
                _logger.LogDebug(new
                {
                    action = nameof(InsertNewItemType),
                    dto = dto?.ToString()
                }.ToString());

                dto.Validate();

                var itemNature = await _context.ItemNature.FirstOrDefaultAsync(itemN => itemN.ItemNatureId == dto.ItemNatureId);
                if (itemNature == null)
                {
                    throw new Exception("A típushoz tartozó eszköz jelleget kötelező megadni!");
                }

                var entity = dto.DtoToEntity();
                entity.ItemNature = null;
                await _context.ItemType.AddAsync(entity);
                await _context.SaveChangesAsync();
                response.Value = entity.EntityToDto();
            }
            catch (Exception e)
            {
                _logger.LogError(new
                {
                    exception = e,
                    message = e.Message,
                    innerException = e,
                    innerExceptionMessage = e.InnerException?.Message
                }.ToString());
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
                _logger.LogDebug(new
                {
                    action = nameof(UpdateItemType),
                    dto = dto?.ToString()
                }.ToString());

                dto.Validate();

                var itemNature = await _context.ItemNature.FirstOrDefaultAsync(itemN => itemN.ItemNatureId == dto.ItemNatureId);
                if(itemNature == null)
                {
                    throw new Exception("A típushoz tartozó eszköz jelleget kötelező megadni!");
                }

                var updatedEntity = await _context.ItemType.FirstOrDefaultAsync(itemType => itemType.ItemTypeId == dto.Id);
                updatedEntity.UpdateEntityByDto(dto);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(new
                {
                    exception = e,
                    message = e.Message,
                    innerException = e,
                    innerExceptionMessage = e.InnerException?.Message
                }.ToString());
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
                _logger.LogDebug(new
                {
                    action = nameof(ToggleItemTypeLockStateById),
                    itemTypeId
                }.ToString());

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
                _logger.LogError(new
                {
                    exception = e,
                    message = e.Message,
                    innerException = e,
                    innerExceptionMessage = e.InnerException?.Message
                }.ToString());
                response.Message = e.Message;
                response.Success = false;
            }

            return response;
        }
    }
}
