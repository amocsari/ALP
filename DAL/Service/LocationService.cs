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
    public class LocationService : ILocationService
    {
        private readonly IAlpContext _context;
        private readonly ILogger<LocationService> _logger;

        public LocationService(IAlpContext context, ILogger<LocationService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<AlpApiResponse<LocationDto>> AddNewLocation(LocationDto dto)
        {
            var response = new AlpApiResponse<LocationDto>();

            try
            {
                _logger.LogDebug(new
                {
                    action = nameof(AddNewLocation),
                    dto = dto.ToString()
                }.ToString());

                dto.Validate();

                var entity = dto.DtoToEntity();
                await _context.Location.AddAsync(entity);
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

        public async Task<AlpApiResponse> DeleteLocationById(int locationId)
        {
            var response = new AlpApiResponse();
            try
            {
                _logger.LogDebug(new
                {
                    action = nameof(DeleteLocationById),
                    locationId
                }.ToString());

                var entity = await _context.Location.FirstOrDefaultAsync(location => location.LocationId == locationId);
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

        public async Task<AlpApiResponse<List<LocationDto>>> GetAllLocations()
        {
            var response = new AlpApiResponse<List<LocationDto>>();
            try
            {
                _logger.LogDebug(new
                {
                    action = nameof(GetAllLocations)
                }.ToString());

                var entites = await _context.Location.AsNoTracking().ToListAsync();
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

        public async Task<AlpApiResponse<List<LocationDto>>> GetAvailableLocations()
        {
            var response = new AlpApiResponse<List<LocationDto>>();
            try
            {
                _logger.LogDebug(new
                {
                    action = nameof(GetAvailableLocations)
                }.ToString());

                var entites = await _context.Location.AsNoTracking().Where(location => !location.Locked).ToListAsync();
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

        public async Task<AlpApiResponse<LocationDto>> GetLocationById(int locationId)
        {
            var response = new AlpApiResponse<LocationDto>();
            try
            {
                _logger.LogDebug(new
                {
                    action = nameof(GetLocationById),
                    locationId
                }.ToString());

                var entity = await _context.Location.FirstOrDefaultAsync(location => location.LocationId == locationId);
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

        public async Task<AlpApiResponse> UpdateLocation(LocationDto dto)
        {
            var response = new AlpApiResponse();
            try
            {
                _logger.LogDebug(new
                {
                    action = nameof(UpdateLocation),
                    dto = dto.ToString()
                }.ToString());

                dto.Validate();

                var updatedEntity = await _context.Location.FirstOrDefaultAsync(location => location.LocationId == dto.Id);
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

        public async Task<AlpApiResponse> ToggleLocationLockStateById(int locationId)
        {
            var response = new AlpApiResponse();
            try
            {
                _logger.LogDebug(new
                {
                    action = nameof(ToggleLocationLockStateById),
                    locationId
                }.ToString());

                var getByIdResponse = await GetLocationById(locationId);
                if (!getByIdResponse.Success)
                {
                    response.Success = getByIdResponse.Success;
                    response.Message = getByIdResponse.Message;
                    return response;
                }

                var location = getByIdResponse.Value;

                location.Locked = !location.Locked;
                var updateResponse = await UpdateLocation(location);
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
