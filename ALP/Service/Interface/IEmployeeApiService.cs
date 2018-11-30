using Common.Model.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ALP.Service.Interface
{
    public interface IEmployeeApiService
    {
        Task<List<EmployeeDto>> GetAll();
    }
}
