using System.Collections.Generic;
using DAL.Entity;
using DAL.Service;
using Microsoft.AspNetCore.Mvc;

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
        public List<ItemState> GetAllItemStates()
        {
            return _itemStateService.GetAll();
        }

        [HttpGet]
        public ItemState GetItemStateById(int itemStateId)
        {
            return _itemStateService.GetSingle(b => b.ItemStateID == itemStateId);
        }

        [HttpPost]
        public void AddNewItemState([FromBody] ItemState itemState)
        {
            _itemStateService.InsertNew(itemState);
        }

        [HttpDelete]
        public void DeleteItemStateById(int itemStateId)
        {
            _itemStateService.Remove(b => b.ItemStateID == itemStateId);
        }
    }
}