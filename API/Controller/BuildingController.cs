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
    public class BuildingController : ControllerBase
    {
        private readonly IBuildingService _buildingService;
        private readonly IAccountService _accountService;

        public BuildingController(IBuildingService BuildingService, IAccountService accountService)
        {
            _buildingService = BuildingService;
            _accountService = accountService;
        }
        
        [HttpGet]
        public Task<AlpApiResponse<List<BuildingDto>>> GetAllBuilding()
        {
            var sessionToken = HttpContext.Request.Headers["sessiontoken"];
            if (!_accountService.AuthorizeAsync(sessionToken, new List<RoleType> { RoleType.Admin, RoleType.DepartmentInventoryOperator }))
            {
                return Task.FromResult(new AlpApiResponse<List<BuildingDto>> { Success = false, Message = "Nincs jogosultsága ehhez a művelethez!" });
            }
            return _buildingService.GetAllBuildings();
        }

        [HttpGet]
        public Task<AlpApiResponse<List<BuildingDto>>> GetAvailableBuilding()
        {
            var sessionToken = HttpContext.Request.Headers["sessiontoken"];
            if (!_accountService.AuthorizeAsync(sessionToken, new List<RoleType> { RoleType.Admin, RoleType.DepartmentInventoryOperator }))
            {
                return Task.FromResult(new AlpApiResponse<List<BuildingDto>> { Success = false, Message = "Nincs jogosultsága ehhez a művelethez!" });
            }
            return _buildingService.GetAvailableBuildings();
        }

        //[HttpGet]
        //public Task<AlpApiResponse<BuildingDto>> GetBuildingById(int buildingId)
        //{
        //    var sessionToken = HttpContext.Request.Headers["sessiontoken"];
        //    if (!_accountService.AuthorizeAsync(sessionToken, new List<RoleType> { RoleType.Admin }))
        //    {
        //        return Task.FromResult(new AlpApiResponse<BuildingDto> { Success = false, Message = "Nincs jogosultsága ehhez a művelethez!" });
        //    }
        //    return _buildingService.GetBuildingById(buildingId);
        //}

        [HttpPost]
        public Task<AlpApiResponse<BuildingDto>> AddNewBuilding([FromBody] BuildingDto building)
        {
            var sessionToken = HttpContext.Request.Headers["sessiontoken"];
            if (!_accountService.AuthorizeAsync(sessionToken, new List<RoleType> { RoleType.Admin }))
            {
                return Task.FromResult(new AlpApiResponse<BuildingDto> { Success = false, Message = "Nincs jogosultsága ehhez a művelethez!" });
            }
            return _buildingService.InsertNewBuilding(building);
        }

        [HttpPost]
        public Task<AlpApiResponse> UpdateBuilding([FromBody] BuildingDto building)
        {
            var sessionToken = HttpContext.Request.Headers["sessiontoken"];
            if (!_accountService.AuthorizeAsync(sessionToken, new List<RoleType> { RoleType.Admin }))
            {
                return Task.FromResult(new AlpApiResponse { Success = false, Message = "Nincs jogosultsága ehhez a művelethez!" });
            }
            return _buildingService.UpdateBuilding(building);
        }

        //[HttpDelete]
        //public Task<AlpApiResponse> DeleteBuildingById(int buildingId)
        //{
        //    var sessionToken = HttpContext.Request.Headers["sessiontoken"];
        //    if (!_accountService.AuthorizeAsync(sessionToken, new List<RoleType> { RoleType.Admin }))
        //    {
        //        return Task.FromResult(new AlpApiResponse { Success = false, Message = "Nincs jogosultsága ehhez a művelethez!" });
        //    }
        //    return _buildingService.DeleteBuildingById(buildingId);
        //}

        [HttpPost]
        public Task<AlpApiResponse> ToggleLockStateByIdBuilding([FromBody] int buildingId)
        {
            var sessionToken = HttpContext.Request.Headers["sessiontoken"];
            if (!_accountService.AuthorizeAsync(sessionToken, new List<RoleType> { RoleType.Admin }))
            {
                return Task.FromResult(new AlpApiResponse { Success = false, Message = "Nincs jogosultsága ehhez a művelethez!" });
            }
            return _buildingService.ToggleBuildingLockStateById(buildingId);
        }
    }
}