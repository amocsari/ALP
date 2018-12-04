using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Context;
using DAL.Extensions;
using Microsoft.EntityFrameworkCore;
using Common.Model.Dto;
using Model.Model;
using Microsoft.Extensions.Logging;

namespace DAL.Service
{
    public class ItemNatureService : IItemNatureService
    {
        private readonly IAlpContext _context;
        private readonly ILogger<ItemNatureService> _logger;

        public ItemNatureService(IAlpContext context, ILogger<ItemNatureService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<AlpApiResponse<ItemNatureDto>> AddNewItemNature(ItemNatureDto dto)
        {
            var response = new AlpApiResponse<ItemNatureDto>();
            try
            {
                _logger.LogDebug(new
                {
                    action = nameof(AddNewItemNature),
                    dto = dto.ToString()
                }.ToString());

                dto.Validate();

                var entity = dto.DtoToEntity();
                await _context.ItemNature.AddAsync(entity);
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

        public async Task<AlpApiResponse> DeleteItemNatureById(int itemNatureId)
        {
            var response = new AlpApiResponse();
            try
            {
                _logger.LogDebug(new
                {
                    action = nameof(DeleteItemNatureById),
                    itemNatureId
                }.ToString());

                var entity = await _context.ItemNature.FirstOrDefaultAsync(itemNature => itemNature.ItemNatureId == itemNatureId);
                _context.ItemNature.Remove(entity);
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

        public async Task<AlpApiResponse<List<ItemNatureDto>>> GetAllItemNatures()
        {
            var response = new AlpApiResponse<List<ItemNatureDto>>();
            try
            {
                _logger.LogDebug(new
                {
                    action = nameof(GetAllItemNatures)
                }.ToString());

                var entites = await _context.ItemNature.AsNoTracking().ToListAsync();
                response.Value = entites.Select(e => e.EntityToDto()).ToList();
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

        public async Task<AlpApiResponse<List<ItemNatureDto>>> GetAvailableItemNatures()
        {
            var response = new AlpApiResponse<List<ItemNatureDto>>();
            try
            {
                _logger.LogDebug(new
                {
                    action = nameof(GetAvailableItemNatures)
                }.ToString());

                var entites = await _context.ItemNature.AsNoTracking().Where(itemNature => !itemNature.Locked).ToListAsync();
                response.Value = entites.Select(e => e.EntityToDto()).ToList();

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

        public async Task<AlpApiResponse<ItemNatureDto>> GetItemNatureById(int itemNatureId)
        {
            var response = new AlpApiResponse<ItemNatureDto>();
            try
            {
                _logger.LogDebug(new
                {
                    action = nameof(GetItemNatureById),
                    itemNatureId
                }.ToString());

                var entity = await _context.ItemNature.FirstOrDefaultAsync(itemNature => itemNature.ItemNatureId == itemNatureId);
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

        public async Task<AlpApiResponse> UpdateItemNature(ItemNatureDto dto)
        {
            var response = new AlpApiResponse();
            try
            {
                _logger.LogDebug(new
                {
                    action = nameof(UpdateItemNature),
                    dto = dto.ToString()
                }.ToString());

                dto.Validate();

                var updatedEntity = await _context.ItemNature.FirstOrDefaultAsync(itemNature => itemNature.ItemNatureId == dto.Id);
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

        public async Task<AlpApiResponse> ToggleItemNatureLockStateById(int itemNatureId)
        {
            var response = new AlpApiResponse();
            try
            {
                _logger.LogDebug(new
                {
                    action = nameof(ToggleItemNatureLockStateById),
                    itemNatureId
                }.ToString());

                var getByIdReply = await GetItemNatureById(itemNatureId);
                if (!getByIdReply.Success)
                {
                    response.Success = getByIdReply.Success;
                    response.Message = getByIdReply.Message;
                    return response;
                }

                var itemNature = getByIdReply.Value;

                itemNature.Locked = !itemNature.Locked;
                var updateRespone = await UpdateItemNature(itemNature);
                if (!updateRespone.Success)
                {
                    response.Success = updateRespone.Success;
                    response.Message = updateRespone.Message;
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
