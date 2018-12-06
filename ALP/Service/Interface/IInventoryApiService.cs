using Common.Model.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Model;

namespace ALP.Service.Interface
{
    public interface IInventoryApiService
    {
        Task<bool> AddNewInventoryItem(ItemDto dto);
        Task<List<ItemDto>> FilterItems(InventoryItemFilterInfo info);
        Task<ItemDto> GetItemById(int itemId);
        Task<List<ItemDto>> ImportItems(List<ImportedItem> importedItems);
        Task<List<ItemDto>> GetItemsByEmployeeId(int employeeId);
    }
}
