using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Entity;
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
        public Task<List<Employee>> GetAllEmployees()
        {
            return _employeeService.GetAll();
        }

        [HttpGet]
        public Task<Employee> GetEmployeeById(int employeeId)
        {
            return _employeeService.GetSingle(b => b.EmployeeId == employeeId);
        }

        [HttpPost]
        public void AddNewEmployee([FromBody] Employee employee)
        {
            _employeeService.InsertNew(employee);
        }

        [HttpDelete]
        public void DeleteEmployeeById(int employeeId)
        {
            _employeeService.Remove(b => b.EmployeeId == employeeId);
        }
    }
}