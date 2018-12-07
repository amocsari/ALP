using System.Collections.Generic;
using System.Threading.Tasks;
using ALP.Service.Interface;
using Common.Model;
using Common.Model.Dto;

namespace ALP.Service
{
    /// <summary>
    /// Handles api calls related to items
    /// </summary>
    public class InventoryApiService: IInventoryApiService
    {
        /// <summary>
        /// injected services
        /// </summary>
        private readonly IAlpApiService _apiService;
        private readonly IAlpLoggingService<InventoryApiService> _loggingService;

        public InventoryApiService(IAlpApiService apiService, IAlpLoggingService<InventoryApiService> loggingService)
        {
            _apiService = apiService;
            _loggingService = loggingService;
        }

        /// <summary>
        /// creates a new item
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<bool> AddNewInventoryItem(ItemDto dto)
        {
            _loggingService.LogDebug(new
            {
                action = nameof(AddNewInventoryItem),
                dto = dto.ToString()
            });

            return await _apiService.PostAsync("Item/AddNewItem", dto);
        }

        /// <summary>
        /// gets the items according to the filter
        /// </summary>
        /// <param name="filterInfo"></param>
        /// <returns></returns>
        public async Task<List<ItemDto>> FilterItems(InventoryItemFilterInfo filterInfo)
        {
            _loggingService.LogDebug(new
            {
                action = nameof(FilterItems),
                filterInfo = filterInfo.ToString()
            });

            return await _apiService.PostAsync<InventoryItemFilterInfo, List<ItemDto>>("Item/FilterItems", filterInfo);
        }

        /// <summary>
        /// gets an item by its id
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public async Task<ItemDto> GetItemById(int itemId)
        {
            _loggingService.LogDebug(new
            {
                action = nameof(GetItemById),
                itemId
            });

            return await _apiService.PostAsync<int, ItemDto>("Item/FindItemById", itemId);
        }

        /// <summary>
        /// adds the items that were imported through an excel file to the database
        /// </summary>
        /// <param name="importedItems"></param>
        /// <returns></returns>
        public async Task<List<ItemDto>> ImportItems(List<ImportedItem> importedItems)
        {
            _loggingService.LogDebug(new
            {
                action = nameof(GetItemById),
                importedItems
            });

            return await _apiService.PostAsync<List<ImportedItem>, List<ItemDto>>("Item/ImportItems", importedItems);
        }

        /// <summary>
        /// Gets the itmes that are connectod a certain employee
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public async Task<List<ItemDto>> GetItemsByEmployeeId(int employeeId)
        {
            _loggingService.LogDebug(new
            {
                action = nameof(GetItemsByEmployeeId),
                employeeId
            });

            return await _apiService.PostAsync<int, List<ItemDto>>("Item/GetItemsByEmployeeId", employeeId);
        }
    }
}
