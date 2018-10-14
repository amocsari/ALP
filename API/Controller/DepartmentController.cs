using System.Collections.Generic;
using DAL.Entity;
using DAL.Service;
using Microsoft.AspNetCore.Mvc;

namespace API.Controller
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet]
        public List<Department> GetAllDepartments()
        {
            return _departmentService.GetAll();
        }

        [HttpGet]
        public Department GetDepartmentById(int departmentId)
        {
            return _departmentService.GetSingle(b => b.DepartmentID == departmentId);
        }

        [HttpPost]
        public void AddNewDepartment([FromBody] Department department)
        {
            _departmentService.InsertNew(department);
        }

        [HttpDelete]
        public void DeleteDepartmentById(int departmentId)
        {
            _departmentService.Remove(b => b.DepartmentID == departmentId);
        }
    }
}