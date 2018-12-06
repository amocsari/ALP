using Common.Model.Dto;
using Model.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ALP.Service.Interface
{
    public interface IOperationService
    {
        Task<List<ItemDto>> Scrap(List<int> itemIds, bool priority);
        Task<List<ItemDto>> ChangeDepartment(List<int> itemIds, int departmentId, bool priority);
        Task<List<ItemDto>> ChangeOwner(List<int> itemIds, int ownerId, bool priority);
        Task<List<ItemDto>> ChangeOwnerToDepartmentChief(List<int> itemIds, bool priority);
    }
}
