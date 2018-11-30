using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ALP.Service;
using ALP.Service.Interface;
using Common.Model;
using Common.Model.Dto;
using Common.Model.Enum;
using GalaSoft.MvvmLight.CommandWpf;

namespace ALP.ViewModel.Inventory
{
    public class InventorySearchPageViewModel : AlpViewModelBase
    {
        public List<ItemPropertyType> ProjectedTypes { get; set; }

        private ObservableCollection<ItemDto> itemList;
        public ObservableCollection<ItemDto> ItemList
        {
            get { return itemList; }
            set
            {
                if (itemList != value)
                {
                    Set(ref itemList, value);
                }
            }
        }

        private ItemDto selectedItem;
        public ItemDto SelectedItem
        {
            get { return selectedItem; }
            set
            {
                if (selectedItem != value)
                {
                    Set(ref selectedItem, value);
                }
            }
        }

        public InventoryItemFilterInfo ItemFilterInfo { get; set; }
        public string FilteredId { get; set; }


        public ICommand MouseLabelClickCommand { get; private set; }
        public ICommand SearchCommand { get; private set; }
        public ICommand ImportCommand { get; private set; }
        public ICommand ExportCommand { get; private set; }
        public ICommand FilterCommand { get; private set; }
        public ICommand DeleteFilterCommand { get; private set; }
        public ICommand QuickCommandsCommand { get; private set; }


        private readonly IInventoryApiService _inventoryApiService;

        public InventorySearchPageViewModel(IInventoryApiService inventoryApiService)
        {
            _inventoryApiService = inventoryApiService;

            MouseLabelClickCommand = new RelayCommand<TextBlock>(OnMouseLabelClickCommand);
            SearchCommand = new RelayCommand(OnSearchCommand);
            ImportCommand = new RelayCommand(OnImportCommand);
            ExportCommand = new RelayCommand(OnExportCommand);
            FilterCommand = new RelayCommand(OnFilterCommand);
            DeleteFilterCommand = new RelayCommand(OnDeleteFilterCommand);
            QuickCommandsCommand = new RelayCommand(OnQuickCommandsCommand);

            ItemFilterInfo = new InventoryItemFilterInfo
            {
                Id = new List<string>()
            };
            ProjectedTypes = new List<ItemPropertyType>
            {
                ItemPropertyType.ItemName,
                ItemPropertyType.InventoryNumber,
            };


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
        }

        private void OnExportCommand()
        {
            throw new NotImplementedException();
        }

        private void OnImportCommand()
        {
            throw new NotImplementedException();
        }

        private async void OnSearchCommand()
        {
            try
            {
                IsLoading = true;
                if(!string.IsNullOrEmpty(FilteredId))
                {
                    ItemFilterInfo.Id.Add(FilteredId);
                }
                var items = await _inventoryApiService.FilterItems(ItemFilterInfo);
                ItemList = new ObservableCollection<ItemDto>(items);
                ItemFilterInfo.Id.Clear();
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

        protected override async Task InitializeAsync() { }
    }
}
