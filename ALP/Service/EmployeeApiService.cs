using System.Collections.Generic;
using System.Threading.Tasks;
using ALP.Service.Interface;
using Common.Model.Dto;

namespace ALP.Service
{
    public class EmployeeApiService : IEmployeeApiService
    {
        private readonly IAlpApiService _apiService;

        public EmployeeApiService(IAlpApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<List<EmployeeDto>> GetAll()
        {
            return await _apiService.GetAsync<List<EmployeeDto>>("Employee/GetAllEmployee");
        }
    }
}
