using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Common.Model.Dto;
using Microsoft.Extensions.Logging;
using Model.Model;
using API.Service;
using Model.Enum;

namespace API.Controller
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ILocationService _locationService;
        private readonly IAccountService _accountService;

        public LocationController(ILocationService LocationService, IAccountService accountService)
        {
            _locationService = LocationService;
            _accountService = accountService;
        }

        [HttpGet]
        public Task<AlpApiResponse<List<LocationDto>>> GetAllLocation()
        {
            var sessionToken = HttpContext.Request.Headers["sessiontoken"];
            if (!_accountService.AuthorizeAsync(sessionToken, new List<RoleType> { RoleType.Admin, RoleType.DepartmentInventoryOperator }))
            {
                return Task.FromResult(new AlpApiResponse<List<LocationDto>> { Success = false, Message = "Nincs jogosultsága ehhez a művelethez!" });
            }
            return _locationService.GetAllLocations();
        }

        [HttpGet]
        public Task<AlpApiResponse<List<LocationDto>>> GetAvailableLocation()
        {
            var sessionToken = HttpContext.Request.Headers["sessiontoken"];
            if (!_accountService.AuthorizeAsync(sessionToken, new List<RoleType> { RoleType.Admin, RoleType.DepartmentInventoryOperator }))
            {
                return Task.FromResult(new AlpApiResponse<List<LocationDto>> { Success = false, Message = "Nincs jogosultsága ehhez a művelethez!" });
            }
            return _locationService.GetAvailableLocations();
        }

        //[HttpGet]
        //public Task<AlpApiResponse<LocationDto>> GetLocationById(int locationId)
        //{
        //    var sessionToken = HttpContext.Request.Headers["sessiontoken"];
        //    if (!_accountService.AuthorizeAsync(sessionToken, new List<RoleType> { RoleType.Admin }))
        //    {
        //        return Task.FromResult(new AlpApiResponse<LocationDto> { Success = false, Message = "Nincs jogosultsága ehhez a művelethez!" });
        //    }
        //    return _locationService.GetLocationById(locationId);
        //}

        [HttpPost]
        public Task<AlpApiResponse<LocationDto>> AddNewLocation([FromBody] LocationDto location)
        {
            var sessionToken = HttpContext.Request.Headers["sessiontoken"];
            if (!_accountService.AuthorizeAsync(sessionToken, new List<RoleType> { RoleType.Admin }))
            {
                return Task.FromResult(new AlpApiResponse<LocationDto> { Success = false, Message = "Nincs jogosultsága ehhez a művelethez!" });
            }
            return _locationService.AddNewLocation(location);
        }

        //[HttpDelete]
        //public Task<AlpApiResponse> DeleteLocationById(int locationId)
        //{
        //    var sessionToken = HttpContext.Request.Headers["sessiontoken"];
        //    if (!_accountService.AuthorizeAsync(sessionToken, new List<RoleType> { RoleType.Admin }))
        //    {
        //        return Task.FromResult(new AlpApiResponse { Success = false, Message = "Nincs jogosultsága ehhez a művelethez!" });
        //    }
        //    return _locationService.DeleteLocationById(locationId);
        //}

        [HttpPost]
        public Task<AlpApiResponse> UpdateLocation([FromBody] LocationDto location)
        {
            var sessionToken = HttpContext.Request.Headers["sessiontoken"];
            if (!_accountService.AuthorizeAsync(sessionToken, new List<RoleType> { RoleType.Admin }))
            {
                return Task.FromResult(new AlpApiResponse { Success = false, Message = "Nincs jogosultsága ehhez a művelethez!" });
            }
            return _locationService.UpdateLocation(location);
        }

        [HttpPost]
        public Task<AlpApiResponse> ToggleLockStateByIdLocation([FromBody] int locationId)
        {
            var sessionToken = HttpContext.Request.Headers["sessiontoken"];
            if (!_accountService.AuthorizeAsync(sessionToken, new List<RoleType> { RoleType.Admin }))
            {
                return Task.FromResult(new AlpApiResponse { Success = false, Message = "Nincs jogosultsága ehhez a művelethez!" });
            }
            return _locationService.ToggleLocationLockStateById(locationId);
        }
    }
}