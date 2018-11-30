using System.Threading.Tasks;
using Common.Model.Dto;
using Common.Model;
using System.Collections.Generic;

namespace DAL.Service
{
    public interface IItemService
    {
        Task<bool> AddNewItem(ItemDto dto);
        Task<List<ItemDto>> FindItemsForDisplay(InventoryItemFilterInfo info);
    }
}
