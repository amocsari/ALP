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
    public class LocationService : BaseService<Location>, ILocationService
    {
        public LocationService(IAlpContext context)
        {
            _context = context;
        }

        public async Task<LocationDto> AddNewLocation(LocationDto location)
        {
            try
            {
                var entity = await InsertNew(location.DtoToEntity());
                return entity.EntityToDto();
            }
            catch (Exception e)
            {
                //TODO: logging
                return null;
            }
        }

        public async Task DeleteLocationById(int locationId)
        {
            try
            {
                await Remove(location => location.LocationID == locationId);
            }
            catch (Exception e)
            {
                //TODO: logging
            }
        }

        public async Task<List<LocationDto>> GetAllLocations()
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

        public async Task<LocationDto> GetLocationById(int locationId)
        {
            try
            {
                var entity = await GetSingle(location => location.LocationID == locationId);
                return entity.EntityToDto();
            }
            catch (Exception e)
            {
                //TODO: logging
                return null;
            }
        }

        public async Task ToggleLocationLockStateById(int locationId)
        {
            try
            {
                var location = await GetLocationById(locationId);
                location.Locked = !location.Locked;
                await UpdateLocation(location);
            }
            catch (Exception e)
            {
                //TODO: logging
            }
        }

        public async Task<LocationDto> UpdateLocation(LocationDto dto)
        {
            try
            {
                var updatedEntity = await _context.Location.FirstOrDefaultAsync(location => location.LocationID == dto.Id);
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
