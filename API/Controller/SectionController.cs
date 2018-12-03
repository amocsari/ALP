using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Service;
using Microsoft.AspNetCore.Mvc;
using Common.Model.Dto;
using Model.Model;

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
        public Task<AlpApiResponse<List<SectionDto>>> GetAllSection()
        {
            return _sectionService.GetAllSections();
        }

        [HttpGet]
        public Task<AlpApiResponse<List<SectionDto>>> GetAvailableSection()
        {
            return _sectionService.GetAvailableSections();
        }

        [HttpGet]
        public Task<AlpApiResponse<SectionDto>> GetSectionById(int sectionId)
        {
            return _sectionService.GetSectionById(sectionId);
        }

        [HttpPost]
        public Task<AlpApiResponse<SectionDto>> AddNewSection([FromBody] SectionDto section)
        {
            return _sectionService.InsertNewSection(section);
        }

        [HttpPost]
        public Task<AlpApiResponse> UpdateSection([FromBody] SectionDto section)
        {
            return _sectionService.UpdateSection(section);
        }

        [HttpDelete]
        public Task<AlpApiResponse> DeleteSectionById(int sectionId)
        {
            return _sectionService.DeleteSectionById(sectionId);
        }

        [HttpPost]
        public Task<AlpApiResponse> ToggleLockStateByIdSection([FromBody] int sectionId)
        {
            return _sectionService.ToggleSectionLockStateById(sectionId);
        }
    }
}