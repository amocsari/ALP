using System.Collections.Generic;
using System.Threading.Tasks;
using API.Service;
using DAL.Entity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controller
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OperationTypeController : ControllerBase
    {
        private readonly IOperationTypeService _operationTypeService;

        public OperationTypeController(IOperationTypeService operationTypeService)
        {
            _operationTypeService = operationTypeService;
        }
    }
}