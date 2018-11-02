using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Entity;
using Model.Dto;

namespace DAL.Service
{
    public interface IBuildingService : IBaseService<Building>
    {
        Task<List<BuildingDto>> GetAllBuildings();
        Task<BuildingDto> GetBuildingById(int buildingId);
        Task<BuildingDto> InsertNewBuilding(BuildingDto building);
        Task DeleteBuildingById(int buildingId);
        Task ToggleLocationLockStateById(int locationId);
        Task<BuildingDto> UpdateBuilding(BuildingDto dto);
    }
}
