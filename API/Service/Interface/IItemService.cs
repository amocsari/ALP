using System.Threading.Tasks;
using Common.Model.Dto;
using Common.Model;
using System.Collections.Generic;
using Model.Model;

namespace API.Service
{
    public interface IItemService
    {
        Task<AlpApiResponse<ItemDto>> GetItemById(int itemId);
        Task<AlpApiResponse> AddNewItem(ItemDto dto);
        Task<AlpApiResponse<List<ItemDto>>> FindItemsForDisplay(InventoryItemFilterInfo info);
    }
}
