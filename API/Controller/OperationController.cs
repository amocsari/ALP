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
    public class OperationController : ControllerBase
    {
        private readonly IOperationService _operationService;

        public OperationController(IOperationService operationService)
        {
            _operationService = operationService;
        }

        [HttpPost]
        public Task<AlpApiResponse> QueueOperations([FromBody] List<OperationDto> operationList)
        {
            return _operationService.QueueOperations(operationList);
        }
    }
}