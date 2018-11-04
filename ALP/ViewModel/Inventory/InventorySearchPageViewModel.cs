using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ALP.Service.Interface;
using Common.Model;
using Common.Model.Enum;
using GalaSoft.MvvmLight.CommandWpf;

namespace ALP.ViewModel.Inventory
{
    public class InventorySearchPageViewModel : AlpViewModelBase
    {
        private readonly IInventoryApiService _inventoryApiService;
        private static List<int> PageSizeList { get; } = new List<int> { 10, 25, 50, 100 };

        public DataTable MyDataTable { get; set; }

        public ICommand MouseLabelClickCommand { get; private set; }
        public ICommand JumpToFirstPageCommand { get; private set; }
        public ICommand PreviousPageCommand { get; private set; }
        public ICommand JumpToLastPageCommand { get; private set; }
        public ICommand NextPageCommand { get; private set; }
        public ICommand SearchCommand { get; private set; }
        public ICommand ImportCommand { get; private set; }
        public ICommand ExportCommand { get; private set; }
        public ICommand FilterCommand { get; private set; }
        public ICommand DeleteFilterCommand { get; private set; }
        public ICommand QuickCommandsCommand { get; private set; }

        private List<ItemPropertyType> ProjectedTypes { get; set; }

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

            MouseLabelClickCommand = new RelayCommand<TextBlock>(OnMouseLabelClickCommand);
            JumpToFirstPageCommand = new RelayCommand(OnJumpToFirstPageCommand);
            PreviousPageCommand = new RelayCommand(OnPreviousPageCommand);
            JumpToLastPageCommand = new RelayCommand(OnJumpToLastPageCommand);
            NextPageCommand = new RelayCommand(OnNextPageCommand);
            SearchCommand = new RelayCommand(OnSearchCommand);
            ImportCommand = new RelayCommand(OnImportCommand);
            ExportCommand = new RelayCommand(OnExportCommand);
            FilterCommand = new RelayCommand(OnFilterCommand);
            DeleteFilterCommand = new RelayCommand(OnDeleteFilterCommand);
            QuickCommandsCommand = new RelayCommand(OnQuickCommandsCommand);

            ItemFilterInfo = new InventoryItemFilterInfo();
            ProjectedTypes = new List<ItemPropertyType>
            {
                ItemPropertyType.ItemName,
                ItemPropertyType.InventoryNumber,
            };

            SelectedPageNumber = 1;
            SelectedPageSize = 10;


            Initialization = InitializeAsync();
        }

        private void OnQuickCommandsCommand()
        {
            throw new NotImplementedException();
        }

        private void OnDeleteFilterCommand()
        {
            throw new NotImplementedException();
        }

        private void OnFilterCommand()
        {
            throw new NotImplementedException();
        }

        private void OnExportCommand()
        {
            throw new NotImplementedException();
        }

        private void OnImportCommand()
        {
            throw new NotImplementedException();
        }

        private void OnSearchCommand()
        {
            throw new NotImplementedException();
        }

        private void OnNextPageCommand()
        {
            throw new NotImplementedException();
        }

        private void OnJumpToLastPageCommand()
        {
            throw new NotImplementedException();
        }

        private void OnPreviousPageCommand()
        {
            throw new NotImplementedException();
        }

        private void OnJumpToFirstPageCommand()
        {
            throw new NotImplementedException();
        }

        public bool IsNotFirstPage { get => SelectedPageNumber > 1; }
        //TODO: logika
        //public bool IsNotLastPage { get => }

        private void OnMouseLabelClickCommand(TextBlock obj)
        {
            var tag = (string)obj.Tag;
            var propertyType = (ItemPropertyType)int.Parse(tag);
            if (!ProjectedTypes.Contains(propertyType))
            {
                obj.FontWeight = FontWeights.Bold;
                obj.TextDecorations.Add(TextDecorations.Underline);
                ProjectedTypes.Add(propertyType);
            }
            else
            {
                ProjectedTypes.Remove(propertyType);
                obj.FontWeight = FontWeights.Normal;
                obj.TextDecorations.Clear();
            }
        }

        protected override async Task InitializeAsync()
        {
            try
            {
                IsLoading = true;
                
                var itemList = await _inventoryApiService.FindFilteredItemsAsStringForDisplay(ItemFilterInfo);

                //ItemDisplayList = new ObservableCollection<List<string>>(itemList);
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
