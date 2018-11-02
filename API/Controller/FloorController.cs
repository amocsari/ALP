using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Service;
using Microsoft.AspNetCore.Mvc;
using Model.Dto;

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
        public Task<List<FloorDto>> GetAllFloor()
        {
            return _floorService.GetAllFloors();
        }

        [HttpGet]
        public Task<FloorDto> GetFloorById(int floorId)
        {
            return _floorService.GetFloorById(floorId);
        }

        [HttpPost]
        public Task<FloorDto> AddNewFloor([FromBody] FloorDto floor)
        {
            return _floorService.InsertNewFloor(floor);
        }

        [HttpPost]
        public void UpdateFloor([FromBody] FloorDto floor)
        {
            _floorService.UpdateFloor(floor);
        }

        [HttpDelete]
        public void DeleteFloorById(int floorId)
        {
            _floorService.DeleteFloorById(floorId);
        }

        [HttpPost]
        public void ToggleLockStateByIdFloor([FromBody] int floorId)
        {
            _floorService.ToggleFloorLockStateById(floorId);
        }
    }
}