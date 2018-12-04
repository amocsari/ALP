using Common.Model.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;
using Model.Model;

namespace API.Service
{
    public interface IItemStateService
    {
        Task<AlpApiResponse<List<ItemStateDto>>> GetAllItemStates();
        Task<AlpApiResponse<List<ItemStateDto>>> GetAvailableItemStates();
        Task<AlpApiResponse<ItemStateDto>> AddNewItemState(ItemStateDto itemState);
        Task<AlpApiResponse<ItemStateDto>> GetItemStateById(int itemStateId);
        Task<AlpApiResponse> UpdateItemState(ItemStateDto itemState);
        Task<AlpApiResponse> DeleteItemStateById(int itemStateId);
        Task<AlpApiResponse> ToggleItemStateLockStateById(int itemStateId);
    }
}
