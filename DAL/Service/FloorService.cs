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
    public class FloorService : IFloorService
    {
        private IAlpContext _context;

        public FloorService(IAlpContext context)
        {
            _context = context;
        }

        public async Task DeleteFloorById(int floorId)
        {
            try
            {
                var entity = await _context.Floor.FirstOrDefaultAsync(floor => floor.FloorId == floorId);
                _context.Floor.Remove(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                //TODO: logging
            }
        }

        public async Task<List<FloorDto>> GetAllFloors()
        {
            try
            {
                var floors = await _context.Floor.AsNoTracking().Include(floor => floor.Building).ToListAsync();
                return floors.Select(floor => floor.EntityToDto()).ToList();
            }
            catch (Exception)
            {
                //TODO: logging
                return null;
            }
        }

        public async Task<List<FloorDto>> GetAvailableFloors()
        {
            try
            {
                var floors = await _context.Floor.AsNoTracking().Include(floor => floor.Building).Where(floor => !floor.Locked).ToListAsync();
                return floors.Select(floor => floor.EntityToDto()).ToList();
            }
            catch (Exception)
            {
                //TODO: logging
                return null;
            }
        }

        public async Task<FloorDto> GetFloorById(int floorId)
        {
            try
            {
                var entity = await _context.Floor.FirstOrDefaultAsync(floor => floor.FloorId == floorId);
                return entity.EntityToDto();
            }
            catch (Exception)
            {
                //TODO: logging
                return null;
            }
        }

        public async Task<FloorDto> InsertNewFloor(FloorDto floor)
        {
            try
            {
                var entity = floor.DtoToEntity();
                await _context.Floor.AddAsync(entity);
                await _context.SaveChangesAsync();
                return entity.EntityToDto();
            }
            catch (Exception)
            {
                //TODO: logging
                return null;
            }
        }

        public async Task<FloorDto> UpdateFloor(FloorDto dto)
        {
            try
            {

                var updatedEntity = await _context.Floor.FirstOrDefaultAsync(floor => floor.FloorId == dto.Id);
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

        public async Task ToggleFloorLockStateById(int floorId)
        {
            try
            {
                var floor = await GetFloorById(floorId);
                floor.Locked = !floor.Locked;
                await UpdateFloor(floor);
            }
            catch (Exception)
            {
                //TODO: logging
            }
        }
    }
}
