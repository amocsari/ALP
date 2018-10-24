using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Entity;
using DAL.Service;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet]
        public Task<List<Item>> GetAllItems()
        {
            return _itemService.GetAll();
        }

        [HttpGet]
        public Task<Item> GetItemById(int itemId)
        {
            return _itemService.GetSingle(b => b.ItemID == itemId);
        }

        [HttpPost]
        public void AddNewItem([FromBody] Item item)
        {
            _itemService.InsertNew(item);
        }

        [HttpDelete]
        public void DeleteItemById(int itemId)
        {
            _itemService.Remove(b => b.ItemID == itemId);
        }
    }
}