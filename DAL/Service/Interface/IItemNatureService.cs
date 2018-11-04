using DAL.Entity;
using Common.Model.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Service
{
    public interface IItemNatureService : IBaseService<ItemNature>
    {
        Task<ItemNatureDto> AddNewItemNature(ItemNatureDto itemNature);
        Task<List<ItemNatureDto>> GetAllItemNatures();
        Task<ItemNatureDto> GetItemNatureById(int itemNatureId);
        Task<ItemNatureDto> UpdateItemNature(ItemNatureDto itemNature);
        Task DeleteItemNatureById(int itemNatureId);
        Task ToggleItemNatureLockStateById(int itemNatureId);
    }
}
