using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Entity;
using DAL.Service;
using Microsoft.AspNetCore.Mvc;
using Model.Dto;

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
        public  Task<List<BuildingDto>> GetAllBuilding()
        {
            return _buildingService.GetAllBuildings();
        }

        [HttpGet]
        public Task<BuildingDto> GetBuildingById(int buildingId)
        {
            return _buildingService.GetBuildingById(buildingId);
        }

        [HttpPost]
        public Task<BuildingDto> AddNewBuilding([FromBody] BuildingDto building)
        {
            return _buildingService.InsertNewBuilding(building);
        }

        [HttpDelete]
        public void DeleteBuildingById(int buildingId)
        {
            _buildingService.DeleteBuildingById(buildingId);
        }

        [HttpPost]
        public void ToggleLockStateByIdBuilding([FromBody] int locationId)
        {
            _buildingService.ToggleLocationLockStateById(locationId);
        }
    }
}