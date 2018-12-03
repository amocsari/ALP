using Common.Model.Dto;
using Model.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Service
{
    public interface IEmployeeService
    {
        Task<AlpApiResponse<List<EmployeeDto>>> GetAllEmployees();
        Task<AlpApiResponse<List<EmployeeDto>>> FilterEmployees(EmployeeFilterInfo info);
        Task<AlpApiResponse> AddNewEmployee(EmployeeDto dto);
        Task<AlpApiResponse<List<EmployeeDto>>> GetByName(string name);
    }
}
