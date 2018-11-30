﻿//using System.Collections.Generic;
//using System.Threading.Tasks;
//using DAL.Entity;
//using DAL.Service;
//using Microsoft.AspNetCore.Mvc;

//namespace API.Controller
//{
//    [Route("api/[controller]/[action]")]
//    [ApiController]
//    public class OperationController : ControllerBase
//    {
//        private readonly IOperationService _operationService;

//        public OperationController(IOperationService operationService)
//        {
//            _operationService = operationService;
//        }

//        [HttpGet]
//        public Task<List<Operation>> GetAllOperations()
//        {
//            return _operationService.GetAll();
//        }

//        [HttpGet]
//        public Task<Operation> GetOperationById(int operationId)
//        {
//            return _operationService.GetSingle(b => b.OperationId == operationId);
//        }

//        [HttpPost]
//        public void AddNewOperation([FromBody] Operation operation)
//        {
//            _operationService.InsertNew(operation);
//        }

//        [HttpDelete]
//        public void DeleteOperationById(int operationId)
//        {
//            _operationService.Remove(b => b.OperationId == operationId);
//        }
//    }
//}