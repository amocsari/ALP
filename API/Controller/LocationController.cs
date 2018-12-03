using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Service;
using Microsoft.AspNetCore.Mvc;
using Common.Model.Dto;
using Model.Model;

namespace API.Controller
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ILocationService _locationService;

        public LocationController(ILocationService LocationService)
        {
            _locationService = LocationService;
        }

        [HttpGet]
        public Task<AlpApiResponse<List<LocationDto>>> GetAllLocation()
        {
            return _locationService.GetAllLocations();
        }

        [HttpGet]
        public Task<AlpApiResponse<List<LocationDto>>> GetAvailableLocation()
        {
            return _locationService.GetAvailableLocations();
        }

        [HttpGet]
        public Task<AlpApiResponse<LocationDto>> GetLocationById(int locationId)
        {
            return _locationService.GetLocationById(locationId);
        }

        [HttpPost]
        public Task<AlpApiResponse<LocationDto>> AddNewLocation([FromBody] LocationDto location)
        {
            return _locationService.AddNewLocation(location);
        }

        [HttpDelete]
        public Task<AlpApiResponse> DeleteLocationById(int locationId)
        {
           return _locationService.DeleteLocationById(locationId);
        }

        [HttpPost]
        public Task<AlpApiResponse> UpdateLocation([FromBody] LocationDto location)
        {
            return _locationService.UpdateLocation(location);
        }

        [HttpPost]
        public Task<AlpApiResponse> ToggleLockStateByIdLocation([FromBody] int locationId)
        {
            return _locationService.ToggleLocationLockStateById(locationId);
        }
    }
}