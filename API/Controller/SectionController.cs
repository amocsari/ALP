//using System.Collections.Generic;
//using System.Threading.Tasks;
//using DAL.Entity;
//using DAL.Service;
//using Microsoft.AspNetCore.Mvc;

//namespace API.Controller
//{
//    [Route("api/[controller]/[action]")]
//    [ApiController]
//    public class SectionController : ControllerBase
//    {
//        private readonly ISectionService _sectionService;

//        public SectionController(ISectionService sectionService)
//        {
//            _sectionService = sectionService;
//        }

//        [HttpGet]
//        public Task<List<Section>> GetAllSections()
//        {
//            return _sectionService.GetAll();
//        }

//        [HttpGet]
//        public Task<Section> GetSectionById(int sectionId)
//        {
//            return _sectionService.GetSingle(b => b.SectionId == sectionId);
//        }

//        [HttpPost]
//        public void AddNewSection([FromBody] Section section)
//        {
//            _sectionService.InsertNew(section);
//        }

//        [HttpDelete]
//        public void DeleteSectionById(int sectionId)
//        {
//            _sectionService.Remove(b => b.SectionId == sectionId);
//        }
//    }
//}