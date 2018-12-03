using DAL.Entity;
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
    public class FloorService : IFloorService
    {
        private IAlpContext _context;

        public FloorService(IAlpContext context)
        {
            _context = context;
        }

        public async Task<AlpApiResponse> DeleteFloorById(int floorId)
        {
            var response = new AlpApiResponse();

            try
            {
                var entity = await _context.Floor.FirstOrDefaultAsync(floor => floor.FloorId == floorId);
                _context.Floor.Remove(entity);
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

        public async Task<AlpApiResponse<List<FloorDto>>> GetAllFloors()
        {
            var response = new AlpApiResponse<List<FloorDto>>();

            try
            {
                var floors = await _context.Floor.AsNoTracking().Include(floor => floor.Building).ToListAsync();
                response.Value = floors.Select(floor => floor.EntityToDto()).ToList();
            }
            catch (Exception e)
            {
                //TODO: logging
                response.Message = e.Message;
                response.Success = false;
            }

            return response;
        }

        public async Task<AlpApiResponse<List<FloorDto>>> GetAvailableFloors()
        {
            var response = new AlpApiResponse<List<FloorDto>>();

            try
            {
                var floors = await _context.Floor.AsNoTracking().Include(floor => floor.Building).Where(floor => !floor.Locked).ToListAsync();
                response.Value = floors.Select(floor => floor.EntityToDto()).ToList();
            }
            catch (Exception e)
            {
                //TODO: logging
                response.Message = e.Message;
                response.Success = false;
            }

            return response;
        }

        public async Task<AlpApiResponse<FloorDto>> GetFloorById(int floorId)
        {
            var response = new AlpApiResponse<FloorDto>();

            try
            {
                var entity = await _context.Floor.FirstOrDefaultAsync(floor => floor.FloorId == floorId);
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

        public async Task<AlpApiResponse<FloorDto>> InsertNewFloor(FloorDto dto)
        {
            var response = new AlpApiResponse<FloorDto>();

            try
            {
                dto.Validate();

                var entity = dto.DtoToEntity();
                entity.Building = null;
                await _context.Floor.AddAsync(entity);
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

        public async Task<AlpApiResponse> UpdateFloor(FloorDto dto)
        {
            var response = new AlpApiResponse();

            try
            {
                dto.Validate();

                var updatedEntity = await _context.Floor.FirstOrDefaultAsync(floor => floor.FloorId == dto.Id);
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

        public async Task<AlpApiResponse> ToggleFloorLockStateById(int floorId)
        {
            var response = new AlpApiResponse();

            try
            {
                var getByIdResponse = await GetFloorById(floorId);
                if (!getByIdResponse.Success)
                {
                    response.Success = getByIdResponse.Success;
                    response.Message = getByIdResponse.Message;
                    return response;
                }

                var floor = getByIdResponse.Value;
                
                floor.Locked = !floor.Locked;
                var updateReply = await UpdateFloor(floor);
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
