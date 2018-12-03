using DAL.Context;
using Common.Model.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using DAL.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using Model.Model;

namespace DAL.Service
{
    public class BuildingService : IBuildingService
    {
        private IAlpContext _context;

        public BuildingService(IAlpContext context)
        {
            _context = context;
        }

        public async Task<AlpApiResponse> DeleteBuildingById(int buildingId)
        {
            var response = new AlpApiResponse();
            try
            {
                var entity = await _context.Building.FirstOrDefaultAsync(building => building.BuildingId == buildingId);
                _context.Building.Remove(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                //TODO: logging
                response.Message = e.Message;
                response.Success = false;
            }

            return response;
        }

        public async Task<AlpApiResponse<List<BuildingDto>>> GetAllBuildings()
        {
            var response = new AlpApiResponse<List<BuildingDto>>();
            try
            {
                var buildings = await _context.Building.AsNoTracking().Include(building => building.Location).ToListAsync();
                response.Value = buildings.Select(building => building.EntityToDto()).ToList();
            }
            catch (Exception e)
            {
                //TODO: logging
                response.Message = e.Message;
                response.Success = false;
            }

            return response;
        }

        public async Task<AlpApiResponse<List<BuildingDto>>> GetAvailableBuildings()
        {
            var response = new AlpApiResponse<List<BuildingDto>>();
            try
            {
                var buildings = await _context.Building.AsNoTracking().Include(building => building.Location).Where(building => !building.Locked).ToListAsync();
                response.Value = buildings.Select(building => building.EntityToDto()).ToList();
            }
            catch (Exception e)
            {
                //TODO: logging
                response.Message = e.Message;
                response.Success = false;
            }

            return response;
        }

        public async Task<AlpApiResponse<BuildingDto>> GetBuildingById(int buildingId)
        {
            var response = new AlpApiResponse<BuildingDto>();
            try
            {
                var entity = await _context.Building.FirstOrDefaultAsync(building => building.BuildingId == buildingId);
                response.Value = entity.EntityToDto();
            }
            catch (Exception e)
            {
                //TODO: logging
                response.Message = e.Message;
                response.Success = false;
            }

            return response;
        }

        public async Task<AlpApiResponse<BuildingDto>> InsertNewBuilding(BuildingDto dto)
        {
            var response = new AlpApiResponse<BuildingDto>();

            try
            {
                dto.Validate();

                var entity = dto.DtoToEntity();
                entity.Location = null;
                await _context.Building.AddAsync(entity);
                await _context.SaveChangesAsync();
                response.Value = entity.EntityToDto();
            }
            catch (Exception e)
            {
                //TODO: logging
                response.Message = e.Message;
                response.Success = false;
            }

            return response;
        }

        public async Task<AlpApiResponse> UpdateBuilding(BuildingDto dto)
        {
            var response = new AlpApiResponse();
            try
            {
                dto.Validate();

                var updatedEntity = await _context.Building.Include(building => building.Location).FirstOrDefaultAsync(building => building.BuildingId == dto.Id);
                updatedEntity.UpdateEntityByDto(dto);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                //TODO: logging
                response.Message = e.Message;
                response.Success = false;
            }

            return response;
        }

        public async Task<AlpApiResponse> ToggleBuildingLockStateById(int buildingId)
        {
            var response = new AlpApiResponse();
            try
            {
                var getByIdReply = await GetBuildingById(buildingId);
                if (!getByIdReply.Success)
                {
                    response.Success = getByIdReply.Success;
                    response.Message = getByIdReply.Message;
                    return response;
                }

                var building = getByIdReply.Value;

                building.Locked = !building.Locked;

                var updateReply = await UpdateBuilding(building);
                if (!updateReply.Success)
                {
                    response.Success = updateReply.Success;
                    response.Message = updateReply.Message;
                    return response;
                }
            }
            catch (Exception e)
            {
                //TODO: logging
                response.Message = e.Message;
                response.Success = false;
            }

            return response;
        }
    }
}
