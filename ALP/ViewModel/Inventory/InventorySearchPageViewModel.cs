using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Input;
using ALP.Service.Interface;
using Common.Model;
using Exception = System.Exception;

namespace ALP.ViewModel.Inventory
{
    public class InventorySearchPageViewModel : AlpViewModelBase
    {
        private readonly IInventoryApiService _inventoryApiService;
        private static List<int> PageSizeList { get; } = new List<int> { 10, 25, 50, 100 };

        public DataTable MyDataTable { get; set; }

        public Action OnMouseDown { get; set; }

        private ObservableCollection<List<string>> itemDisplayList;
        public ObservableCollection<List<string>> ItemDisplayList
        {
            get { return itemDisplayList; }
            set
            {
                if (itemDisplayList != value)
                {
                    Set(ref itemDisplayList, value);
                }
            }
        }

        public InventoryItemFilterInfo ItemFilterInfo { get; set; }
        public string FilteredId { get; set; }

        public int SelectedPageNumber { get; set; }

        public int SelectedPageSize
        {
            get { return ItemFilterInfo.PageSize; }
            set
            {
                if (ItemFilterInfo.PageSize != value && PageSizeList.Contains(value))
                {
                    ItemFilterInfo.PageSize = value;
                    RaisePropertyChanged();
                }
            }
        }

        public InventorySearchPageViewModel(IInventoryApiService inventoryApiService)
        {
            _inventoryApiService = inventoryApiService;

            ItemFilterInfo = new InventoryItemFilterInfo();

            SelectedPageNumber = 1;
            SelectedPageSize = 10;


            Initialization = InitializeAsync();
        }

        protected override async Task InitializeAsync()
        {
            try
            {
                IsLoading = true;
                //var itemList = await _inventoryApiService.FindFilteredItemsAsStringForDisplay(ItemFilterInfo);
                var itemList = new List<List<string>> {new List<string>{"1", "2", "3"}};

                ItemDisplayList = new ObservableCollection<List<string>>(itemList);
            }
            catch (Exception)
            {
                //TODO: logging
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}
