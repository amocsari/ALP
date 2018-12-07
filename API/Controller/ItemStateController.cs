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
    public class ItemStateController : ControllerBase
    {
        private readonly IItemStateService _itemStateService;
        private readonly IAccountService _accountService;

        public ItemStateController(IItemStateService itemStateService, IAccountService accountService)
        {
            _itemStateService = itemStateService;
            _accountService = accountService;
        }

        [HttpGet]
        public Task<AlpApiResponse<List<ItemStateDto>>> GetAllItemState()
        {
            var sessionToken = HttpContext.Request.Headers["sessiontoken"];
            if (!_accountService.Authorize(sessionToken, new List<RoleType> { RoleType.Admin, RoleType.DepartmentInventoryOperator }))
            {
                return Task.FromResult(new AlpApiResponse<List<ItemStateDto>> { Success = false, Message = "Nincs jogosultsága ehhez a művelethez!" });
            }
            return _itemStateService.GetAllItemStates();
        }

        [HttpGet]
        public Task<AlpApiResponse<List<ItemStateDto>>> GetAvailableItemState()
        {
            var sessionToken = HttpContext.Request.Headers["sessiontoken"];
            if (!_accountService.Authorize(sessionToken, new List<RoleType> { RoleType.Admin, RoleType.DepartmentInventoryOperator }))
            {
                return Task.FromResult(new AlpApiResponse<List<ItemStateDto>> { Success = false, Message = "Nincs jogosultsága ehhez a művelethez!" });
            }
            return _itemStateService.GetAvailableItemStates();
        }

        //[HttpGet]
        //public Task<AlpApiResponse<ItemStateDto>> GetItemStateById(int itemStateId)
        //{
        //    var sessionToken = HttpContext.Request.Headers["sessiontoken"];
        //    if (!_accountService.AuthorizeAsync(sessionToken, new List<RoleType> { RoleType.Admin }))
        //    {
        //        return Task.FromResult(new AlpApiResponse<ItemStateDto> { Success = false, Message = "Nincs jogosultsága ehhez a művelethez!" });
        //    }
        //    return _itemStateService.GetItemStateById(itemStateId);
        //}

        [HttpPost]
        public Task<AlpApiResponse<ItemStateDto>> AddNewItemState([FromBody] ItemStateDto itemState)
        {
            var sessionToken = HttpContext.Request.Headers["sessiontoken"];
            if (!_accountService.Authorize(sessionToken, new List<RoleType> { RoleType.Admin }))
            {
                return Task.FromResult(new AlpApiResponse<ItemStateDto> { Success = false, Message = "Nincs jogosultsága ehhez a művelethez!" });
            }
            return _itemStateService.AddNewItemState(itemState);
        }

        //[HttpDelete]
        //public Task<AlpApiResponse> DeleteItemStateById(int itemStateId)
        //{
        //    var sessionToken = HttpContext.Request.Headers["sessiontoken"];
        //    if (!_accountService.AuthorizeAsync(sessionToken, new List<RoleType> { RoleType.Admin }))
        //    {
        //        return Task.FromResult(new AlpApiResponse { Success = false, Message = "Nincs jogosultsága ehhez a művelethez!" });
        //    }
        //    return _itemStateService.DeleteItemStateById(itemStateId);
        //}

        [HttpPost]
        public Task<AlpApiResponse> UpdateItemState([FromBody] ItemStateDto itemState)
        {
            var sessionToken = HttpContext.Request.Headers["sessiontoken"];
            if (!_accountService.Authorize(sessionToken, new List<RoleType> { RoleType.Admin }))
            {
                return Task.FromResult(new AlpApiResponse { Success = false, Message = "Nincs jogosultsága ehhez a művelethez!" });
            }
            return _itemStateService.UpdateItemState(itemState);
        }

        [HttpPost]
        public Task<AlpApiResponse> ToggleLockStateByIdItemState([FromBody] int itemStateId)
        {
            var sessionToken = HttpContext.Request.Headers["sessiontoken"];
            if (!_accountService.Authorize(sessionToken, new List<RoleType> { RoleType.Admin }))
            {
                return Task.FromResult(new AlpApiResponse { Success = false, Message = "Nincs jogosultsága ehhez a művelethez!" });
            }
            return _itemStateService.ToggleItemStateLockStateById(itemStateId);
        }
    }
}