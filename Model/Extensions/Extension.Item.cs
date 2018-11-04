using System.Collections.Generic;
using Common.Model.Dto;
using Common.Model.Enum;
using Model.Model;

namespace Model.Extensions
{
    public static partial class Extension
    {
        public static ItemDisplay TransformToDisplay(this ItemDto dto)
        {
            var display = new ItemDisplay
            {
                ItemId = dto.ItemID,
                DisplayValues = new Dictionary<ItemPropertyType, string>(),
            };

            display.DisplayValues.Add(ItemPropertyType.ItemID, dto.ItemID.ToString());
            display.DisplayValues.Add(ItemPropertyType.InventoryNumber, dto.InventoryNumber);
            display.DisplayValues.Add(ItemPropertyType.OldInventoryNumber, dto.OldInventoryNumber);
            display.DisplayValues.Add(ItemPropertyType.SerialNumber, dto.SerialNumber);
            display.DisplayValues.Add(ItemPropertyType.AccreditationNumber, dto.AccreditationNumber);
            display.DisplayValues.Add(ItemPropertyType.YellowNumber, dto.YellowNumber.ToString());
            display.DisplayValues.Add(ItemPropertyType.ItemName, dto.ItemName);
            display.DisplayValues.Add(ItemPropertyType.ManufacturerModelType, $"{dto.Manufacturer} {dto.ModelType}");
            display.DisplayValues.Add(ItemPropertyType.ItemNature, dto.ItemNature.Name);
            display.DisplayValues.Add(ItemPropertyType.ItemType, dto.ItemType.Name);
            display.DisplayValues.Add(ItemPropertyType.ProductionYear, dto.ProductionYear?.Year.ToString());
            display.DisplayValues.Add(ItemPropertyType.Department, dto.Department.Name);
            display.DisplayValues.Add(ItemPropertyType.Section, dto.Section.Name);
            display.DisplayValues.Add(ItemPropertyType.Employee, dto.Employee.Name);
            display.DisplayValues.Add(ItemPropertyType.Building, dto.Building.Name);
            display.DisplayValues.Add(ItemPropertyType.Floor, dto.Floor.Name);
            display.DisplayValues.Add(ItemPropertyType.Room, dto.Room);
            display.DisplayValues.Add(ItemPropertyType.ItemState, dto.ItemState.Name);
            display.DisplayValues.Add(ItemPropertyType.DateOfCreation, dto.DateOfCreation?.ToShortDateString());
            display.DisplayValues.Add(ItemPropertyType.BruttoPrice, dto.BruttoPrice.ToString());
            display.DisplayValues.Add(ItemPropertyType.Comment, dto.Comment);
            display.DisplayValues.Add(ItemPropertyType.DateOfScrap, dto.DateOfScrap?.ToShortDateString());

            return display;
        }
    }
}
