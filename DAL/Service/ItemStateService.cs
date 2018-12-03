using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Context;
using DAL.Extensions;
using Microsoft.EntityFrameworkCore;
using Common.Model.Dto;
using Model.Model;

namespace DAL.Service
{
    public class ItemStateService : IItemStateService
    {
        private IAlpContext _context;

        public ItemStateService(IAlpContext context)
        {
            _context = context;
        }

        public async Task<AlpApiResponse<ItemStateDto>> AddNewItemState(ItemStateDto dto)
        {
            var response = new AlpApiResponse<ItemStateDto>();
            try
            {
                dto.Validate();

                var entity = dto.DtoToEntity();
                await _context.ItemState.AddAsync(entity);
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

        public async Task<AlpApiResponse> DeleteItemStateById(int itemStateId)
        {
            var response = new AlpApiResponse();
            try
            {
                var entity = await _context.ItemState.FirstOrDefaultAsync(itemState => itemState.ItemStateId == itemStateId);
                _context.Remove(entity);
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

        public async Task<AlpApiResponse<List<ItemStateDto>>> GetAllItemStates()
        {
            var response = new AlpApiResponse<List<ItemStateDto>>();
            try
            {
                var entites = await _context.ItemState.AsNoTracking().ToListAsync();
                response.Value = entites.Select(e => e.EntityToDto()).ToList();
            }
            catch (Exception e)
            {
                //TODO: logging
                response.Message = e.Message;
                response.Success = false;
            }

            return response;
        }

        public async Task<AlpApiResponse<List<ItemStateDto>>> GetAvailableItemStates()
        {
            var response = new AlpApiResponse<List<ItemStateDto>>();
            try
            {
                var entites = await _context.ItemState.AsNoTracking().Where(itemState => !itemState.Locked).ToListAsync();
                response.Value = entites.Select(e => e.EntityToDto()).ToList();
            }
            catch (Exception e)
            {
                //TODO: logging
                response.Message = e.Message;
                response.Success = false;
            }

            return response;
        }

        public async Task<AlpApiResponse<ItemStateDto>> GetItemStateById(int itemStateId)
        {
            var response = new AlpApiResponse<ItemStateDto>();
            try
            {
                var entity = await _context.ItemState.FirstOrDefaultAsync(itemState => itemState.ItemStateId == itemStateId);
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

        public async Task<AlpApiResponse> UpdateItemState(ItemStateDto dto)
        {
            var response = new AlpApiResponse();
            try
            {
                dto.Validate();
                
                var updatedEntity = await _context.ItemState.FirstOrDefaultAsync(itemState => itemState.ItemStateId == dto.Id);
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

        public async Task<AlpApiResponse> ToggleItemStateLockStateById(int itemStateId)
        {
            var response = new AlpApiResponse();
            try
            {
                var getByIdResponse = await GetItemStateById(itemStateId);
                if (!getByIdResponse.Success)
                {
                    response.Success = getByIdResponse.Success;
                    response.Message = getByIdResponse.Message;
                    return response;
                }

                var itemState = getByIdResponse.Value;
                itemState.Locked = !itemState.Locked;
                var updateResponse = await UpdateItemState(itemState);
                if (!updateResponse.Success)
                {
                    response.Success = updateResponse.Success;
                    response.Message = updateResponse.Message;
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
