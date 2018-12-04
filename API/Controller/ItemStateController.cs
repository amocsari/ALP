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
    public class ItemStateController : ControllerBase
    {
        private readonly IItemStateService _itemStateService;

        public ItemStateController(IItemStateService itemStateService)
        {
            _itemStateService = itemStateService;
        }

        [HttpGet]
        public Task<AlpApiResponse<List<ItemStateDto>>> GetAllItemState()
        {
            return _itemStateService.GetAllItemStates();
        }

        [HttpGet]
        public Task<AlpApiResponse<List<ItemStateDto>>> GetAvailableItemState()
        {
            return _itemStateService.GetAvailableItemStates();
        }

        [HttpGet]
        public Task<AlpApiResponse<ItemStateDto>> GetItemStateById(int itemStateId)
        {
            return _itemStateService.GetItemStateById(itemStateId);
        }

        [HttpPost]
        public Task<AlpApiResponse<ItemStateDto>> AddNewItemState([FromBody] ItemStateDto itemState)
        {
            return _itemStateService.AddNewItemState(itemState);
        }

        [HttpDelete]
        public Task<AlpApiResponse> DeleteItemStateById(int itemStateId)
        {
            return _itemStateService.DeleteItemStateById(itemStateId);
        }

        [HttpPost]
        public Task<AlpApiResponse> UpdateItemState([FromBody] ItemStateDto itemState)
        {
            return _itemStateService.UpdateItemState(itemState);
        }

        [HttpPost]
        public Task<AlpApiResponse> ToggleLockStateByIdItemState([FromBody] int itemStateId)
        {
            return _itemStateService.ToggleItemStateLockStateById(itemStateId);
        }
    }
}