using DAL.Entity;
using DAL.Context;
using Model.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using DAL.Extensions;
using Microsoft.EntityFrameworkCore;

namespace DAL.Service
{
    public class BuildingService: BaseService<Building>, IBuildingService
    {
        public BuildingService(IAlpContext context)
        {
            _context = context;
        }

        public async Task DeleteBuildingById(int buildingId)
        {
            await Remove(building => building.BuildingID == buildingId);
        }

        public async Task<List<BuildingDto>> GetAllBuildings()
        {
            //var buildings = await _context.Building.Include(building => building.Location).ToListAsync();
            //return buildings.Select(building => building.EntityToDto()).ToList();

            var buildings = await GetAll(navigationProperties: building => building.Location);
            return buildings.Select(building => building.EntityToDto()).ToList();
        }

        public async Task<BuildingDto> GetBuildingById(int buildingId)
        {
            var entity = await GetSingle(building => building.BuildingID == buildingId);
            return entity.EntityToDto();
        }

        public async Task<BuildingDto> InsertNewBuilding(BuildingDto building)
        {
            var entity = await InsertNew(building.DtoToEntity());
            return entity.EntityToDto();
        }

        public async Task ToggleLocationLockStateById(int buildingId)
        {
            var building = await GetBuildingById(buildingId);
            building.Locked = !building.Locked;
            await UpdateBuilding(building);
        }

        public async Task<BuildingDto> UpdateBuilding(BuildingDto dto)
        {
            var updatedEntity = await _context.Building.FirstOrDefaultAsync(building => building.BuildingID == dto.Id);
            updatedEntity.UpdateEntityByDto(dto);
            await _context.SaveChangesAsync();
            return updatedEntity.EntityToDto();
        }
    }
}
