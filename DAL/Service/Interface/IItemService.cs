using DAL.Entity;
using System.Threading.Tasks;
using Common.Model.Dto;
using Common.Model;
using Model.Model;
using System.Collections.Generic;

namespace DAL.Service
{
    public interface IItemService: IBaseService<Item>
    {
        Task<bool> AddNewItem(ItemDto dto);
        Task<List<ItemDisplay>> FindItemsForDisplay(InventoryItemFilterInfo info);
    }
}
