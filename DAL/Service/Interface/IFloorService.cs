using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Entity;
using Common.Model.Dto;

namespace DAL.Service
{
    public interface IFloorService : IBaseService<Floor>
    {
        Task<List<FloorDto>> GetAllFloors();
        Task<FloorDto> GetFloorById(int FloorId);
        Task<FloorDto> InsertNewFloor(FloorDto Floor);
        Task DeleteFloorById(int FloorId);
        Task ToggleFloorLockStateById(int floorId);
        Task<FloorDto> UpdateFloor(FloorDto dto);
    }
}
