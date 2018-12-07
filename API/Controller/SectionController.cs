using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Common.Model.Dto;
using Model.Model;
using API.Service;
using Model.Enum;

namespace API.Controller
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SectionController : ControllerBase
    {
        private readonly ISectionService _sectionService;
        private readonly IAccountService _accountService;

        public SectionController(ISectionService sectionService, IAccountService accountService)
        {
            _sectionService = sectionService;
            _accountService = accountService;
        }

        [HttpGet]
        public Task<AlpApiResponse<List<SectionDto>>> GetAllSection()
        {
            var sessionToken = HttpContext.Request.Headers["sessiontoken"];
            if (!_accountService.Authorize(sessionToken, new List<RoleType>{ RoleType.Admin, RoleType.DepartmentInventoryOperator }))
            {
                return Task.FromResult(new AlpApiResponse<List<SectionDto>> { Success = false, Message = "Nincs jogosultsága ehhez a művelethez!" });
            }
            return _sectionService.GetAllSections();
        }

        [HttpGet]
        public Task<AlpApiResponse<List<SectionDto>>> GetAvailableSection()
        {
            var sessionToken = HttpContext.Request.Headers["sessiontoken"];
            if (!_accountService.Authorize(sessionToken, new List<RoleType> { RoleType.Admin, RoleType.DepartmentInventoryOperator }))
            {
                return Task.FromResult(new AlpApiResponse<List<SectionDto>> { Success = false, Message = "Nincs jogosultsága ehhez a művelethez!" });
            }
            return _sectionService.GetAvailableSections();
        }

        //[HttpGet]
        //public Task<AlpApiResponse<SectionDto>> GetSectionById(int sectionId)
        //{
        //    var sessionToken = HttpContext.Request.Headers["sessiontoken"];
        //    if (!_accountService.AuthorizeAsync(sessionToken, new List<RoleType> { RoleType.Admin }))
        //    {
        //        return Task.FromResult(new AlpApiResponse<List<SectionDto>> { Success = false, Message = "Nincs jogosultsága ehhez a művelethez!" });
        //    }
        //    return _sectionService.GetSectionById(sectionId);
        //}

        [HttpPost]
        public Task<AlpApiResponse<SectionDto>> AddNewSection([FromBody] SectionDto section)
        {
            var sessionToken = HttpContext.Request.Headers["sessiontoken"];
            if (!_accountService.Authorize(sessionToken, new List<RoleType> { RoleType.Admin }))
            {
                return Task.FromResult(new AlpApiResponse<SectionDto> { Success = false, Message = "Nincs jogosultsága ehhez a művelethez!" });
            }
            return _sectionService.InsertNewSection(section);
        }

        [HttpPost]
        public Task<AlpApiResponse> UpdateSection([FromBody] SectionDto section)
        {
            var sessionToken = HttpContext.Request.Headers["sessiontoken"];
            if (!_accountService.Authorize(sessionToken, new List<RoleType> { RoleType.Admin }))
            {
                return Task.FromResult(new AlpApiResponse { Success = false, Message = "Nincs jogosultsága ehhez a művelethez!" });
            }
            return _sectionService.UpdateSection(section);
        }

        //[HttpDelete]
        //public Task<AlpApiResponse> DeleteSectionById(int sectionId)
        //{
        //    var sessionToken = HttpContext.Request.Headers["sessiontoken"];
        //    if (!_accountService.AuthorizeAsync(sessionToken, new List<RoleType> { RoleType.Admin }))
        //    {
        //        return Task.FromResult(new AlpApiResponse<List<SectionDto>> { Success = false, Message = "Nincs jogosultsága ehhez a művelethez!" });
        //    }
        //    return _sectionService.DeleteSectionById(sectionId);
        //}

        [HttpPost]
        public Task<AlpApiResponse> ToggleLockStateByIdSection([FromBody] int sectionId)
        {
            var sessionToken = HttpContext.Request.Headers["sessiontoken"];
            if (!_accountService.Authorize(sessionToken, new List<RoleType> { RoleType.Admin }))
            {
                return Task.FromResult(new AlpApiResponse { Success = false, Message = "Nincs jogosultsága ehhez a művelethez!" });
            }
            return _sectionService.ToggleSectionLockStateById(sectionId);
        }
    }
}