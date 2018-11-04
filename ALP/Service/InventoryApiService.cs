using System.Collections.Generic;
using System.Threading.Tasks;
using ALP.Service.Interface;
using Common.Model;
using Common.Model.Dto;

namespace ALP.Service
{
    public class InventoryApiService: IInventoryApiService
    {
        private readonly IAlpApiService _apiService;

        public InventoryApiService(IAlpApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<bool> AddNewInventoryItem(ItemDto dto)
        {
            return await _apiService.PostAsync("Item/AddNewItem", dto);
        }

        public async Task<List<List<string>>> FindFilteredItemsAsStringForDisplay(InventoryItemFilterInfo info)
        {
            return await _apiService.PostAsync<InventoryItemFilterInfo, List<List<string>>>("Item/FindItemsForDisplay", info);
        }

        public async Task<ItemDto> GetItemById(int itemId)
        {
            throw new System.NotImplementedException();
        }
    }
}
