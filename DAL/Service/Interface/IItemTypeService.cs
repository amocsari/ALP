using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Entity;
using Common.Model.Dto;

namespace DAL.Service
{
    public interface IItemTypeService
    {
        Task<List<ItemTypeDto>> GetAllItemTypes();
        Task<List<ItemTypeDto>> GetAvailableItemTypes();
        Task<ItemTypeDto> GetItemTypeById(int itemTypeId);
        Task<ItemTypeDto> InsertNewItemType(ItemTypeDto itemType);
        Task DeleteItemTypeById(int itemTypeId);
        Task ToggleItemTypeLockStateById(int itemTypeId);
        Task<ItemTypeDto> UpdateItemType(ItemTypeDto dto);
    }
}
