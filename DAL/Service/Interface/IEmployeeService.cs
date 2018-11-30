using Common.Model.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Service
{
    public interface IEmployeeService
    {
        Task<List<EmployeeDto>> GetAllEmployees();
    }
}
