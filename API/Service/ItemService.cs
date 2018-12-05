﻿using DAL.Entity;
using DAL.Context;
using Common.Model.Dto;
using System.Threading.Tasks;
using System;
using DAL.Extensions;
using Common.Model;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Model.Model;
using Microsoft.Extensions.Logging;

namespace API.Service
{
    public class ItemService : IItemService
    {
        private readonly IAlpContext _context;
        private readonly ILogger<ItemService> _logger;

        public ItemService(IAlpContext context, ILogger<ItemService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<AlpApiResponse<ItemDto>> GetItemById(int itemId)
        {
            var response = new AlpApiResponse<ItemDto>();
            try
            {
                _logger.LogDebug(new
                {
                    action = nameof(GetItemById),
                    itemId
                }.ToString());

                var entity = await _context.Item.FirstAsync(item => item.ItemId == itemId);

                if (entity == null)
                {
                    response.Success = false;
                    response.Message = "Ez a leltárelem nincs benne az adatbázisban!";
                    return response;
                }

                response.Value = entity.EntityToDto();
            }
            catch (Exception e)
            {
                _logger.LogError(new
                {
                    exception = e,
                    message = e.Message,
                    innerException = e,
                    innerExceptionMessage = e.InnerException?.Message
                }.ToString());
                response.Message = e.Message;
                response.Success = false;
            }

            return response;
        }

        public async Task<AlpApiResponse> AddNewItem(ItemDto dto)
        {
            var response = new AlpApiResponse();
            try
            {
                _logger.LogDebug(new
                {
                    action = nameof(AddNewItem),
                    dto = dto?.ToString()
                }.ToString());

                dto.Validate();

                var entity = dto.DtoToEntity();

                entity.Building = null;
                entity.Floor = null;
                entity.ItemNature = null;
                entity.ItemState = null;
                entity.ItemType = null;
                entity.Department = null;
                entity.Employee = null;
                entity.Section = null;

                await _context.Item.AddAsync(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(new
                {
                    exception = e,
                    message = e.Message,
                    innerException = e,
                    innerExceptionMessage = e.InnerException?.Message
                }.ToString());
                response.Message = e.Message;
                response.Success = false;
            }

            return response;
        }

        public async Task<AlpApiResponse<List<ItemDto>>> FindItemsForDisplay(InventoryItemFilterInfo info)
        {
            var response = new AlpApiResponse<List<ItemDto>>();
            try
            {
                _logger.LogDebug(new
                {
                    action = nameof(FindItemsForDisplay),
                    info = info?.ToString()
                }.ToString());

                var includesIds = info.Id != null && info.Id.Count > 0;
                var isManufacturerAndTypeSpecified = !string.IsNullOrEmpty(info.ManufacturerAndType);
                List<Item> entities;
                if (!includesIds)
                {
                    //TODO: TRANCTUATETIME
                    entities = await _context.Item.AsNoTracking()
                                                    .Include(item => item.Department)
                                                    .Include(item => item.Employee)
                                                    .Include(item => item.Building)
                                                    .Include(item => item.Section)
                                                    .Include(item => item.Floor)
                                                    .Include(item => item.ItemNature)
                                                    .Include(item => item.ItemState)
                                                    .Include(item => item.ItemType)
                                                    .Where(item => (!isManufacturerAndTypeSpecified
                                                                    || item.Manufacturer.Contains(info.ManufacturerAndType)
                                                                    || item.ModelType.Contains(info.ManufacturerAndType))
                                                                && (!info.BruttoPriceMin.HasValue || (item.BruttoPrice.HasValue && item.BruttoPrice >= info.BruttoPriceMin))
                                                                && (!info.BruttoPriceMax.HasValue || (item.BruttoPrice.HasValue && item.BruttoPrice <= info.BruttoPriceMax))
                                                                && (!info.DateOfCreationMax.HasValue || (item.DateOfCreation.HasValue && info.DateOfCreationMax >= item.DateOfCreation))
                                                                && (!info.DateOfCreationMax.HasValue || (item.DateOfCreation.HasValue && info.DateOfCreationMin <= item.DateOfCreation))
                                                                && (!info.YearOfManufactureMax.HasValue || (item.ProductionYear.HasValue && info.YearOfManufactureMax >= item.ProductionYear))
                                                                && (!info.YearOfManufactureMin.HasValue || (item.ProductionYear.HasValue && info.YearOfManufactureMin <= item.ProductionYear))
                                                                && (!info.DateOfScrapMax.HasValue || (item.DateOfScrap.HasValue && info.DateOfScrapMax >= item.DateOfScrap))
                                                                && (!info.DateOfCreationMin.HasValue || item.DateOfScrap.HasValue && info.DateOfScrapMin >= item.DateOfScrap))
                                                    .ToListAsync();
                }
                else
                {
                    entities = await _context.Item.AsNoTracking()
                                                    .Include(item => item.Department)
                                                    .Include(item => item.Employee)
                                                    .Include(item => item.Building)
                                                    .Include(item => item.Section)
                                                    .Include(item => item.Floor)
                                                    .Include(item => item.ItemNature)
                                                    .Include(item => item.ItemState)
                                                    .Include(item => item.ItemType)
                                                    .Where(item => info.Id.Contains(item.InventoryNumber)
                                                                 || info.Id.Contains(item.OldInventoryNumber)
                                                                 || info.Id.Contains(item.SerialNumber)
                                                                 || info.Id.Contains(item.YellowNumber.ToString())
                                                                 || info.Id.Contains(item.AccreditationNumber))
                                                    .ToListAsync();
                }

                var items = entities.Select(entity => entity.EntityToDto()).ToList();
                response.Value = items;
            }
            catch (Exception e)
            {
                _logger.LogError(new
                {
                    exception = e,
                    message = e.Message,
                    innerException = e,
                    innerExceptionMessage = e.InnerException?.Message
                }.ToString());
                response.Message = e.Message;
                response.Success = false;
            }

            return response;
        }
    }
}