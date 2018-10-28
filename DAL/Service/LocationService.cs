﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Context;
using DAL.Entity;
using DAL.Extensions;
using Microsoft.EntityFrameworkCore;
using Model;
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
            var entity = await InsertNew(location.DtoToEntity());
            return entity.EntityToDto();
        }

        public async Task DeleteLocationById(int locationId)
        {
            await Remove(location => location.LocationID == locationId);
        }

        public async Task<List<LocationDto>> GetAllLocations()
        {
            var entites = await GetAll();
            return entites.Select(e => e.EntityToDto()).ToList();
        }

        public async Task<LocationDto> GetLocationById(int locationId)
        {
            var entity = await GetSingle(location => location.LocationID == locationId);
            return entity.EntityToDto();
        }

        public async Task ToggleLocationLockStateById(int locationId)
        {
            var location = await GetLocationById(locationId);
            location.Locked = !location.Locked;
            await UpdateLocation(location);
        }

        public async Task<LocationDto> UpdateLocation(LocationDto dto)
        {
            var updatedEntity = await _context.Location.FirstOrDefaultAsync(location => location.LocationID == dto.Id);
            updatedEntity.UpdateEntityByDto(dto);
            await _context.SaveChangesAsync();
            return updatedEntity.EntityToDto();
        }
    }
}
