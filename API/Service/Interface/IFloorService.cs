using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Entity;
using Common.Model.Dto;
using Model.Model;

namespace API.Service
{
    public interface IFloorService
    {
        Task<AlpApiResponse<List<FloorDto>>> GetAllFloors();
        Task<AlpApiResponse<List<FloorDto>>> GetAvailableFloors();
        Task<AlpApiResponse<FloorDto>> GetFloorById(int FloorId);
        Task<AlpApiResponse<FloorDto>> InsertNewFloor(FloorDto Floor);
        Task<AlpApiResponse> DeleteFloorById(int FloorId);
        Task<AlpApiResponse> ToggleFloorLockStateById(int floorId);
        Task<AlpApiResponse> UpdateFloor(FloorDto dto);
    }
}
