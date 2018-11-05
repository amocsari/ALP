using Common.Model.Dto;
using DAL.Entity;

namespace DAL.Extensions
{
    public static partial class Extensions
    {
        public static Item DtoToEntity(this ItemDto dto)
        {
            return new Item
            {
                Building = dto.Building?.DtoToEntity(),
                Floor = dto.Floor?.DtoToEntity(),
                Department = dto.Department?.DtoToEntity(),
                Employee = dto.Employee?.DtoToEntity(),
                ItemNature = dto.ItemNature?.DtoToEntity(),
                Section = dto.Section?.DtoToEntity(),
                ItemType = dto.ItemType?.DtoToEntity(),
                ItemState = dto.ItemState?.DtoToEntity(),
                ItemName = dto.ItemName,
                BuildingID = dto.BuildingID,
                SectionID = dto.SectionID,
                EmployeeID = dto.EmployeeID,
                FloorID = dto.FloorID,
                ItemID = dto.ItemID,
                Manufacturer = dto.Manufacturer,
                DateOfScrap = dto.DateOfScrap,
                InventoryNumber = dto.InventoryNumber,
                ModelType = dto.ModelType,
                AccreditationNumber = dto.AccreditationNumber,
                BruttoPrice = dto.BruttoPrice,
                Comment = dto.Comment,
                DateOfCreation = dto.DateOfCreation,
                DepartementID = dto.DepartementID,
                ItemNatureID = dto.ItemNatureID,
                ItemStateID = dto.ItemStateID,
                ItemTypeID = dto.ItemTypeID,
                OldInventoryNumber = dto.OldInventoryNumber,
                ProductionYear = dto.ProductionYear,
                Room = dto.Room,
                SerialNumber = dto.SerialNumber,
                YellowNumber = dto.YellowNumber,
            };
        }

        public static ItemDto EntityToDto(this Item entity)
        {
            return new ItemDto
            {
                Building = entity.Building?.EntityToDto(),
                Floor = entity.Floor?.EntityToDto(),
                Department = entity.Department?.EntityToDto(),
                Employee = entity.Employee?.EntityToDto(),
                ItemNature = entity.ItemNature?.EntityToDto(),
                Section = entity.Section?.EntityToDto(),
                ItemType = entity.ItemType?.EntityToDto(),
                ItemState = entity.ItemState?.EntityToDto(),
                ItemName = entity.ItemName,
                BuildingID = entity.BuildingID,
                SectionID = entity.SectionID,
                EmployeeID = entity.EmployeeID,
                FloorID = entity.FloorID,
                ItemID = entity.ItemID,
                Manufacturer = entity.Manufacturer,
                DateOfScrap = entity.DateOfScrap,
                InventoryNumber = entity.InventoryNumber,
                ModelType = entity.ModelType,
                AccreditationNumber = entity.AccreditationNumber,
                BruttoPrice = entity.BruttoPrice,
                Comment = entity.Comment,
                DateOfCreation = entity.DateOfCreation,
                DepartementID = entity.DepartementID,
                ItemNatureID = entity.ItemNatureID,
                ItemStateID = entity.ItemStateID,
                ItemTypeID = entity.ItemTypeID,
                OldInventoryNumber = entity.OldInventoryNumber,
                ProductionYear = entity.ProductionYear,
                Room = entity.Room,
                SerialNumber = entity.SerialNumber,
                YellowNumber = entity.YellowNumber,
            };
        }
    }
}
