using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Model.Dto;
using DAL.Service;
using Microsoft.AspNetCore.Mvc;

namespace API.Controller
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;

        //public ItemController(IItemService itemService)
        //{
        //    _itemService = itemService;
        //}

        //[HttpGet]
        //public Task<List<ItemDto>> GetAllItems()
        //{
        //    return _itemService.GetAll();
        //}

        //[HttpGet]
        //public Task<ItemDto> GetItemById(int itemId)
        //{
        //    return _itemService.GetSingle(b => b.ItemID == itemId);
        //}

        //[HttpPost]
        //public void AddNewItem([FromBody] ItemDto item)
        //{
        //    //_itemService.InsertNew(item);
        //}

        //[HttpDelete]
        //public void DeleteItemById(int itemId)
        //{
        //    _itemService.Remove(b => b.ItemID == itemId);
        //}
    }
}