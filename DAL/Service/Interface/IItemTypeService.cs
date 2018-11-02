using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Entity;
using Model.Dto;

namespace DAL.Service
{
    public interface IItemTypeService : IBaseService<ItemType>
    {
        Task<List<ItemTypeDto>> GetAllItemTypes();
        Task<ItemTypeDto> GetItemTypeById(int itemTypeId);
        Task<ItemTypeDto> InsertNewItemType(ItemTypeDto itemType);
        Task DeleteItemTypeById(int itemTypeId);
        Task ToggleItemTypeLockStateById(int itemTypeId);
        Task<ItemTypeDto> UpdateItemType(ItemTypeDto dto);
    }
}
