using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Common.Model.Dto;
using Model.Model;
using System.Threading.Tasks;
using API.Service;
using Model.Enum;

namespace API.Controller
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ItemNatureController : ControllerBase
    {
        private readonly IItemNatureService _itemNatureService;
        private readonly IAccountService _accountService;

        public ItemNatureController(IItemNatureService itemNatureService, IAccountService accountService)
        {
            _itemNatureService = itemNatureService;
            _accountService = accountService;
        }

        [HttpGet]
        public Task<AlpApiResponse<List<ItemNatureDto>>> GetAllItemNature()
        {
            var sessionToken = HttpContext.Request.Headers["sessiontoken"];
            if (!_accountService.AuthorizeAsync(sessionToken, new List<RoleType> { RoleType.Admin, RoleType.DepartmentInventoryOperator }))
            {
                return Task.FromResult(new AlpApiResponse<List<ItemNatureDto>> { Success = false, Message = "Nincs jogosultsága ehhez a művelethez!" });
            }
            return _itemNatureService.GetAllItemNatures();
        }

        [HttpGet]
        public Task<AlpApiResponse<List<ItemNatureDto>>> GetAvailableItemNature()
        {
            var sessionToken = HttpContext.Request.Headers["sessiontoken"];
            if (!_accountService.AuthorizeAsync(sessionToken, new List<RoleType> { RoleType.Admin, RoleType.DepartmentInventoryOperator }))
            {
                return Task.FromResult(new AlpApiResponse<List<ItemNatureDto>> { Success = false, Message = "Nincs jogosultsága ehhez a művelethez!" });
            }
            return _itemNatureService.GetAvailableItemNatures();
        }

        //[HttpGet]
        //public Task<AlpApiResponse<ItemNatureDto>> GetItemNatureById(int itemNatureId)
        //{
        //    var sessionToken = HttpContext.Request.Headers["sessiontoken"];
        //    if (!_accountService.AuthorizeAsync(sessionToken, new List<RoleType> { RoleType.Admin }))
        //    {
        //        return Task.FromResult(new AlpApiResponse<ItemNatureDto> { Success = false, Message = "Nincs jogosultsága ehhez a művelethez!" });
        //    }
        //    return _itemNatureService.GetItemNatureById(itemNatureId);
        //}

        [HttpPost]
        public Task<AlpApiResponse<ItemNatureDto>> AddNewItemNature([FromBody] ItemNatureDto itemNature)
        {
            var sessionToken = HttpContext.Request.Headers["sessiontoken"];
            if (!_accountService.AuthorizeAsync(sessionToken, new List<RoleType> { RoleType.Admin }))
            {
                return Task.FromResult(new AlpApiResponse<ItemNatureDto> { Success = false, Message = "Nincs jogosultsága ehhez a művelethez!" });
            }
            return _itemNatureService.AddNewItemNature(itemNature);
        }

        //[HttpDelete]
        //public Task<AlpApiResponse> DeleteItemNatureById(int itemNatureId)
        //{
        //    var sessionToken = HttpContext.Request.Headers["sessiontoken"];
        //    if (!_accountService.AuthorizeAsync(sessionToken, new List<RoleType> { RoleType.Admin }))
        //    {
        //        return Task.FromResult(new AlpApiResponse { Success = false, Message = "Nincs jogosultsága ehhez a művelethez!" });
        //    }
        //    return _itemNatureService.DeleteItemNatureById(itemNatureId);
        //}

        [HttpPost]
        public Task<AlpApiResponse> UpdateItemNature([FromBody] ItemNatureDto itemNature)
        {
            var sessionToken = HttpContext.Request.Headers["sessiontoken"];
            if (!_accountService.AuthorizeAsync(sessionToken, new List<RoleType> { RoleType.Admin }))
            {
                return Task.FromResult(new AlpApiResponse { Success = false, Message = "Nincs jogosultsága ehhez a művelethez!" });
            }
            return _itemNatureService.UpdateItemNature(itemNature);
        }

        [HttpPost]
        public Task<AlpApiResponse> ToggleLockStateByIdItemNature([FromBody] int itemNatureId)
        {
            var sessionToken = HttpContext.Request.Headers["sessiontoken"];
            if (!_accountService.AuthorizeAsync(sessionToken, new List<RoleType> { RoleType.Admin }))
            {
                return Task.FromResult(new AlpApiResponse { Success = false, Message = "Nincs jogosultsága ehhez a művelethez!" });
            }
            return _itemNatureService.ToggleItemNatureLockStateById(itemNatureId);
        }
    }
}