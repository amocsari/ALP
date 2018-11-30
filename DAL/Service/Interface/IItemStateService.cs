using DAL.Entity;
using Common.Model.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Service
{
    public interface IItemStateService
    {
        Task<List<ItemStateDto>> GetAllItemStates();
        Task<List<ItemStateDto>> GetAvailableItemStates();
        Task<ItemStateDto> AddNewItemState(ItemStateDto itemState);
        Task<ItemStateDto> GetItemStateById(int itemStateId);
        Task<ItemStateDto> UpdateItemState(ItemStateDto itemState);
        Task DeleteItemStateById(int itemStateId);
        Task ToggleItemStateLockStateById(int itemStateId);
    }
}
