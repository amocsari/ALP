using System.Collections.Generic;
using DAL.Entity;
using DAL.Service;
using Microsoft.AspNetCore.Mvc;

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
        public List<Location> GetAllLocations()
        {
            return _locationService.GetAll();
        }

        [HttpGet]
        public Location GetLocationById(int locationId)
        {
            return _locationService.GetSingle(b => b.LocationID == locationId);
        }

        [HttpPost]
        public void AddNewLocation([FromBody] Location location)
        {
            _locationService.InsertNew(location);
        }

        [HttpDelete]
        public void DeleteLocationById(int locationId)
        {
            _locationService.Remove(b => b.LocationID == locationId);
        }
    }
}