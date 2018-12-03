using System.Threading.Tasks;
using Common.Model.Dto;
using Common.Model;
using System.Collections.Generic;
using Model.Model;

namespace DAL.Service
{
    public interface IItemService
    {
        Task<AlpApiResponse> AddNewItem(ItemDto dto);
        Task<AlpApiResponse<List<ItemDto>>> FindItemsForDisplay(InventoryItemFilterInfo info);
    }
}
