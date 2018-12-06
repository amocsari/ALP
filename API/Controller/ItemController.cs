using System.Collections.Generic;
using System.Threading.Tasks;
using API.Service;
using Common.Model;
using Common.Model.Dto;
using Microsoft.AspNetCore.Mvc;
using Model.Enum;
using Model.Model;

namespace API.Controller
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;
        private readonly IAccountService _accountService;

        public ItemController(IItemService itemService, IAccountService accountService)
        {
            _itemService = itemService;
            _accountService = accountService;
        }

        [HttpPost]
        public Task<AlpApiResponse<List<ItemDto>>> FilterItems(InventoryItemFilterInfo info)
        {
            var sessionToken = HttpContext.Request.Headers["sessiontoken"];
            if (!_accountService.AuthorizeAsync(sessionToken, new List<RoleType> { RoleType.Admin, RoleType.DepartmentInventoryOperator }))
            {
                return Task.FromResult(new AlpApiResponse<List<ItemDto>> { Success = false, Message = "Nincs jogosultsága ehhez a művelethez!" });
            }
            return _itemService.FindItemsForDisplay(info);
        }

        [HttpPost]
        public Task<AlpApiResponse> AddNewItem([FromBody] ItemDto item)
        {
            var sessionToken = HttpContext.Request.Headers["sessiontoken"];
            if (!_accountService.AuthorizeAsync(sessionToken, new List<RoleType> { RoleType.Admin }))
            {
                return Task.FromResult(new AlpApiResponse { Success = false, Message = "Nincs jogosultsága ehhez a művelethez!" });
            }
            return _itemService.AddNewItem(item);
        }

        [HttpPost]
        public Task<AlpApiResponse<List<ItemDto>>> ImportItems([FromBody] List<ImportedItem> importedItems)
        {
            var sessionToken = HttpContext.Request.Headers["sessiontoken"];
            if (!_accountService.AuthorizeAsync(sessionToken, new List<RoleType> { RoleType.Admin }))
            {
                return Task.FromResult(new AlpApiResponse<List<ItemDto>> { Success = false, Message = "Nincs jogosultsága ehhez a művelethez!" });
            }
            return _itemService.ImportItems(importedItems);
        }
    }
}