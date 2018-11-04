using Common.Model.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Model;

namespace ALP.Service.Interface
{
    public interface IInventoryApiService
    {
        Task<bool> AddNewInventoryItem(ItemDto dto);
        Task<List<string>> FindFilteredItemsAsStringForDisplay(InventoryItemFilterInfo info);
        Task<ItemDto> GetItemById(int itemId);
    }
}
