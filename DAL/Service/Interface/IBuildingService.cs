using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Model.Dto;
using Model.Model;

namespace DAL.Service
{
    public interface IBuildingService
    {
        Task<AlpApiResponse<List<BuildingDto>>> GetAllBuildings();
        Task<AlpApiResponse<List<BuildingDto>>> GetAvailableBuildings();
        Task<AlpApiResponse<BuildingDto>> GetBuildingById(int buildingId);
        Task<AlpApiResponse<BuildingDto>> InsertNewBuilding(BuildingDto building);
        Task<AlpApiResponse> DeleteBuildingById(int buildingId);
        Task<AlpApiResponse> ToggleBuildingLockStateById(int buildingId);
        Task<AlpApiResponse> UpdateBuilding(BuildingDto dto);
    }
}
