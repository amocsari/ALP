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
    public class OperationController : ControllerBase
    {
        private readonly IOperationService _operationService;
        private readonly IAccountService _accountService;

        public OperationController(IOperationService operationService, IAccountService accountService)
        {
            _operationService = operationService;
            _accountService = accountService;
        }

        [HttpPost]
        public Task<AlpApiResponse<List<ItemDto>>> QueueOperations([FromBody] List<OperationDto> operationList)
        {
            var sessionToken = HttpContext.Request.Headers["sessiontoken"];
            if (!_accountService.Authorize(sessionToken, new List<RoleType> { RoleType.Admin, RoleType.DepartmentInventoryOperator }))
            {
                return Task.FromResult(new AlpApiResponse<List<ItemDto>> { Success = false, Message = "Nincs jogosultsága ehhez a művelethez!" });
            }
            return _operationService.QueueOperations(operationList, sessionToken);
        }

        [HttpPost]
        public Task<AlpApiResponse> DoOperation([FromBody] int operationId)
        {
            var sessionToken = HttpContext.Request.Headers["sessiontoken"];
            if (!_accountService.Authorize(sessionToken, new List<RoleType> { RoleType.Admin }))
            {
                return Task.FromResult(new AlpApiResponse { Success = false, Message = "Nincs jogosultsága ehhez a művelethez!" });
            }
            return _operationService.DoOperation(operationId);
        }
    }
}