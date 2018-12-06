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
        private readonly IAlpLoggingService<InventoryApiService> _loggingService;

        public InventoryApiService(IAlpApiService apiService, IAlpLoggingService<InventoryApiService> loggingService)
        {
            _apiService = apiService;
            _loggingService = loggingService;
        }

        public async Task<bool> AddNewInventoryItem(ItemDto dto)
        {
            _loggingService.LogDebug(new
            {
                action = nameof(AddNewInventoryItem),
                dto = dto.ToString()
            });

            return await _apiService.PostAsync("Item/AddNewItem", dto);
        }

        public async Task<List<ItemDto>> FilterItems(InventoryItemFilterInfo filterInfo)
        {
            _loggingService.LogDebug(new
            {
                action = nameof(FilterItems),
                filterInfo = filterInfo.ToString()
            });

            return await _apiService.PostAsync<InventoryItemFilterInfo, List<ItemDto>>("Item/FilterItems", filterInfo);
        }

        public async Task<ItemDto> GetItemById(int itemId)
        {
            _loggingService.LogDebug(new
            {
                action = nameof(GetItemById),
                itemId
            });

            return await _apiService.PostAsync<int, ItemDto>("Item/FindItemById", itemId);
        }

        public async Task<List<ItemDto>> ImportItems(List<ImportedItem> importedItems)
        {
            _loggingService.LogDebug(new
            {
                action = nameof(GetItemById),
                importedItems
            });

            return await _apiService.PostAsync<List<ImportedItem>, List<ItemDto>>("Item/ImportItems", importedItems);
        }
    }
}
