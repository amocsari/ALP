using DAL.Entity;
using Common.Model.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;
using Model.Model;

namespace DAL.Service
{
    public interface ILocationService
    {
        Task<AlpApiResponse<List<LocationDto>>> GetAllLocations();
        Task<AlpApiResponse<List<LocationDto>>> GetAvailableLocations();
        Task<AlpApiResponse<LocationDto>> AddNewLocation(LocationDto location);
        Task<AlpApiResponse<LocationDto>> GetLocationById(int locationId);
        Task<AlpApiResponse> UpdateLocation(LocationDto location);
        Task<AlpApiResponse> DeleteLocationById(int locationId);
        Task<AlpApiResponse> ToggleLocationLockStateById(int locationId);
    }
}
