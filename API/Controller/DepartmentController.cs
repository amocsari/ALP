using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Service;
using Microsoft.AspNetCore.Mvc;
using Common.Model.Dto;

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
        public Task<List<DepartmentDto>> GetAllDepartment()
        {
            return _departmentService.GetAllDepartments();
        }

        [HttpGet]
        public Task<List<DepartmentDto>> GetAvailableDepartment()
        {
            return _departmentService.GetAvailableDepartments();
        }

        [HttpGet]
        public Task<DepartmentDto> GetDepartmentById(int departmentId)
        {
            return _departmentService.GetDepartmentById(departmentId);
        }

        [HttpPost]
        public Task<DepartmentDto> AddNewDepartment([FromBody] DepartmentDto department)
        {
            return _departmentService.InsertNewDepartment(department);
        }

        [HttpPost]
        public void UpdateDepartment([FromBody] DepartmentDto department)
        {
            _departmentService.UpdateDepartment(department);
        }

        [HttpDelete]
        public void DeleteDepartmentById(int departmentId)
        {
            _departmentService.DeleteDepartmentById(departmentId);
        }

        [HttpPost]
        public void ToggleLockStateByIdDepartment([FromBody] int departmentId)
        {
            _departmentService.ToggleDepartmentLockStateById(departmentId);
        }
    }
}