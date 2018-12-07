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
    /// Handles building related database operations
    /// </summary>
    public class BuildingService : IBuildingService
    {
        private readonly IAlpContext _context;
        private readonly ILogger<BuildingService> _logger;

        public BuildingService(IAlpContext context, ILogger<BuildingService> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Deletes a building by its id
        /// </summary>
        /// <param name="buildingId"></param>
        /// <returns></returns>
        public async Task<AlpApiResponse> DeleteBuildingById(int buildingId)
        {
            var response = new AlpApiResponse();
            try
            {
                _logger.LogDebug(new
                {
                    action = nameof(DeleteBuildingById),
                    buildingId
                }.ToString());

                var entity = await _context.Building.FirstOrDefaultAsync(building => building.BuildingId == buildingId);
                _context.Building.Remove(entity);
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
        /// gets all the buldings from the database
        /// </summary>
        /// <returns></returns>
        public async Task<AlpApiResponse<List<BuildingDto>>> GetAllBuildings()
        {
            var response = new AlpApiResponse<List<BuildingDto>>();
            try
            {
                _logger.LogDebug(new
                {
                    action = nameof(GetAllBuildings)
                }.ToString());

                var buildings = await _context.Building.AsNoTracking().Include(building => building.Location).ToListAsync();
                response.Value = buildings.Select(building => building.EntityToDto()).ToList();
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
        /// gets all the non-locked buildings
        /// </summary>
        /// <returns></returns>
        public async Task<AlpApiResponse<List<BuildingDto>>> GetAvailableBuildings()
        {
            var response = new AlpApiResponse<List<BuildingDto>>();
            try
            {
                _logger.LogDebug(new
                {
                    action = nameof(GetAvailableBuildings)
                }.ToString());

                var buildings = await _context.Building.AsNoTracking().Include(building => building.Location).Where(building => !building.Locked).ToListAsync();
                response.Value = buildings.Select(building => building.EntityToDto()).ToList();
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
        /// gets a building by its id
        /// </summary>
        /// <param name="buildingId"></param>
        /// <returns></returns>
        public async Task<AlpApiResponse<BuildingDto>> GetBuildingById(int buildingId)
        {
            var response = new AlpApiResponse<BuildingDto>();
            try
            {
                _logger.LogDebug(new
                {
                    action = nameof(GetBuildingById),
                    buildingId
                }.ToString());

                var entity = await _context.Building.Include(building => building.Location).FirstOrDefaultAsync(building => building.BuildingId == buildingId);
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
        /// adds a new building into the database
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<AlpApiResponse<BuildingDto>> InsertNewBuilding(BuildingDto dto)
        {
            var response = new AlpApiResponse<BuildingDto>();

            try
            {
                _logger.LogDebug(new
                {
                    action = nameof(InsertNewBuilding),
                    dto = dto?.ToString()
                }.ToString());

                dto.Validate();

                var entity = dto.DtoToEntity();
                entity.Location = null;
                await _context.Building.AddAsync(entity);
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
        /// updates a building by a dto
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<AlpApiResponse> UpdateBuilding(BuildingDto dto)
        {
            var response = new AlpApiResponse();
            try
            {
                _logger.LogDebug(new
                {
                    action = nameof(UpdateBuilding),
                    dto = dto?.ToString()
                }.ToString());

                dto.Validate();

                var updatedEntity = await _context.Building.Include(building => building.Location).FirstOrDefaultAsync(building => building.BuildingId == dto.Id);
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
        /// changes the lockstate of a building
        /// </summary>
        /// <param name="buildingId"></param>
        /// <returns></returns>
        public async Task<AlpApiResponse> ToggleBuildingLockStateById(int buildingId)
        {
            var response = new AlpApiResponse();
            try
            {
                _logger.LogDebug(new
                {
                    action = nameof(ToggleBuildingLockStateById),
                    buildingId
                }.ToString());

                var getByIdReply = await GetBuildingById(buildingId);
                if (!getByIdReply.Success)
                {
                    response.Success = getByIdReply.Success;
                    response.Message = getByIdReply.Message;
                    return response;
                }

                var building = getByIdReply.Value;

                building.Locked = !building.Locked;

                var updateReply = await UpdateBuilding(building);
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
