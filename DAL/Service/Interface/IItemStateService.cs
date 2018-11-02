using DAL.Entity;
using Model.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Service
{
    public interface IItemStateService : IBaseService<ItemState>
    {
        Task<ItemStateDto> AddNewItemState(ItemStateDto itemState);
        Task<List<ItemStateDto>> GetAllItemStates();
        Task<ItemStateDto> GetItemStateById(int itemStateId);
        Task<ItemStateDto> UpdateItemState(ItemStateDto itemState);
        Task DeleteItemStateById(int itemStateId);
        Task ToggleItemStateLockStateById(int itemStateId);
    }
}
