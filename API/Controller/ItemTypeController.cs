using System.Collections.Generic;
using DAL.Entity;
using DAL.Service;
using Microsoft.AspNetCore.Mvc;

namespace API.Controller
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ItemTypeController : ControllerBase
    {
        private readonly IItemTypeService _itemTypeService;

        public ItemTypeController(IItemTypeService itemTypeService)
        {
            _itemTypeService = itemTypeService;
        }

        [HttpGet]
        public List<ItemType> GetAllItemTypes()
        {
            return _itemTypeService.GetAll();
        }

        [HttpGet]
        public ItemType GetItemTypeById(int itemTypeId)
        {
            return _itemTypeService.GetSingle(b => b.ItemTypeID == itemTypeId);
        }

        [HttpPost]
        public void AddNewItemType([FromBody] ItemType itemType)
        {
            _itemTypeService.InsertNew(itemType);
        }

        [HttpDelete]
        public void DeleteItemTypeById(int itemTypeId)
        {
            _itemTypeService.Remove(b => b.ItemTypeID == itemTypeId);
        }
    }
}