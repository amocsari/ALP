using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Common.Model.Dto;
using Model.Model;
using API.Service;

namespace API.Controller
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BuildingController : ControllerBase
    {
        private readonly IBuildingService _buildingService;

        public BuildingController(IBuildingService BuildingService)
        {
            _buildingService = BuildingService;
        }
        
        [HttpGet]
        public Task<AlpApiResponse<List<BuildingDto>>> GetAllBuilding()
        {
            return _buildingService.GetAllBuildings();
        }

        [HttpGet]
        public Task<AlpApiResponse<List<BuildingDto>>> GetAvailableBuilding()
        {
            return _buildingService.GetAvailableBuildings();
        }

        [HttpGet]
        public Task<AlpApiResponse<BuildingDto>> GetBuildingById(int buildingId)
        {
            return _buildingService.GetBuildingById(buildingId);
        }

        [HttpPost]
        public Task<AlpApiResponse<BuildingDto>> AddNewBuilding([FromBody] BuildingDto building)
        {
            return _buildingService.InsertNewBuilding(building);
        }

        [HttpPost]
        public Task<AlpApiResponse> UpdateBuilding([FromBody] BuildingDto building)
        {
            return _buildingService.UpdateBuilding(building);
        }

        [HttpDelete]
        public Task<AlpApiResponse> DeleteBuildingById(int buildingId)
        {
            return _buildingService.DeleteBuildingById(buildingId);
        }

        [HttpPost]
        public Task<AlpApiResponse> ToggleLockStateByIdBuilding([FromBody] int buildingId)
        {
            return _buildingService.ToggleBuildingLockStateById(buildingId);
        }
    }
}