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
    public class FloorController : ControllerBase
    {
        private readonly IFloorService _floorService;

        public FloorController(IFloorService floorService)
        {
            _floorService = floorService;
        }

        [HttpGet]
        public Task<AlpApiResponse<List<FloorDto>>> GetAllFloor()
        {
            return _floorService.GetAllFloors();
        }

        [HttpGet]
        public Task<AlpApiResponse<List<FloorDto>>> GetAvailableFloor()
        {
            return _floorService.GetAvailableFloors();
        }

        [HttpGet]
        public Task<AlpApiResponse<FloorDto>> GetFloorById(int floorId)
        {
            return _floorService.GetFloorById(floorId);
        }

        [HttpPost]
        public Task<AlpApiResponse<FloorDto>> AddNewFloor([FromBody] FloorDto floor)
        {
            return _floorService.InsertNewFloor(floor);
        }

        [HttpPost]
        public Task<AlpApiResponse> UpdateFloor([FromBody] FloorDto floor)
        {
            return _floorService.UpdateFloor(floor);
        }

        [HttpDelete]
        public Task<AlpApiResponse> DeleteFloorById(int floorId)
        {
            return _floorService.DeleteFloorById(floorId);
        }

        [HttpPost]
        public Task<AlpApiResponse> ToggleLockStateByIdFloor([FromBody] int floorId)
        {
            return _floorService.ToggleFloorLockStateById(floorId);
        }
    }
}