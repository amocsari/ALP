using DAL.Entity;
using Common.Model.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Service
{
    public interface IItemNatureService : IBaseService<ItemNature>
    {
        Task<List<ItemNatureDto>> GetAllItemNatures();
        Task<List<ItemNatureDto>> GetAvailableItemNatures();
        Task<ItemNatureDto> AddNewItemNature(ItemNatureDto itemNature);
        Task<ItemNatureDto> GetItemNatureById(int itemNatureId);
        Task<ItemNatureDto> UpdateItemNature(ItemNatureDto itemNature);
        Task DeleteItemNatureById(int itemNatureId);
        Task ToggleItemNatureLockStateById(int itemNatureId);
    }
}
