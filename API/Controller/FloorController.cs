using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Entity;
using DAL.Service;
using Microsoft.AspNetCore.Mvc;

namespace API.Controller
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FloorController : ControllerBase
    {
        private readonly IFloorService _floorService;

        public FloorController(IFloorService floorService)
        {
            _floorService = floorService;
        }

        [HttpGet]
        public Task<List<Floor>> GetAllFloors()
        {
            return _floorService.GetAll();
        }

        [HttpGet]
        public Task<Floor> GetFloorById(int floorId)
        {
            return _floorService.GetSingle(b => b.FloorID == floorId);
        }

        [HttpPost]
        public void AddNewFloor([FromBody] Floor floor)
        {
            _floorService.InsertNew(floor);
        }

        [HttpDelete]
        public void DeleteFloorById(int floorId)
        {
            _floorService.Remove(b => b.FloorID == floorId);
        }
    }
}