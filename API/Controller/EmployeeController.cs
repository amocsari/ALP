using System.Collections.Generic;
using System.Threading.Tasks;
using API.Service;
using Common.Model.Dto;
using Microsoft.AspNetCore.Mvc;
using Model.Model;

namespace API.Controller
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpPost]
        public Task<AlpApiResponse> AddNewEmployee(EmployeeDto dto)
        {
            return _employeeService.AddNewEmployee(dto);
        }

        [HttpGet]
        public Task<AlpApiResponse<List<EmployeeDto>>> GetAllEmployees()
        {
            return _employeeService.GetAllEmployees();
        }

        [HttpPost]
        public Task<AlpApiResponse<List<EmployeeDto>>> FilterEmployees([FromBody] EmployeeFilterInfo info)
        {
            return _employeeService.FilterEmployees(info);
        }

        [HttpPost]
        public Task<AlpApiResponse<List<EmployeeDto>>> GetEmployeeByName(string name)
        {
            return _employeeService.GetByName(name);
        }
    }
}