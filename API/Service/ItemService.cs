using DAL.Entity;
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
using Employee = DAL.Entity.Employee;

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

                entity.DateOfCreation = DateTime.Now;

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
                                       && (!info.BruttoPriceMin.HasValue ||
                                           (item.BruttoPrice.HasValue && item.BruttoPrice >= info.BruttoPriceMin))
                                       && (!info.BruttoPriceMax.HasValue ||
                                           (item.BruttoPrice.HasValue && item.BruttoPrice <= info.BruttoPriceMax))
                                       && (!info.DateOfCreationMax.HasValue ||
                                           (item.DateOfCreation.HasValue &&
                                            info.DateOfCreationMax >= item.DateOfCreation))
                                       && (!info.DateOfCreationMax.HasValue ||
                                           (item.DateOfCreation.HasValue &&
                                            info.DateOfCreationMin <= item.DateOfCreation))
                                       && (!info.YearOfManufactureMax.HasValue ||
                                           (item.ProductionYear.HasValue &&
                                            info.YearOfManufactureMax >= item.ProductionYear))
                                       && (!info.YearOfManufactureMin.HasValue ||
                                           (item.ProductionYear.HasValue &&
                                            info.YearOfManufactureMin <= item.ProductionYear))
                                       && (!info.DateOfScrapMax.HasValue ||
                                           (item.DateOfScrap.HasValue && info.DateOfScrapMax >= item.DateOfScrap))
                                       && (!info.DateOfCreationMin.HasValue || item.DateOfScrap.HasValue &&
                                           info.DateOfScrapMin >= item.DateOfScrap))
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

        public async Task<AlpApiResponse<List<ItemDto>>> ImportItems(List<ImportedItem> importedItems)
        {
            var response = new AlpApiResponse<List<ItemDto>>();
            try
            {
                _logger.LogDebug(new
                {
                    action = nameof(ImportItems),
                    importedItems
                }.ToString());

                var dtoList = new List<ItemDto>();
                var employeeList = new List<Employee>();

                foreach (var importedItem in importedItems)
                {
                    var containedItem = await _context.Item.FirstOrDefaultAsync(item =>
                           item.YellowNumber == importedItem.YellowNumber
                        || item.SerialNumber == importedItem.SerialNumber
                        || item.InventoryNumber == importedItem.InventoryNumber
                        || item.OldInventoryNumber == importedItem.OldInventoryNumber);

                    if (containedItem != null)
                    {
                        continue;
                    }

                    var newItem = new Item
                    {
                        ItemName = importedItem.Name,
                        YellowNumber = importedItem.YellowNumber,
                        SerialNumber = importedItem.SerialNumber,
                        InventoryNumber = importedItem.InventoryNumber,
                        OldInventoryNumber = importedItem.OldInventoryNumber,
                        DateOfCreation = DateTime.Now,
                        ItemStateId = 1
                    };

                    if (!string.IsNullOrEmpty(importedItem.OwnerName))
                    {
                        var owner = await _context.Employee.FirstOrDefaultAsync(employee => employee.EmployeeName == importedItem.OwnerName);
                        if (owner == null)
                        {
                            owner = employeeList.FirstOrDefault(employee => employee.EmployeeName == importedItem.OwnerName);
                        }

                        if (owner != null)
                        {
                            newItem.EmployeeId = owner.EmployeeId;
                        }
                        else
                        {
                            var newEmployee = new Employee
                            {
                                EmployeeName = importedItem.OwnerName
                            };
                            var insertedRow = await _context.Employee.AddAsync(newEmployee);
                            employeeList.Add(insertedRow.Entity);
                            newItem.EmployeeId = insertedRow.Entity.EmployeeId;
                            insertedRow.State = EntityState.Detached;                            
                            
                        }
                    }

                    var entity = (await _context.Item.AddAsync(newItem)).Entity;
                    var dto = entity?.EntityToDto();

                    if (dto != null)
                    {
                        dtoList.Add(dto);
                    }
                }
                employeeList.Clear();
                await _context.SaveChangesAsync();
                response.Value = dtoList;
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
