using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Entity;
using DAL.Service;
using Microsoft.AspNetCore.Mvc;
using Common.Model.Dto;

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
        public Task<List<ItemTypeDto>> GetAllItemType()
        {
            return _itemTypeService.GetAllItemTypes();
        }

        [HttpGet]
        public Task<List<ItemTypeDto>> GetAvailableItemType()
        {
            return _itemTypeService.GetAvailableItemTypes();
        }

        [HttpGet]
        public Task<ItemTypeDto> GetItemTypeById(int itemTypeId)
        {
            return _itemTypeService.GetItemTypeById(itemTypeId);
        }

        [HttpPost]
        public Task<ItemTypeDto> AddNewItemType([FromBody] ItemTypeDto itemType)
        {
            return _itemTypeService.InsertNewItemType(itemType);
        }

        [HttpPost]
        public void UpdateItemType([FromBody] ItemTypeDto itemType)
        {
            _itemTypeService.UpdateItemType(itemType);
        }

        [HttpDelete]
        public void DeleteItemTypeById(int itemTypeId)
        {
            _itemTypeService.DeleteItemTypeById(itemTypeId);
        }

        [HttpPost]
        public void ToggleLockStateByIdItemType([FromBody] int itemTypeId)
        {
            _itemTypeService.ToggleItemTypeLockStateById(itemTypeId);
        }
    }
}