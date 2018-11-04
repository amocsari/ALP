using Common.Model.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ALP.Service
{
    public interface ILookupApiService<T> where T : LookupDtoBase
    {
        Task<List<T>> GetAll();
        Task<List<T>> GetAvailable();
        Task<T> AddNew(T dto);
        Task<T> Update(T dto);
        Task ToggleLockStateById(int dtoId);
    }
}
