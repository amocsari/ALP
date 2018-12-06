using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Common.Model.Dto;
using Model.Model;
using API.Service;
using Model.Enum;

namespace API.Controller
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;
        private readonly IAccountService _accountService;

        public DepartmentController(IDepartmentService departmentService, IAccountService accountService)
        {
            _departmentService = departmentService;
            _accountService = accountService;
        }

        [HttpGet]
        public Task<AlpApiResponse<List<DepartmentDto>>> GetAllDepartment()
        {
            var sessionToken = HttpContext.Request.Headers["sessiontoken"];
            if (!_accountService.AuthorizeAsync(sessionToken, new List<RoleType> { RoleType.Admin, RoleType.DepartmentInventoryOperator }))
            {
                return Task.FromResult(new AlpApiResponse<List<DepartmentDto>> { Success = false, Message = "Nincs jogosultsága ehhez a művelethez!" });
            }
            return _departmentService.GetAllDepartments();
        }

        [HttpGet]
        public Task<AlpApiResponse<List<DepartmentDto>>> GetAvailableDepartment()
        {
            var sessionToken = HttpContext.Request.Headers["sessiontoken"];
            if (!_accountService.AuthorizeAsync(sessionToken, new List<RoleType> { RoleType.Admin, RoleType.DepartmentInventoryOperator }))
            {
                return Task.FromResult(new AlpApiResponse<List<DepartmentDto>> { Success = false, Message = "Nincs jogosultsága ehhez a művelethez!" });
            }
            return _departmentService.GetAvailableDepartments();
        }

        //[HttpGet]
        //public Task<AlpApiResponse<DepartmentDto>> GetDepartmentById(int departmentId)
        //{
        //    var sessionToken = HttpContext.Request.Headers["sessiontoken"];
        //    if (!_accountService.AuthorizeAsync(sessionToken, new List<RoleType> { RoleType.Admin }))
        //    {
        //        return Task.FromResult(new AlpApiResponse<DepartmentDto> { Success = false, Message = "Nincs jogosultsága ehhez a művelethez!" });
        //    }
        //    return _departmentService.GetDepartmentById(departmentId);
        //}

        [HttpPost]
        public Task<AlpApiResponse<DepartmentDto>> AddNewDepartment([FromBody] DepartmentDto department)
        {
            var sessionToken = HttpContext.Request.Headers["sessiontoken"];
            if (!_accountService.AuthorizeAsync(sessionToken, new List<RoleType> { RoleType.Admin }))
            {
                return Task.FromResult(new AlpApiResponse<DepartmentDto> { Success = false, Message = "Nincs jogosultsága ehhez a művelethez!" });
            }
            return _departmentService.InsertNewDepartment(department);
        }

        [HttpPost]
        public Task<AlpApiResponse> UpdateDepartment([FromBody] DepartmentDto department)
        {
            var sessionToken = HttpContext.Request.Headers["sessiontoken"];
            if (!_accountService.AuthorizeAsync(sessionToken, new List<RoleType> { RoleType.Admin }))
            {
                return Task.FromResult(new AlpApiResponse { Success = false, Message = "Nincs jogosultsága ehhez a művelethez!" });
            }
            return _departmentService.UpdateDepartment(department);
        }

        //[HttpDelete]
        //public Task<AlpApiResponse> DeleteDepartmentById(int departmentId)
        //{
        //    var sessionToken = HttpContext.Request.Headers["sessiontoken"];
        //    if (!_accountService.AuthorizeAsync(sessionToken, new List<RoleType> { RoleType.Admin }))
        //    {
        //        return Task.FromResult(new AlpApiResponse { Success = false, Message = "Nincs jogosultsága ehhez a művelethez!" });
        //    }
        //    return _departmentService.DeleteDepartmentById(departmentId);
        //}

        [HttpPost]
        public Task<AlpApiResponse> ToggleLockStateByIdDepartment([FromBody] int departmentId)
        {
            var sessionToken = HttpContext.Request.Headers["sessiontoken"];
            if (!_accountService.AuthorizeAsync(sessionToken, new List<RoleType> { RoleType.Admin }))
            {
                return Task.FromResult(new AlpApiResponse { Success = false, Message = "Nincs jogosultsága ehhez a művelethez!" });
            }
            return _departmentService.ToggleDepartmentLockStateById(departmentId);
        }
    }
}