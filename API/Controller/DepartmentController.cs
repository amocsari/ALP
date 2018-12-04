using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Common.Model.Dto;
using Model.Model;
using API.Service;

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
        public Task<AlpApiResponse<List<DepartmentDto>>> GetAllDepartment()
        {
            return _departmentService.GetAllDepartments();
        }

        [HttpGet]
        public Task<AlpApiResponse<List<DepartmentDto>>> GetAvailableDepartment()
        {
            return _departmentService.GetAvailableDepartments();
        }

        [HttpGet]
        public Task<AlpApiResponse<DepartmentDto>> GetDepartmentById(int departmentId)
        {
            return _departmentService.GetDepartmentById(departmentId);
        }

        [HttpPost]
        public Task<AlpApiResponse<DepartmentDto>> AddNewDepartment([FromBody] DepartmentDto department)
        {
            return _departmentService.InsertNewDepartment(department);
        }

        [HttpPost]
        public Task<AlpApiResponse> UpdateDepartment([FromBody] DepartmentDto department)
        {
            return _departmentService.UpdateDepartment(department);
        }

        [HttpDelete]
        public Task<AlpApiResponse> DeleteDepartmentById(int departmentId)
        {
            return _departmentService.DeleteDepartmentById(departmentId);
        }

        [HttpPost]
        public Task<AlpApiResponse> ToggleLockStateByIdDepartment([FromBody] int departmentId)
        {
            return _departmentService.ToggleDepartmentLockStateById(departmentId);
        }
    }
}