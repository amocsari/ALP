using DAL.Entity;
using DAL.Service;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DAL.Controller
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private ILocationService _locationService;

        public LocationController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        [HttpGet]
        public void AddLocation()
        {
            _locationService.AddLocation();
        }

        [HttpGet]
        public List<Location> GetAllLocations()
        {
            return _locationService.GetAllLocations();
        }
    }
}