using DAL.Entity;
using DAL.Context;
using Common.Model.Dto;
using System.Threading.Tasks;
using System;
using DAL.Extensions;
using Common.Model;
using Model.Model;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using Model.Extensions;

namespace DAL.Service
{
    public class ItemService : BaseService<Item>, IItemService
    {
        public ItemService(IAlpContext context)
        {
            _context = context;
        }

        public async Task<bool> AddNewItem(ItemDto dto)
        {
            try
            {
                var entity = dto.DtoToEntity();

                entity.Building = null;
                entity.Floor = null;
                entity.ItemNature = null;
                entity.ItemState = null;
                entity.ItemType = null;
                entity.Department = null;
                entity.Employee = null;
                entity.Section = null;

                await InsertNew(entity);
            }
            catch (Exception e)
            {
                //TODO: logging
                return false;
            }

            return true;

        }

        public async Task<List<ItemDisplay>> FindItemsForDisplay(InventoryItemFilterInfo info)
        {
            var items = new List<Item>
            {
                new Item
                {
                    AccreditationNumber = "123456",
                    BruttoPrice = 12000,
                    BuildingId = 1,
                    Comment = "wasd",
                    DateOfCreation = DateTime.Today,
                    DateOfScrap = DateTime.Now,
                    FloorId = 1,
                    ItemId = 1,
                    ItemNatureId = 1,
                    ItemName = "This name",
                    ItemStateId = 1,
                    YellowNumber = 501,
                    ItemTypeId = 1,
                    Manufacturer = "HP",
                    ModelType = "COMPAQ",
                    OldInventoryNumber = "00-00112",
                    InventoryNumber = "349G112233",
                    Room = "106",
                    ProductionYear = DateTime.Today,
                    SerialNumber = "SN123456"
                }
            };
            items[0].Building = _context.Building.FirstOrDefault(b => b.BuildingId == 1);
            items[0].Floor = _context.Floor.FirstOrDefault(f => f.FloorId == 1);
            items[0].ItemNature = _context.ItemNature.FirstOrDefault(i => i.ItemNatureId == 1);
            items[0].ItemType = _context.ItemType.FirstOrDefault(i => i.ItemTypeId == 1);
            items[0].ItemState = _context.ItemState.FirstOrDefault(i => i.ItemStateId == 1);

            return items.Select(i => i.EntityToDto().TransformToDisplay()).ToList();

            try
            {
                //TODO: regex szerint szétszedni az azonosítókat
                var includesIds = info.Id != null && info.Id.Count > 0;
                var isManufacturerAndTypeSpecified = string.IsNullOrEmpty(info.ManufacturerAndType);
                List<Item> entities;
                if (!includesIds)
                {
                    //TODO: TRANCTUATETIME
                    entities = await GetByExpression(item => (!isManufacturerAndTypeSpecified
                                                                    || item.Manufacturer.Contains(info.ManufacturerAndType)
                                                                    || item.ModelType.Contains(info.ManufacturerAndType))
                                                                && (!info.BruttoPriceMin.HasValue || (item.BruttoPrice.HasValue && item.BruttoPrice >= info.BruttoPriceMin))
                                                                && (!info.BruttoPriceMax.HasValue || (item.BruttoPrice.HasValue && item.BruttoPrice <= info.BruttoPriceMax))
                                                                && (!info.DateOfCreationMax.HasValue || (item.DateOfCreation.HasValue && info.DateOfCreationMax >= item.DateOfCreation))
                                                                && (!info.DateOfCreationMax.HasValue || (item.DateOfCreation.HasValue && info.DateOfCreationMin <= item.DateOfCreation))
                                                                && (!info.YearOfManufactureMax.HasValue || (item.ProductionYear.HasValue && info.YearOfManufactureMax >= item.ProductionYear))
                                                                && (!info.YearOfManufactureMin.HasValue || (item.ProductionYear.HasValue && info.YearOfManufactureMin <= item.ProductionYear))
                                                                && (!info.DateOfScrapMax.HasValue || (item.DateOfScrap.HasValue && info.DateOfScrapMax >= item.DateOfScrap))
                                                                && (!info.DateOfCreationMin.HasValue || item.DateOfScrap.HasValue && info.DateOfScrapMin >= item.DateOfScrap),
                                                     item => item.Department,
                                                     item => item.Employee,
                                                     item => item.Building,
                                                     item => item.Section,
                                                     item => item.Floor,
                                                     item => item.ItemNature,
                                                     item => item.ItemState,
                                                     item => item.ItemType);
                }
                else
                {
                    entities = await GetByExpression(item => info.Id.Contains(item.InventoryNumber)
                                                                 || info.Id.Contains(item.OldInventoryNumber)
                                                                 || info.Id.Contains(item.SerialNumber)
                                                                 || info.Id.Contains(item.YellowNumber.ToString())
                                                                 || info.Id.Contains(item.AccreditationNumber),
                                                     item => item.Department,
                                                     item => item.Employee,
                                                     item => item.Building,
                                                     item => item.Section,
                                                     item => item.Floor,
                                                     item => item.ItemNature,
                                                     item => item.ItemState,
                                                     item => item.ItemType);
                }

                if (entities == null)
                {
                    //TODO: logging
                    throw new Exception();
                }

                var displays = entities.Select(entity => entity.EntityToDto()).Select(dto => dto.TransformToDisplay()).ToList();
                return displays;
            }
            catch (Exception e)
            {
                //TODO: logging
                return null;
            }
        }
    }
}
