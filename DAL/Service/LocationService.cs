using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Context;
using DAL.Extensions;
using Microsoft.EntityFrameworkCore;
using Common.Model.Dto;
using Model.Model;

namespace DAL.Service
{
    public class LocationService : ILocationService
    {
        private IAlpContext _context;

        public LocationService(IAlpContext context)
        {
            _context = context;
        }

        public async Task<AlpApiResponse<LocationDto>> AddNewLocation(LocationDto dto)
        {
            var response = new AlpApiResponse<LocationDto>();

            try
            {
                dto.Validate();

                var entity = dto.DtoToEntity();
                await _context.Location.AddAsync(entity);
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

        public async Task<AlpApiResponse> DeleteLocationById(int locationId)
        {
            var response = new AlpApiResponse();
            try
            {
                var entity = await _context.Location.FirstOrDefaultAsync(location => location.LocationId == locationId);
                _context.Remove(entity);
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

        public async Task<AlpApiResponse<List<LocationDto>>> GetAllLocations()
        {
            var response = new AlpApiResponse<List<LocationDto>>();
            try
            {
                var entites = await _context.Location.AsNoTracking().ToListAsync();
                response.Value = entites.Select(e => e.EntityToDto()).ToList();
            }
            catch (Exception e)
            {
                //TODO: logging
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
                var entites = await _context.Location.AsNoTracking().Where(location => !location.Locked).ToListAsync();
                response.Value = entites.Select(e => e.EntityToDto()).ToList();
            }
            catch (Exception e)
            {
                //TODO: logging
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
                var entity = await _context.Location.FirstOrDefaultAsync(location => location.LocationId == locationId);
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

        public async Task<AlpApiResponse> UpdateLocation(LocationDto dto)
        {
            var response = new AlpApiResponse();
            try
            {
                dto.Validate();

                var updatedEntity = await _context.Location.FirstOrDefaultAsync(location => location.LocationId == dto.Id);
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

        public async Task<AlpApiResponse> ToggleLocationLockStateById(int locationId)
        {
            var response = new AlpApiResponse();
            try
            {
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
                //TODO: logging
                response.Message = e.Message;
                response.Success = false;
            }

            return response;
        }
    }
}
