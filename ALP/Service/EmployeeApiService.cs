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
        private readonly IAlpLoggingService<EmployeeApiService> _loggingService;

        public EmployeeApiService(IAlpApiService apiService, IAlpLoggingService<EmployeeApiService> loggingService)
        {
            _apiService = apiService;
            _loggingService = loggingService;
        }

        public async Task AddOrEditEmployee(EmployeeDto dto)
        {
            _loggingService.LogDebug(new
            {
                action = nameof(AddOrEditEmployee),
                dto = dto.ToString()
            });

            await _apiService.PostAsync("Employee/AddOrEditEmployee",dto);
        }

        public async Task<EmployeeDto> GetEmployeeByName(string employeeName)
        {
            _loggingService.LogDebug(new
            {
                action = nameof(GetEmployeeByName),
                employeeName
            });

            return await _apiService.PostAsync<string, EmployeeDto>("Employee/GetEmployeeByName", employeeName);
        }

        public async Task<List<EmployeeDto>> FilterEmployees(EmployeeFilterInfo filterInfo)
        {
            _loggingService.LogDebug(new
            {
                action = nameof(FilterEmployees),
                filterInfo = filterInfo.ToString()
            });

            return await _apiService.PostAsync<EmployeeFilterInfo, List<EmployeeDto>>("Employee/FilterEmployees", filterInfo);
        }

        public async Task<List<EmployeeDto>> GetAll()
        {
            _loggingService.LogDebug(new
            {
                action = nameof(GetAll)
            });

            return await _apiService.GetAsync<List<EmployeeDto>>("Employee/GetAllEmployees");
        }

        public async Task<List<EmployeeDto>> GetAvailable()
        {
            _loggingService.LogDebug(new
            {
                action = nameof(GetAvailable)
            });

            return await _apiService.GetAsync<List<EmployeeDto>>("Employee/GetAvailableEmployees");
        }
    }
}
