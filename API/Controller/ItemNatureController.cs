using System.Collections.Generic;
using DAL.Service;
using Microsoft.AspNetCore.Mvc;
using Common.Model.Dto;
using Model.Model;
using System.Threading.Tasks;

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
        public Task<AlpApiResponse<List<ItemNatureDto>>> GetAllItemNature()
        {
            return _itemNatureService.GetAllItemNatures();
        }

        [HttpGet]
        public Task<AlpApiResponse<List<ItemNatureDto>>> GetAvailableItemNature()
        {
            return _itemNatureService.GetAvailableItemNatures();
        }

        [HttpGet]
        public Task<AlpApiResponse<ItemNatureDto>> GetItemNatureById(int itemNatureId)
        {
            return _itemNatureService.GetItemNatureById(itemNatureId);
        }

        [HttpPost]
        public Task<AlpApiResponse<ItemNatureDto>> AddNewItemNature([FromBody] ItemNatureDto itemNature)
        {
            return _itemNatureService.AddNewItemNature(itemNature);
        }

        [HttpDelete]
        public Task<AlpApiResponse> DeleteItemNatureById(int itemNatureId)
        {
            return _itemNatureService.DeleteItemNatureById(itemNatureId);
        }

        [HttpPost]
        public Task<AlpApiResponse> UpdateItemNature([FromBody] ItemNatureDto itemNature)
        {
            return _itemNatureService.UpdateItemNature(itemNature);
        }

        [HttpPost]
        public Task<AlpApiResponse> ToggleLockStateByIdItemNature([FromBody] int itemNatureId)
        {
            return _itemNatureService.ToggleItemNatureLockStateById(itemNatureId);
        }
    }
}