//using System.Collections.Generic;
//using System.Threading.Tasks;
//using DAL.Entity;
//using DAL.Service;
//using Microsoft.AspNetCore.Mvc;

//namespace API.Controller
//{
//    [Route("api/[controller]/[action]")]
//    [ApiController]
//    public class DepartmentController : ControllerBase
//    {
//        private readonly IDepartmentService _departmentService;

//        public DepartmentController(IDepartmentService departmentService)
//        {
//            _departmentService = departmentService;
//        }

//        [HttpGet]
//        public Task<List<Department>> GetAllDepartments()
//        {
//            return _departmentService.GetAll();
//        }

//        [HttpGet]
//        public Task<Department> GetDepartmentById(int departmentId)
//        {
//            return _departmentService.GetSingle(b => b.DepartmentId == departmentId);
//        }

//        [HttpPost]
//        public void AddNewDepartment([FromBody] Department department)
//        {
//            _departmentService.InsertNew(department);
//        }

//        [HttpDelete]
//        public void DeleteDepartmentById(int departmentId)
//        {
//            _departmentService.Remove(b => b.DepartmentId == departmentId);
//        }
//    }
//}