using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Model;
using Common.Model.Dto;
using DAL.Service;
using Microsoft.AspNetCore.Mvc;
using Model.Model;

namespace API.Controller
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;

        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpPost]
        public Task<AlpApiResponse<List<ItemDto>>> FilterItems(InventoryItemFilterInfo info)
        {
            return _itemService.FindItemsForDisplay(info);
        }

        //[HttpGet]
        //public Task<ItemDto> GetItemById(int itemId)
        //{
        //    return _itemService.GetSingle(b => b.ItemID == itemId);
        //}

        [HttpPost]
        public Task<AlpApiResponse> AddNewItem([FromBody] ItemDto item)
        {
            return _itemService.AddNewItem(item);
        }
    }
}