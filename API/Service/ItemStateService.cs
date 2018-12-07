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

namespace API.Service
{
    /// <summary>
    /// handles itemstate related database operations
    /// </summary>
    public class ItemStateService : IItemStateService
    {
        private readonly IAlpContext _context;
        private readonly ILogger<ItemStateService> _logger;

        public ItemStateService(IAlpContext context, ILogger<ItemStateService> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// adds new itemstate
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<AlpApiResponse<ItemStateDto>> AddNewItemState(ItemStateDto dto)
        {
            var response = new AlpApiResponse<ItemStateDto>();
            try
            {
                _logger.LogDebug(new
                {
                    action = nameof(AddNewItemState),
                    dto = dto?.ToString()
                }.ToString());

                dto.Validate();

                var entity = dto.DtoToEntity();
                await _context.ItemState.AddAsync(entity);
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

        /// <summary>
        /// deletes itemstate by id
        /// </summary>
        /// <param name="itemStateId"></param>
        /// <returns></returns>
        public async Task<AlpApiResponse> DeleteItemStateById(int itemStateId)
        {
            var response = new AlpApiResponse();
            try
            {
                _logger.LogDebug(new
                {
                    action = nameof(DeleteItemStateById),
                    itemStateId
                }.ToString());

                var entity = await _context.ItemState.FirstOrDefaultAsync(itemState => itemState.ItemStateId == itemStateId);
                _context.Remove(entity);
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

        /// <summary>
        /// gets all itemstates
        /// </summary>
        /// <returns></returns>
        public async Task<AlpApiResponse<List<ItemStateDto>>> GetAllItemStates()
        {
            var response = new AlpApiResponse<List<ItemStateDto>>();
            try
            {
                _logger.LogDebug(new
                {
                    action = nameof(GetAllItemStates)
                }.ToString());

                var entites = await _context.ItemState.AsNoTracking().ToListAsync();
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

        /// <summary>
        /// gets not locked itemstates
        /// </summary>
        /// <returns></returns>
        public async Task<AlpApiResponse<List<ItemStateDto>>> GetAvailableItemStates()
        {
            var response = new AlpApiResponse<List<ItemStateDto>>();
            try
            {
                _logger.LogDebug(new
                {
                    action = nameof(GetAvailableItemStates)
                }.ToString());

                var entites = await _context.ItemState.AsNoTracking().Where(itemState => !itemState.Locked).ToListAsync();
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

        /// <summary>
        /// gets an itemstate by id
        /// </summary>
        /// <param name="itemStateId"></param>
        /// <returns></returns>
        public async Task<AlpApiResponse<ItemStateDto>> GetItemStateById(int itemStateId)
        {
            var response = new AlpApiResponse<ItemStateDto>();
            try
            {
                _logger.LogDebug(new
                {
                    action = nameof(GetItemStateById),
                    itemStateId
                }.ToString());

                var entity = await _context.ItemState.FirstOrDefaultAsync(itemState => itemState.ItemStateId == itemStateId);
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

        /// <summary>
        /// updates itemstate by dto
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<AlpApiResponse> UpdateItemState(ItemStateDto dto)
        {
            var response = new AlpApiResponse();
            try
            {
                _logger.LogDebug(new
                {
                    action = nameof(UpdateItemState),
                    dto = dto?.ToString()
                }.ToString());

                dto.Validate();

                if (dto.Id == 1 || dto.Id == 2 || dto.Id == 3)
                {
                    response.Success = false;
                    response.Message = "Ennek a státusznak az adatai nem változtathatóak!";
                    return response;
                }

                var updatedEntity = await _context.ItemState.FirstOrDefaultAsync(itemState => itemState.ItemStateId == dto.Id);
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

        /// <summary>
        /// changes locked state if itemstate
        /// </summary>
        /// <param name="itemStateId"></param>
        /// <returns></returns>
        public async Task<AlpApiResponse> ToggleItemStateLockStateById(int itemStateId)
        {
            var response = new AlpApiResponse();
            try
            {
                _logger.LogDebug(new
                {
                    action = nameof(ToggleItemStateLockStateById),
                    itemStateId
                }.ToString());

                var getByIdResponse = await GetItemStateById(itemStateId);
                if (!getByIdResponse.Success)
                {
                    response.Success = getByIdResponse.Success;
                    response.Message = getByIdResponse.Message;
                    return response;
                }

                var itemState = getByIdResponse.Value;
                itemState.Locked = !itemState.Locked;
                var updateResponse = await UpdateItemState(itemState);
                if (!updateResponse.Success)
                {
                    response.Success = updateResponse.Success;
                    response.Message = updateResponse.Message;
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
