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
    public class FloorService : BaseService<Floor>, IFloorService
    {
        public FloorService(IAlpContext context)
        {
            _context = context;
        }

        public async Task DeleteFloorById(int floorId)
        {
            await Remove(floor => floor.FloorID == floorId);
        }

        public async Task<List<FloorDto>> GetAllFloors()
        {
            //var Floors = await _context.Floor.Include(Floor => floor.Building).ToListAsync();
            //return Floors.Select(Floor => floor.EntityToDto()).ToList();

            var floors = await GetAll(navigationProperties: floor => floor.Building);
            return floors.Select(floor => floor.EntityToDto()).ToList();
        }

        public async Task<FloorDto> GetFloorById(int floorId)
        {
            var entity = await GetSingle(floor => floor.FloorID == floorId);
            return entity.EntityToDto();
        }

        public async Task<FloorDto> InsertNewFloor(FloorDto floor)
        {
            var entity = await InsertNew(floor.DtoToEntity());
            return entity.EntityToDto();
        }

        public async Task ToggleFloorLockStateById(int floorId)
        {
            var floor = await GetFloorById(floorId);
            floor.Locked = !floor.Locked;
            await UpdateFloor(floor);
        }

        public async Task<FloorDto> UpdateFloor(FloorDto dto)
        {
            var updatedEntity = await _context.Floor.FirstOrDefaultAsync(floor => floor.FloorID == dto.Id);
            updatedEntity.UpdateEntityByDto(dto);
            await _context.SaveChangesAsync();
            return updatedEntity.EntityToDto();
        }
    }
}
