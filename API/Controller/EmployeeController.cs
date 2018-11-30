using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Model.Dto;
using DAL.Service;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet]
        public Task<List<EmployeeDto>> GetAllEmployees()
        {
            return _employeeService.GetAllEmployees();
        }
    }
}