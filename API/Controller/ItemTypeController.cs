using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Common.Model.Dto;
using Model.Model;
using API.Service;

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
        public Task<AlpApiResponse<List<ItemTypeDto>>> GetAllItemType()
        {
            return _itemTypeService.GetAllItemTypes();
        }

        [HttpGet]
        public Task<AlpApiResponse<List<ItemTypeDto>>> GetAvailableItemType()
        {
            return _itemTypeService.GetAvailableItemTypes();
        }

        [HttpGet]
        public Task<AlpApiResponse<ItemTypeDto>> GetItemTypeById(int itemTypeId)
        {
            return _itemTypeService.GetItemTypeById(itemTypeId);
        }

        [HttpPost]
        public Task<AlpApiResponse<ItemTypeDto>> AddNewItemType([FromBody] ItemTypeDto itemType)
        {
            return _itemTypeService.InsertNewItemType(itemType);
        }

        [HttpPost]
        public Task<AlpApiResponse> UpdateItemType([FromBody] ItemTypeDto itemType)
        {
            return _itemTypeService.UpdateItemType(itemType);
        }

        [HttpDelete]
        public Task<AlpApiResponse> DeleteItemTypeById(int itemTypeId)
        {
            return _itemTypeService.DeleteItemTypeById(itemTypeId);
        }

        [HttpPost]
        public Task<AlpApiResponse> ToggleLockStateByIdItemType([FromBody] int itemTypeId)
        {
            return _itemTypeService.ToggleItemTypeLockStateById(itemTypeId);
        }
    }
}