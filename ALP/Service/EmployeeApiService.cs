using System.Collections.Generic;
using System.Threading.Tasks;
using ALP.Service.Interface;
using Common.Model.Dto;
using Model.Model;

namespace ALP.Service
{
    public class EmployeeApiService : IEmployeeApiService
    {
        private readonly IAlpApiService _apiService;

        public EmployeeApiService(IAlpApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task AddNewEmployee(EmployeeDto dto)
        {
            await _apiService.PostAsync<EmployeeDto>("Employee/AddNewEmployee",dto);
        }

        public async Task<EmployeeDto> GetEmployeeByName(string employeeName)
        {
            return await _apiService.PostAsync<string, EmployeeDto>("Employee/GetEmployeeByName", employeeName);
        }

        public async Task<List<EmployeeDto>> FilterEmployees(EmployeeFilterInfo info)
        {
            return await _apiService.PostAsync<EmployeeFilterInfo, List<EmployeeDto>>("Employee/FilterEmployees", info);
        }

        public async Task<List<EmployeeDto>> GetAll()
        {
            return await _apiService.GetAsync<List<EmployeeDto>>("Employee/GetAllEmployees");
        }
    }
}
