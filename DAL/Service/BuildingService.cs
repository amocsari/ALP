using DAL.Entity;
using DAL.Context;
using Common.Model.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using DAL.Extensions;
using Microsoft.EntityFrameworkCore;
using System;

namespace DAL.Service
{
    public class BuildingService : IBuildingService
    {
        private IAlpContext _context;

        public BuildingService(IAlpContext context)
        {
            _context = context;
        }

        public async Task DeleteBuildingById(int buildingId)
        {
            try
            {
                var entity = await _context.Building.FirstOrDefaultAsync(building => building.BuildingId == buildingId);
                _context.Building.Remove(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                //TODO: logging
            }
        }

        public async Task<List<BuildingDto>> GetAllBuildings()
        {
            try
            {
                var buildings = await _context.Building.AsNoTracking().Include(building => building.Location).ToListAsync();
                return buildings.Select(building => building.EntityToDto()).ToList();
            }
            catch (Exception)
            {
                //TODO: logging
                return null;
            }
        }

        public async Task<List<BuildingDto>> GetAvailableBuildings()
        {
            try
            {
                var buildings = await _context.Building.AsNoTracking().Include(building => building.Location).Where(building => !building.Locked).ToListAsync();
                return buildings.Select(building => building.EntityToDto()).ToList();
            }
            catch (Exception)
            {
                //TODO: logging
                return null;
            }
        }

        public async Task<BuildingDto> GetBuildingById(int buildingId)
        {
            try
            {
                var entity = await _context.Building.FirstOrDefaultAsync(building => building.BuildingId == buildingId);
                return entity.EntityToDto();
            }
            catch (Exception)
            {
                //TODO: logging
                return null;
            }
        }

        public async Task<BuildingDto> InsertNewBuilding(BuildingDto building)
        {
            try
            {
                var entity = building.DtoToEntity();
                await _context.Building.AddAsync(entity);
                await _context.SaveChangesAsync();
                return entity.EntityToDto();
            }
            catch (Exception e)
            {
                //TODO: logging
                return null;
            }
        }

        public async Task<BuildingDto> UpdateBuilding(BuildingDto dto)
        {
            try
            {
                var updatedEntity = await _context.Building.FirstOrDefaultAsync(building => building.BuildingId == dto.Id);
                updatedEntity.UpdateEntityByDto(dto);
                await _context.SaveChangesAsync();
                return updatedEntity.EntityToDto();
            }
            catch (Exception)
            {
                //TODO: logging
                return null;
            }
        }

        public async Task ToggleBuildingLockStateById(int buildingId)
        {
            try
            {
                var building = await GetBuildingById(buildingId);
                building.Locked = !building.Locked;
                await UpdateBuilding(building);
            }
            catch (Exception)
            {
                //TODO: logging
            }
        }
    }
}
