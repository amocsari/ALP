using System.Collections.Generic;
using DAL.Entity;
using DAL.Service;
using Microsoft.AspNetCore.Mvc;

namespace API.Controller
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ItemNatureController : ControllerBase
    {
        private readonly IItemNatureService _itemNatureService;

        public ItemNatureController(IItemNatureService itemNatureService)
        {
            _itemNatureService = itemNatureService;
        }

        [HttpGet]
        public List<ItemNature> GetAllItemNatures()
        {
            return _itemNatureService.GetAll();
        }

        [HttpGet]
        public ItemNature GetItemNatureById(int itemNatureId)
        {
            return _itemNatureService.GetSingle(b => b.ItemNatureID == itemNatureId);
        }

        [HttpPost]
        public void AddNewItemNature([FromBody] ItemNature itemNature)
        {
            _itemNatureService.InsertNew(itemNature);
        }

        [HttpDelete]
        public void DeleteItemNatureById(int itemNatureId)
        {
            _itemNatureService.Remove(b => b.ItemNatureID == itemNatureId);
        }
    }
}