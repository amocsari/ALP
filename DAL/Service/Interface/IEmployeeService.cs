using Common.Model.Dto;
using Model.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Service
{
    public interface IEmployeeService
    {
        Task<List<EmployeeDto>> GetAllEmployees();
        Task<List<EmployeeDto>> FilterEmployees(EmployeeFilterInfo info);
        Task AddNewEmployee(EmployeeDto dto);
    }
}
