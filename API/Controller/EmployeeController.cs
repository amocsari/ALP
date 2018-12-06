using System.Collections.Generic;
using System.Threading.Tasks;
using API.Service;
using Common.Model.Dto;
using Microsoft.AspNetCore.Mvc;
using Model.Enum;
using Model.Model;

namespace API.Controller
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IAccountService _accountService;

        public EmployeeController(IEmployeeService employeeService, IAccountService accountService)
        {
            _employeeService = employeeService;
            _accountService = accountService;
        }

        [HttpPost]
        public Task<AlpApiResponse> AddOrEditEmployee(EmployeeDto dto)
        {
            var sessionToken = HttpContext.Request.Headers["sessiontoken"];
            if (!_accountService.AuthorizeAsync(sessionToken, new List<RoleType> { RoleType.Admin }))
            {
                return Task.FromResult(new AlpApiResponse { Success = false, Message = "Nincs jogosultsága ehhez a művelethez!" });
            }
            return _employeeService.AddOrEditEmployee(dto);
        }

        [HttpGet]
        public Task<AlpApiResponse<List<EmployeeDto>>> GetAllEmployees()
        {
            var sessionToken = HttpContext.Request.Headers["sessiontoken"];
            if (!_accountService.AuthorizeAsync(sessionToken, new List<RoleType> { RoleType.Admin, RoleType.DepartmentInventoryOperator }))
            {
                return Task.FromResult(new AlpApiResponse<List<EmployeeDto>> { Success = false, Message = "Nincs jogosultsága ehhez a művelethez!" });
            }
            return _employeeService.GetAllEmployees();
        }

        [HttpGet]
        public Task<AlpApiResponse<List<EmployeeDto>>> GetAvailableEmployees()
        {
            var sessionToken = HttpContext.Request.Headers["sessiontoken"];
            if (!_accountService.AuthorizeAsync(sessionToken, new List<RoleType> { RoleType.Admin, RoleType.DepartmentInventoryOperator }))
            {
                return Task.FromResult(new AlpApiResponse<List<EmployeeDto>> { Success = false, Message = "Nincs jogosultsága ehhez a művelethez!" });
            }
            return _employeeService.GetAvailableEmployees();
        }

        [HttpPost]
        public Task<AlpApiResponse<List<EmployeeDto>>> FilterEmployees([FromBody] EmployeeFilterInfo info)
        {
            var sessionToken = HttpContext.Request.Headers["sessiontoken"];
            if (!_accountService.AuthorizeAsync(sessionToken, new List<RoleType> { RoleType.Admin }))
            {
                return Task.FromResult(new AlpApiResponse<List<EmployeeDto>> { Success = false, Message = "Nincs jogosultsága ehhez a művelethez!" });
            }
            return _employeeService.FilterEmployees(info);
        }

        [HttpPost]
        public Task<AlpApiResponse<List<EmployeeDto>>> GetEmployeeByName([FromBody]string name)
        {
            var sessionToken = HttpContext.Request.Headers["sessiontoken"];
            if (!_accountService.AuthorizeAsync(sessionToken, new List<RoleType> { RoleType.Admin }))
            {
                return Task.FromResult(new AlpApiResponse<List<EmployeeDto>> { Success = false, Message = "Nincs jogosultsága ehhez a művelethez!" });
            }
            return _employeeService.GetByName(name);
        }

        [HttpPost]
        public Task<AlpApiResponse> RetireEmployee([FromBody]int employeeId)
        {
            var sessionToken = HttpContext.Request.Headers["sessiontoken"];
            if (!_accountService.AuthorizeAsync(sessionToken, new List<RoleType> { RoleType.Admin }))
            {
                return Task.FromResult(new AlpApiResponse { Success = false, Message = "Nincs jogosultsága ehhez a művelethez!" });
            }
            return _employeeService.RetireEmployee(employeeId);
        }
    }
}