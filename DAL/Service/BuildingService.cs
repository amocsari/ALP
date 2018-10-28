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

        public void DeleteBuildingById(int buildingId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<BuildingDto>> GetAllBuildings()
        {
            //var buildings = await _context.Building.Include(building => building.Location).ToListAsync();
            //return buildings.Select(building => building.EntityToDto()).ToList();

            var buildings = await GetAll(navigationProperties: building => building.Location);
            return buildings.Select(building => building.EntityToDto()).ToList();
        }

        public Task<BuildingDto> GetBuildingById(int buildingId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<BuildingDto> InsertNewBuilding(BuildingDto building)
        {
            var entity = await InsertNew(building.DtoToEntity());
            return entity.EntityToDto();
        }
    }
}
