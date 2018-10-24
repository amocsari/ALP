﻿using DAL.Entity;
using Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Service
{
    public interface ILocationService: IBaseService<Location>
    {
        Task<LocationDto> AddNewLocation(LocationDto location);
        Task<List<LocationDto>> GetAllLocations();
        Task<LocationDto> GetLocationById(int locationId);
        Task<LocationDto> UpdateLocation(LocationDto location);
        Task DeleteLocationById(int locationId);
    }
}
