using Model.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ALP.Service.Interface
{
    public interface IOperationService
    {
        Task Scrap(List<int> itemIds, bool priority);
        Task ChangeDepartment(List<int> itemIds, int departmentId, bool priority);
        Task ChangeOwner(List<int> itemIds, int ownerId, bool priority);
        Task ChangeOwnerToDepartmentChief(List<int> itemIds, bool priority);
    }
}
