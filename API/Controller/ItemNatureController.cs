using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Service;
using Microsoft.AspNetCore.Mvc;
using Common.Model.Dto;

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
        public Task<List<ItemNatureDto>> GetAllItemNature()
        {
            return _itemNatureService.GetAllItemNatures();
        }

        [HttpGet]
        public Task<List<ItemNatureDto>> GetAvailableItemNature()
        {
            return _itemNatureService.GetAvailableItemNatures();
        }

        [HttpGet]
        public Task<ItemNatureDto> GetItemNatureById(int itemNatureId)
        {
            return _itemNatureService.GetItemNatureById(itemNatureId);
        }

        [HttpPost]
        public Task<ItemNatureDto> AddNewItemNature([FromBody] ItemNatureDto itemNature)
        {
            return _itemNatureService.AddNewItemNature(itemNature);
        }

        [HttpDelete]
        public void DeleteItemNatureById(int itemNatureId)
        {
            _itemNatureService.Remove(b => b.ItemNatureId == itemNatureId);
        }

        [HttpPost]
        public void UpdateItemNature([FromBody] ItemNatureDto itemNature)
        {
            _itemNatureService.UpdateItemNature(itemNature);
        }

        [HttpPost]
        public void ToggleLockStateByIdItemNature([FromBody] int itemNatureId)
        {
            _itemNatureService.ToggleItemNatureLockStateById(itemNatureId);
        }
    }
}