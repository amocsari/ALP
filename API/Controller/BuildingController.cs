using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Entity;
using DAL.Service;
using Microsoft.AspNetCore.Mvc;

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
        public  Task<List<Building>> GetAllBuildings()
        {
            return _buildingService.GetAll();
        }

        [HttpGet]
        public Task<Building> GetBuildingById(int buildingId)
        {
            return _buildingService.GetSingle(b => b.BuildingID == buildingId);
        }

        [HttpPost]
        public void AddNewBuilding([FromBody] Building building)
        {
            _buildingService.InsertNew(building);
        }

        [HttpDelete]
        public void DeleteBuildingById(int buildingId)
        {
            _buildingService.Remove(b => b.BuildingID == buildingId);
        }
    }
}