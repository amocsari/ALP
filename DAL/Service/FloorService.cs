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
    public class FloorService : BaseService<Floor>, IFloorService
    {
        public FloorService(IAlpContext context)
        {
            _context = context;
        }

        public async Task DeleteFloorById(int floorId)
        {
            try
            {
                await Remove(floor => floor.FloorID == floorId);
            }
            catch (Exception e)
            {
                //TODO: logging
            }
        }

        public async Task<List<FloorDto>> GetAllFloors()
        {
            try
            {
                //var Floors = await _context.Floor.Include(Floor => floor.Building).ToListAsync();
                //return Floors.Select(Floor => floor.EntityToDto()).ToList();

                var floors = await GetAll(floor => floor.Building);
                return floors.Select(floor => floor.EntityToDto()).ToList();
            }
            catch (Exception e)
            {
                //TODO: logging
                return null;
            }
        }

        public async Task<FloorDto> GetFloorById(int floorId)
        {
            try
            {
                var entity = await GetSingle(floor => floor.FloorID == floorId);
                return entity.EntityToDto();
            }
            catch (Exception e)
            {
                //TODO: logging
                return null;
            }
        }

        public async Task<FloorDto> InsertNewFloor(FloorDto floor)
        {
            try
            {
                var entity = await InsertNew(floor.DtoToEntity());
                return entity.EntityToDto();
            }
            catch (Exception e)
            {
                //TODO: logging
                return null;
            }
        }

        public async Task ToggleFloorLockStateById(int floorId)
        {
            try
            {
                var floor = await GetFloorById(floorId);
                floor.Locked = !floor.Locked;
                await UpdateFloor(floor);
            }
            catch (Exception e)
            {
                //TODO: logging
            }
        }

        public async Task<FloorDto> UpdateFloor(FloorDto dto)
        {
            try
            {

                var updatedEntity = await _context.Floor.FirstOrDefaultAsync(floor => floor.FloorID == dto.Id);
                updatedEntity.UpdateEntityByDto(dto);
                await _context.SaveChangesAsync();
                return updatedEntity.EntityToDto();
            }
            catch (Exception e)
            {
                //TODO: logging
                return null;
            }
        }
    }
}
