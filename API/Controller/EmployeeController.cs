using System.Collections.Generic;
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
        public List<Employee> GetAllEmployees()
        {
            return _employeeService.GetAll();
        }

        [HttpGet]
        public Employee GetEmployeeById(int employeeId)
        {
            return _employeeService.GetSingle(b => b.EmployeeID == employeeId);
        }

        [HttpPost]
        public void AddNewEmployee([FromBody] Employee employee)
        {
            _employeeService.InsertNew(employee);
        }

        [HttpDelete]
        public void DeleteEmployeeById(int employeeId)
        {
            _employeeService.Remove(b => b.EmployeeID == employeeId);
        }
    }
}