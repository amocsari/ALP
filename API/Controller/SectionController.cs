using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Service;
using Microsoft.AspNetCore.Mvc;
using Common.Model.Dto;

namespace API.Controller
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SectionController : ControllerBase
    {
        private readonly ISectionService _sectionService;

        public SectionController(ISectionService sectionService)
        {
            _sectionService = sectionService;
        }

        [HttpGet]
        public Task<List<SectionDto>> GetAllSection()
        {
            return _sectionService.GetAllSections();
        }

        [HttpGet]
        public Task<List<SectionDto>> GetAvailableSection()
        {
            return _sectionService.GetAvailableSections();
        }

        [HttpGet]
        public Task<SectionDto> GetSectionById(int sectionId)
        {
            return _sectionService.GetSectionById(sectionId);
        }

        [HttpPost]
        public Task<SectionDto> AddNewSection([FromBody] SectionDto section)
        {
            return _sectionService.InsertNewSection(section);
        }

        [HttpPost]
        public void UpdateSection([FromBody] SectionDto section)
        {
            _sectionService.UpdateSection(section);
        }

        [HttpDelete]
        public void DeleteSectionById(int sectionId)
        {
            _sectionService.DeleteSectionById(sectionId);
        }

        [HttpPost]
        public void ToggleLockStateByIdSection([FromBody] int sectionId)
        {
            _sectionService.ToggleSectionLockStateById(sectionId);
        }
    }
}