using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Entity;
using DAL.Service;
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

        [HttpGet]
        public Task<List<OperationType>> GetAllOperationTypes()
        {
            return _operationTypeService.GetAll();
        }

        [HttpGet]
        public Task<OperationType> GetOperationTypeById(int operationTypeId)
        {
            return _operationTypeService.GetSingle(b => b.OperationTypeID == operationTypeId);
        }

        [HttpPost]
        public void AddNewOperationType([FromBody] OperationType operationType)
        {
            _operationTypeService.InsertNew(operationType);
        }

        [HttpDelete]
        public void DeleteOperationTypeById(int operationTypeId)
        {
            _operationTypeService.Remove(b => b.OperationTypeID == operationTypeId);
        }
    }
}