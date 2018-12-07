using System.Collections.Generic;
using System.Threading.Tasks;
using ALP.Service.Interface;
using Common.Model.Dto;
using Model.Model;

namespace ALP.Service
{
    /// <summary>
    /// Handles api calls towards the server
    /// </summary>
    public class EmployeeApiService : IEmployeeApiService
    {
        /// <summary>
        /// Injected services
        /// </summary>
        private readonly IAlpApiService _apiService;
        private readonly IAlpLoggingService<EmployeeApiService> _loggingService;

        public EmployeeApiService(IAlpApiService apiService, IAlpLoggingService<EmployeeApiService> loggingService)
        {
            _apiService = apiService;
            _loggingService = loggingService;
        }

        /// <summary>
        /// sends employee data towards the server
        /// if it is already in the database, it updates
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task AddOrEditEmployee(EmployeeDto dto)
        {
            _loggingService.LogDebug(new
            {
                action = nameof(AddOrEditEmployee),
                dto = dto.ToString()
            });

            await _apiService.PostAsync("Employee/AddOrEditEmployee",dto);
        }

        /// <summary>
        /// returns an employee by its name
        /// </summary>
        /// <param name="employeeName"></param>
        /// <returns></returns>
        public async Task<EmployeeDto> GetEmployeeByName(string employeeName)
        {
            _loggingService.LogDebug(new
            {
                action = nameof(GetEmployeeByName),
                employeeName
            });

            return await _apiService.PostAsync<string, EmployeeDto>("Employee/GetEmployeeByName", employeeName);
        }

        /// <summary>
        /// Requests a list of employees that fit the filter criteria
        /// </summary>
        /// <param name="filterInfo"></param>
        /// <returns></returns>
        public async Task<List<EmployeeDto>> FilterEmployees(EmployeeFilterInfo filterInfo)
        {
            _loggingService.LogDebug(new
            {
                action = nameof(FilterEmployees),
                filterInfo = filterInfo.ToString()
            });

            return await _apiService.PostAsync<EmployeeFilterInfo, List<EmployeeDto>>("Employee/FilterEmployees", filterInfo);
        }

        /// <summary>
        /// Gets all employees.
        /// </summary>
        /// <returns></returns>
        public async Task<List<EmployeeDto>> GetAll()
        {
            _loggingService.LogDebug(new
            {
                action = nameof(GetAll)
            });

            return await _apiService.GetAsync<List<EmployeeDto>>("Employee/GetAllEmployees");
        }

        /// <summary>
        /// Gets employees that did not retire yet
        /// Retirement date = null
        /// </summary>
        /// <returns></returns>
        public async Task<List<EmployeeDto>> GetAvailable()
        {
            _loggingService.LogDebug(new
            {
                action = nameof(GetAvailable)
            });

            return await _apiService.GetAsync<List<EmployeeDto>>("Employee/GetAvailableEmployees");
        }

        /// <summary>
        /// Tells the server to set an employees 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task RetireEmployeeById(int id)
        {
            _loggingService.LogDebug(new
            {
                action = nameof(RetireEmployeeById),
                id
            });

            await _apiService.PostAsync("Employee/RetireEmployee", id);
        }
    }
}
