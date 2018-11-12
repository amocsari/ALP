using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Service;
using Microsoft.AspNetCore.Mvc;
using Common.Model.Dto;

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
        public Task<List<ItemStateDto>> GetAllItemState()
        {
            return _itemStateService.GetAllItemStates();
        }

        [HttpGet]
        public Task<List<ItemStateDto>> GetAvailableItemState()
        {
            return _itemStateService.GetAvailableItemStates();
        }

        [HttpGet]
        public Task<ItemStateDto> GetItemStateById(int itemStateId)
        {
            return _itemStateService.GetItemStateById(itemStateId);
        }

        [HttpPost]
        public Task<ItemStateDto> AddNewItemState([FromBody] ItemStateDto itemState)
        {
            return _itemStateService.AddNewItemState(itemState);
        }

        [HttpDelete]
        public void DeleteItemStateById(int itemStateId)
        {
            _itemStateService.Remove(b => b.ItemStateId == itemStateId);
        }

        [HttpPost]
        public void UpdateItemState([FromBody] ItemStateDto itemState)
        {
            _itemStateService.UpdateItemState(itemState);
        }

        [HttpPost]
        public void ToggleLockStateByIdItemState([FromBody] int itemStateId)
        {
            _itemStateService.ToggleItemStateLockStateById(itemStateId);
        }
    }
}