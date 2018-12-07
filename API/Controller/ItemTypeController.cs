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
    public class ItemTypeController : ControllerBase
    {
        private readonly IItemTypeService _itemTypeService;
        private readonly IAccountService _accountService;

        public ItemTypeController(IItemTypeService itemTypeService, IAccountService accountService)
        {
            _itemTypeService = itemTypeService;
            _accountService = accountService;
        }

        [HttpGet]
        public Task<AlpApiResponse<List<ItemTypeDto>>> GetAllItemType()
        {
            var sessionToken = HttpContext.Request.Headers["sessiontoken"];
            if (!_accountService.Authorize(sessionToken, new List<RoleType> { RoleType.Admin, RoleType.DepartmentInventoryOperator }))
            {
                return Task.FromResult(new AlpApiResponse<List<ItemTypeDto>> { Success = false, Message = "Nincs jogosultsága ehhez a művelethez!" });
            }
            return _itemTypeService.GetAllItemTypes();
        }

        [HttpGet]
        public Task<AlpApiResponse<List<ItemTypeDto>>> GetAvailableItemType()
        {
            var sessionToken = HttpContext.Request.Headers["sessiontoken"];
            if (!_accountService.Authorize(sessionToken, new List<RoleType> { RoleType.Admin, RoleType.DepartmentInventoryOperator }))
            {
                return Task.FromResult(new AlpApiResponse<List<ItemTypeDto>> { Success = false, Message = "Nincs jogosultsága ehhez a művelethez!" });
            }
            return _itemTypeService.GetAvailableItemTypes();
        }

        //[HttpGet]
        //public Task<AlpApiResponse<ItemTypeDto>> GetItemTypeById(int itemTypeId)
        //{
        //    var sessionToken = HttpContext.Request.Headers["sessiontoken"];
        //    if (!_accountService.AuthorizeAsync(sessionToken, new List<RoleType> { RoleType.Admin }))
        //    {
        //        return Task.FromResult(new AlpApiResponse<ItemTypeDto> { Success = false, Message = "Nincs jogosultsága ehhez a művelethez!" });
        //    }
        //    return _itemTypeService.GetItemTypeById(itemTypeId);
        //}

        [HttpPost]
        public Task<AlpApiResponse<ItemTypeDto>> AddNewItemType([FromBody] ItemTypeDto itemType)
        {
            var sessionToken = HttpContext.Request.Headers["sessiontoken"];
            if (!_accountService.Authorize(sessionToken, new List<RoleType> { RoleType.Admin }))
            {
                return Task.FromResult(new AlpApiResponse<ItemTypeDto> { Success = false, Message = "Nincs jogosultsága ehhez a művelethez!" });
            }
            return _itemTypeService.InsertNewItemType(itemType);
        }

        [HttpPost]
        public Task<AlpApiResponse> UpdateItemType([FromBody] ItemTypeDto itemType)
        {
            var sessionToken = HttpContext.Request.Headers["sessiontoken"];
            if (!_accountService.Authorize(sessionToken, new List<RoleType> { RoleType.Admin }))
            {
                return Task.FromResult(new AlpApiResponse { Success = false, Message = "Nincs jogosultsága ehhez a művelethez!" });
            }
            return _itemTypeService.UpdateItemType(itemType);
        }

        //[HttpDelete]
        //public Task<AlpApiResponse> DeleteItemTypeById(int itemTypeId)
        //{
        //    var sessionToken = HttpContext.Request.Headers["sessiontoken"];
        //    if (!_accountService.AuthorizeAsync(sessionToken, new List<RoleType> { RoleType.Admin }))
        //    {
        //        return Task.FromResult(new AlpApiResponse { Success = false, Message = "Nincs jogosultsága ehhez a művelethez!" });
        //    }
        //    return _itemTypeService.DeleteItemTypeById(itemTypeId);
        //}

        [HttpPost]
        public Task<AlpApiResponse> ToggleLockStateByIdItemType([FromBody] int itemTypeId)
        {
            var sessionToken = HttpContext.Request.Headers["sessiontoken"];
            if (!_accountService.Authorize(sessionToken, new List<RoleType> { RoleType.Admin }))
            {
                return Task.FromResult(new AlpApiResponse { Success = false, Message = "Nincs jogosultsága ehhez a művelethez!" });
            }
            return _itemTypeService.ToggleItemTypeLockStateById(itemTypeId);
        }
    }
}