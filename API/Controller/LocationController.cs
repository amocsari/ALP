using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using DAL.Entity;
using DAL.Service;
using Microsoft.AspNetCore.Mvc;
using Model;

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
        public Task<List<LocationDto>> GetAllLocations()
        {
            return _locationService.GetAllLocations();
        }

        [HttpGet]
        public Task<LocationDto> GetLocationById(int locationId)
        {
            return _locationService.GetLocationById(locationId);
        }

        [HttpPost]
        public Task<LocationDto> AddNewLocation([FromBody] LocationDto location)
        {
            return _locationService.AddNewLocation(location);
        }

        [HttpDelete]
        public void DeleteLocationById(int locationId)
        {
            _locationService.Remove(b => b.LocationID == locationId);
        }

        [HttpPost]
        public void UpdateLocation(LocationDto location)
        {
            _locationService.UpdateLocation(location);
        }
    }
}