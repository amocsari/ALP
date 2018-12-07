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
    /// <summary>
    /// Handles item related database methods
    /// </summary>
    public class ItemService : IItemService
    {
        private readonly IAlpContext _context;
        private readonly ILogger<ItemService> _logger;

        public ItemService(IAlpContext context, ILogger<ItemService> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// gets an item by id
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
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

        /// <summary>
        /// adds a new item
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
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

        /// <summary>
        /// filters items
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
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

                if (info == null)
                {
                    response.Success = false;
                    response.Message = "Hibás szűrőparaméter!";
                    return response;
                }

                var includesIds = info.Id != null && info.Id.Count > 0;
                var isManufacturerAndTypeSpecified = !string.IsNullOrEmpty(info.ManufacturerAndType);

                var areBuildingsSpecified = info.Buildings != null && info.Buildings.Count > 0;
                var areDepartmentsSpecified = info.Departments != null && info.Departments.Count > 0;
                var areFloorsSpecified = info.Floors != null && info.Floors.Count > 0;
                var areItemNaturesSpecified = info.ItemNatures != null && info.ItemNatures.Count > 0;
                var areItemStatesSpecified = info.ItemStates != null && info.ItemStates.Count > 0;
                var areItemTypesSpecified = info.ItemTypes != null && info.ItemTypes.Count > 0;
                var areLocationsSpecified = info.Locations != null && info.Locations.Count > 0;
                var areSectionsSpecified = info.Sections != null && info.Sections.Count > 0;

                var buildingIds = info.Buildings.Select(b => b.Id).ToList();
                var departmentIds = info.Departments.Select(b => b.Id).ToList();
                var floorIds = info.Floors.Select(b => b.Id).ToList();
                var itemNatureIds = info.ItemNatures.Select(b => b.Id).ToList();
                var itemStateIds = info.ItemStates.Select(b => b.Id).ToList();
                var itemTypeIds = info.ItemTypes.Select(b => b.Id).ToList();
                var locationIds = info.Locations.Select(b => b.Id).ToList();
                var sectionIds = info.Sections.Select(b => b.Id).ToList();

                List<Item> entities;
                if (!includesIds)
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
                                           info.DateOfScrapMin >= item.DateOfScrap)
                                       && (!areBuildingsSpecified || (item.BuildingId.HasValue && buildingIds.Contains(item.BuildingId.Value)))
                                       && (!areDepartmentsSpecified || (item.DepartmentId.HasValue && departmentIds.Contains(item.DepartmentId.Value)))
                                       && (!areItemNaturesSpecified || (item.ItemNatureId.HasValue && itemNatureIds.Contains(item.ItemNatureId.Value)))
                                       && (!areItemTypesSpecified || (item.ItemTypeId.HasValue && itemTypeIds.Contains(item.ItemTypeId.Value)))
                                       && (!areItemStatesSpecified || itemStateIds.Contains(item.ItemStateId))
                                       && (!areSectionsSpecified || (item.SectionId.HasValue && sectionIds.Contains(item.SectionId.Value)))
                                       && (!areFloorsSpecified || (item.FloorId.HasValue && floorIds.Contains(item.FloorId.Value))))
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

        /// <summary>
        /// adds excel imported items
        /// </summary>
        /// <param name="importedItems"></param>
        /// <returns></returns>
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

        /// <summary>
        /// gets all the items an employee has
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public async Task<AlpApiResponse<List<ItemDto>>> GetItemsByEmployeeId(int employeeId)
        {
            var response = new AlpApiResponse<List<ItemDto>>();
            try
            {
                _logger.LogDebug(new
                {
                    action = nameof(GetItemsByEmployeeId),
                    employeeId
                }.ToString());

                var items = await _context.Item.AsNoTracking()
                        .Include(item => item.Department)
                        .Include(item => item.Employee)
                        .Include(item => item.Building)
                        .Include(item => item.Section)
                        .Include(item => item.Floor)
                        .Include(item => item.ItemNature)
                        .Include(item => item.ItemState)
                        .Include(item => item.ItemType)
                        .Where(item => item.EmployeeId == employeeId).ToListAsync();

                var dtoList = items.Select(item => item.EntityToDto()).ToList();

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

        /// <summary>
        /// change items owner to departmentchief
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public async Task<AlpApiResponse> ChangeOwnerToDepartmentChief(int itemId)
        {
            var response = new AlpApiResponse();
            try
            {
                _logger.LogDebug(new
                {
                    action = nameof(ChangeOwnerToDepartmentChief),
                    itemId
                }.ToString());

                var item = await _context.Item.Include(it => it.Department).FirstOrDefaultAsync(it => it.ItemId == itemId);
                if (item == null)
                {
                    response.Success = false;
                    response.Message = "A tárgy nem található az adatbázisban!";
                    return response;
                }

                if (item.Department == null)
                {
                    response.Success = false;
                    response.Message = "A tárgy nincs osztályhoz rendelve, így nem lehet az osztályvezetőjére terhelni!";
                    return response;
                }

                if (!item.Department.EmployeeId.HasValue)
                {
                    response.Success = false;
                    response.Message = "A választott osztálynak nincs tárolva az osztályvezetője!";
                    return response;
                }

                item.EmployeeId = item.Department.EmployeeId;
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

        /// <summary>
        /// change itms departement to payloadid
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="payLoadId"></param>
        /// <returns></returns>
        public async Task<AlpApiResponse> ChangeDepartment(int itemId, int? payLoadId)
        {
            var response = new AlpApiResponse();
            try
            {
                _logger.LogDebug(new
                {
                    action = nameof(ChangeOwnerToDepartmentChief),
                    itemId
                }.ToString());

                var item = await _context.Item.Include(it => it.Department).FirstOrDefaultAsync(it => it.ItemId == itemId);
                if (item == null)
                {
                    response.Success = false;
                    response.Message = "A tárgy nem található az adatbázisban!";
                    return response;
                }

                var department = await _context.Department.FirstOrDefaultAsync(dep => dep.DepartmentId == payLoadId);
                if (department == null)
                {
                    response.Success = false;
                    response.Message = "Az osztály nem található az adatbázisban!";
                    return response;
                }

                item.DepartmentId = department.DepartmentId;
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

        /// <summary>
        /// change items owner to payloadid
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="payLoadId"></param>
        /// <returns></returns>
        public async Task<AlpApiResponse> ChangeOwner(int itemId, int? payLoadId)
        {
            var response = new AlpApiResponse();
            try
            {
                _logger.LogDebug(new
                {
                    action = nameof(ChangeOwnerToDepartmentChief),
                    itemId
                }.ToString());

                var item = await _context.Item.Include(it => it.Department).FirstOrDefaultAsync(it => it.ItemId == itemId);
                if (item == null)
                {
                    response.Success = false;
                    response.Message = "A tárgy nem található az adatbázisban!";
                    return response;
                }

                var employee = await _context.Employee.FirstOrDefaultAsync(emp => emp.EmployeeId == payLoadId);
                if (employee == null)
                {
                    response.Success = false;
                    response.Message = "Az munkavállaló nem található az adatbázisban!";
                    return response;
                }

                item.EmployeeId = employee.EmployeeId;
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

        /// <summary>
        /// set items dateofscrap to now
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public async Task<AlpApiResponse> Scrap(int itemId)
        {
            var response = new AlpApiResponse();
            try
            {
                _logger.LogDebug(new
                {
                    action = nameof(ChangeOwnerToDepartmentChief),
                    itemId
                }.ToString());

                var item = await _context.Item.Include(it => it.Department).FirstOrDefaultAsync(it => it.ItemId == itemId);
                if (item == null)
                {
                    response.Success = false;
                    response.Message = "A tárgy nem található az adatbázisban!";
                    return response;
                }

                item.DateOfScrap = DateTime.Now;
                item.ItemStateId = 1;

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
    }
}
