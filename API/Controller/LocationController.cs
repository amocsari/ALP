using System.Collections.Generic;
using System.Linq;
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
        public List<LocationDto> GetAllLocations()
        {
            return _locationService.GetAll().Select(x => x.EntityToDto()).ToList();
        }

        [HttpGet]
        public Location GetLocationById(int locationId)
        {
            return _locationService.GetSingle(b => b.LocationID == locationId);
        }

        [HttpPost]
        public void AddNewLocation([FromBody] LocationDto location)
        {
            _locationService.InsertNew(location.DtoToEntity());
        }

        [HttpDelete]
        public void DeleteLocationById(int locationId)
        {
            _locationService.Remove(b => b.LocationID == locationId);
        }
    }
}