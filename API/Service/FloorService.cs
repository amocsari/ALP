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
    /// Handles floor related database operations
    /// </summary>
    public class FloorService : IFloorService
    {
        private readonly IAlpContext _context;
        private readonly ILogger<FloorService> _logger;

        public FloorService(IAlpContext context, ILogger<FloorService> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// feletes floor by id
        /// </summary>
        /// <param name="floorId"></param>
        /// <returns></returns>
        public async Task<AlpApiResponse> DeleteFloorById(int floorId)
        {
            var response = new AlpApiResponse();

            try
            {
                _logger.LogDebug(new
                {
                    action = nameof(DeleteFloorById),
                    floorId
                }.ToString());

                var entity = await _context.Floor.FirstOrDefaultAsync(floor => floor.FloorId == floorId);
                _context.Floor.Remove(entity);
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
        /// gets all floors from database
        /// </summary>
        /// <returns></returns>
        public async Task<AlpApiResponse<List<FloorDto>>> GetAllFloors()
        {
            var response = new AlpApiResponse<List<FloorDto>>();

            try
            {
                _logger.LogDebug(new
                {
                    action = nameof(GetAllFloors)
                }.ToString());

                var floors = await _context.Floor.AsNoTracking().Include(floor => floor.Building).ToListAsync();
                response.Value = floors.Select(floor => floor.EntityToDto()).ToList();
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
        /// gets nonlocked floors
        /// </summary>
        /// <returns></returns>
        public async Task<AlpApiResponse<List<FloorDto>>> GetAvailableFloors()
        {
            var response = new AlpApiResponse<List<FloorDto>>();

            try
            {
                _logger.LogDebug(new
                {
                    action = nameof(GetAvailableFloors)
                }.ToString());

                var floors = await _context.Floor.AsNoTracking().Include(floor => floor.Building).Where(floor => !floor.Locked).ToListAsync();
                response.Value = floors.Select(floor => floor.EntityToDto()).ToList();
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
        /// gets a floor by id
        /// </summary>
        /// <param name="floorId"></param>
        /// <returns></returns>
        public async Task<AlpApiResponse<FloorDto>> GetFloorById(int floorId)
        {
            var response = new AlpApiResponse<FloorDto>();

            try
            {
                _logger.LogDebug(new
                {
                    action = nameof(GetFloorById),
                    floorId
                }.ToString());

                var entity = await _context.Floor.FirstOrDefaultAsync(floor => floor.FloorId == floorId);
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
        /// adds a new floor to the database
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<AlpApiResponse<FloorDto>> InsertNewFloor(FloorDto dto)
        {
            var response = new AlpApiResponse<FloorDto>();

            try
            {
                _logger.LogDebug(new
                {
                    action = nameof(InsertNewFloor),
                    dto = dto?.ToString()
                }.ToString());

                dto.Validate();

                var entity = dto.DtoToEntity();
                entity.Building = null;
                await _context.Floor.AddAsync(entity);
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
        /// updates a floor by dto
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<AlpApiResponse> UpdateFloor(FloorDto dto)
        {
            var response = new AlpApiResponse();

            try
            {
                _logger.LogDebug(new
                {
                    action = nameof(UpdateFloor),
                    dto = dto?.ToString()
                }.ToString());

                dto.Validate();

                var updatedEntity = await _context.Floor.FirstOrDefaultAsync(floor => floor.FloorId == dto.Id);
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
        /// change the locked state of floor
        /// </summary>
        /// <param name="floorId"></param>
        /// <returns></returns>
        public async Task<AlpApiResponse> ToggleFloorLockStateById(int floorId)
        {
            var response = new AlpApiResponse();

            try
            {
                _logger.LogDebug(new
                {
                    action = nameof(ToggleFloorLockStateById),
                    floorId
                }.ToString());

                var getByIdResponse = await GetFloorById(floorId);
                if (!getByIdResponse.Success)
                {
                    response.Success = getByIdResponse.Success;
                    response.Message = getByIdResponse.Message;
                    return response;
                }

                var floor = getByIdResponse.Value;
                
                floor.Locked = !floor.Locked;
                var updateReply = await UpdateFloor(floor);
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
