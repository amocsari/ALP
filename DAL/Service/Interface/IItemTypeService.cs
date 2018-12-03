using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Model.Dto;
using Model.Model;

namespace DAL.Service
{
    public interface IItemTypeService
    {
        Task<AlpApiResponse<List<ItemTypeDto>>> GetAllItemTypes();
        Task<AlpApiResponse<List<ItemTypeDto>>> GetAvailableItemTypes();
        Task<AlpApiResponse<ItemTypeDto>> GetItemTypeById(int itemTypeId);
        Task<AlpApiResponse<ItemTypeDto>> InsertNewItemType(ItemTypeDto itemType);
        Task<AlpApiResponse> DeleteItemTypeById(int itemTypeId);
        Task<AlpApiResponse> ToggleItemTypeLockStateById(int itemTypeId);
        Task<AlpApiResponse> UpdateItemType(ItemTypeDto dto);
    }
}
