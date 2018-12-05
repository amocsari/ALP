using Common.Model.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;
using Model.Model;

namespace ALP.Service.Interface
{
    public interface IEmployeeApiService
    {
        Task<List<EmployeeDto>> GetAll();
        Task<List<EmployeeDto>> FilterEmployees(EmployeeFilterInfo info);
        Task AddNewEmployee(EmployeeDto dto);
        Task<EmployeeDto> GetEmployeeByName(string employeeName);
        Task<List<EmployeeDto>> GetAvailable();
    }
}
