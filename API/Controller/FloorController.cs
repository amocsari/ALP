using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Common.Model.Dto;
using Model.Model;
using API.Service;
using Model.Enum;

namespace API.Controller
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FloorController : ControllerBase
    {
        private readonly IFloorService _floorService;
        private readonly IAccountService _accountService;

        public FloorController(IFloorService floorService, IAccountService accountService)
        {
            _floorService = floorService;
            _accountService = accountService;
        }

        [HttpGet]
        public Task<AlpApiResponse<List<FloorDto>>> GetAllFloor()
        {
            var sessionToken = HttpContext.Request.Headers["sessiontoken"];
            if (!_accountService.Authorize(sessionToken, new List<RoleType> { RoleType.Admin, RoleType.DepartmentInventoryOperator }))
            {
                return Task.FromResult(new AlpApiResponse<List<FloorDto>> { Success = false, Message = "Nincs jogosultsága ehhez a művelethez!" });
            }
            return _floorService.GetAllFloors();
        }

        [HttpGet]
        public Task<AlpApiResponse<List<FloorDto>>> GetAvailableFloor()
        {
            var sessionToken = HttpContext.Request.Headers["sessiontoken"];
            if (!_accountService.Authorize(sessionToken, new List<RoleType> { RoleType.Admin, RoleType.DepartmentInventoryOperator }))
            {
                return Task.FromResult(new AlpApiResponse<List<FloorDto>> { Success = false, Message = "Nincs jogosultsága ehhez a művelethez!" });
            }
            return _floorService.GetAvailableFloors();
        }

        //[HttpGet]
        //public Task<AlpApiResponse<FloorDto>> GetFloorById(int floorId)
        //{
        //    var sessionToken = HttpContext.Request.Headers["sessiontoken"];
        //    if (!_accountService.AuthorizeAsync(sessionToken, new List<RoleType> { RoleType.Admin }))
        //    {
        //        return Task.FromResult(new AlpApiResponse<FloorDto> { Success = false, Message = "Nincs jogosultsága ehhez a művelethez!" });
        //    }
        //    return _floorService.GetFloorById(floorId);
        //}

        [HttpPost]
        public Task<AlpApiResponse<FloorDto>> AddNewFloor([FromBody] FloorDto floor)
        {
            var sessionToken = HttpContext.Request.Headers["sessiontoken"];
            if (!_accountService.Authorize(sessionToken, new List<RoleType> { RoleType.Admin }))
            {
                return Task.FromResult(new AlpApiResponse<FloorDto> { Success = false, Message = "Nincs jogosultsága ehhez a művelethez!" });
            }
            return _floorService.InsertNewFloor(floor);
        }

        [HttpPost]
        public Task<AlpApiResponse> UpdateFloor([FromBody] FloorDto floor)
        {
            var sessionToken = HttpContext.Request.Headers["sessiontoken"];
            if (!_accountService.Authorize(sessionToken, new List<RoleType> { RoleType.Admin }))
            {
                return Task.FromResult(new AlpApiResponse { Success = false, Message = "Nincs jogosultsága ehhez a művelethez!" });
            }
            return _floorService.UpdateFloor(floor);
        }

        //[HttpDelete]
        //public Task<AlpApiResponse> DeleteFloorById(int floorId)
        //{
        //    var sessionToken = HttpContext.Request.Headers["sessiontoken"];
        //    if (!_accountService.AuthorizeAsync(sessionToken, new List<RoleType> { RoleType.Admin }))
        //    {
        //        return Task.FromResult(new AlpApiResponse { Success = false, Message = "Nincs jogosultsága ehhez a művelethez!" });
        //    }
        //    return _floorService.DeleteFloorById(floorId);
        //}

        [HttpPost]
        public Task<AlpApiResponse> ToggleLockStateByIdFloor([FromBody] int floorId)
        {
            var sessionToken = HttpContext.Request.Headers["sessiontoken"];
            if (!_accountService.Authorize(sessionToken, new List<RoleType> { RoleType.Admin }))
            {
                return Task.FromResult(new AlpApiResponse { Success = false, Message = "Nincs jogosultsága ehhez a művelethez!" });
            }
            return _floorService.ToggleFloorLockStateById(floorId);
        }
    }
}