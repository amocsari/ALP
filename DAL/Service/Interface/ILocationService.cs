using DAL.Entity;
using Common.Model.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Service
{
    public interface ILocationService
    {
        Task<List<LocationDto>> GetAllLocations();
        Task<List<LocationDto>> GetAvailableLocations();
        Task<LocationDto> AddNewLocation(LocationDto location);
        Task<LocationDto> GetLocationById(int locationId);
        Task<LocationDto> UpdateLocation(LocationDto location);
        Task DeleteLocationById(int locationId);
        Task ToggleLocationLockStateById(int locationId);
    }
}
