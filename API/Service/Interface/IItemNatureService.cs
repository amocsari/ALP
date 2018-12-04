using Common.Model.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;
using Model.Model;

namespace API.Service
{
    public interface IItemNatureService
    {
        Task<AlpApiResponse<List<ItemNatureDto>>> GetAllItemNatures();
        Task<AlpApiResponse<List<ItemNatureDto>>> GetAvailableItemNatures();
        Task<AlpApiResponse<ItemNatureDto>> AddNewItemNature(ItemNatureDto itemNature);
        Task<AlpApiResponse<ItemNatureDto>> GetItemNatureById(int itemNatureId);
        Task<AlpApiResponse> UpdateItemNature(ItemNatureDto itemNature);
        Task<AlpApiResponse> DeleteItemNatureById(int itemNatureId);
        Task<AlpApiResponse> ToggleItemNatureLockStateById(int itemNatureId);
    }
}
