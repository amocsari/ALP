using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ALP.Navigation;
using ALP.Service;
using ALP.Service.Interface;
using ALP.View.Inventory;
using Common.Model;
using Common.Model.Dto;
using GalaSoft.MvvmLight.CommandWpf;
using JB.Collections.Reactive;

namespace ALP.ViewModel.Inventory
{
    public class InventorySearchPageViewModel : AlpViewModelBase
    {
        private ObservableDictionary<string, Visibility> visibilities;
        public ObservableDictionary<string, Visibility> Visibilities
        {
            get => visibilities;
            set
            {
                if (Visibilities != value)
                {
                    Set(ref visibilities, value);
                }
            }
        }

        public List<ItemPropertyType> ProjectedTypes { get; set; }

        private System.Collections.ObjectModel.ObservableCollection<InventoryItemSearchListItemViewModel> itemList;
        public System.Collections.ObjectModel.ObservableCollection<InventoryItemSearchListItemViewModel> ItemList
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

        private InventoryItemSearchListItemViewModel selectedItem;
        public InventoryItemSearchListItemViewModel SelectedItem
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
        public ICommand ExportCommand { get; private set; }
        public ICommand FilterCommand { get; private set; }
        public ICommand DeleteFilterCommand { get; private set; }
        public ICommand QuickCommandsCommand { get; private set; }


        private readonly IInventoryApiService _inventoryApiService;
        private readonly IAlpDialogService _dialogService;
        private readonly IAlpLoggingService<InventorySearchPageViewModel> _loggingService;
        private readonly IAlpNavigationService _navigationService;
        private readonly IExportService _exportService;

        public InventorySearchPageViewModel(IInventoryApiService inventoryApiService, IAlpDialogService dialogService, IAlpLoggingService<InventorySearchPageViewModel> loggingService, IAlpNavigationService navigationService, IExportService exportService)
        {
            _inventoryApiService = inventoryApiService;
            _dialogService = dialogService;
            _loggingService = loggingService;
            _navigationService = navigationService;
            _exportService = exportService;

            MouseLabelClickCommand = new RelayCommand<TextBlock>(OnMouseLabelClickCommand);
            SearchCommand = new RelayCommand(OnSearchCommand);
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
            visibilities = new ObservableDictionary<string, Visibility>
            {
                { "TextBlockOldInventoryNumber", Visibility.Collapsed },
                { "TextBlockSerialNumber", Visibility.Collapsed },
                { "TextBlockAccreditationNumber", Visibility.Collapsed },
                { "TextBlockYellowNumber", Visibility.Collapsed },
                { "TextBlockManufacturerType", Visibility.Collapsed },
                { "TextBlockItemNature", Visibility.Collapsed },
                { "TextBlockItemType", Visibility.Collapsed },
                { "TextBlockProductionYear", Visibility.Collapsed },
                { "TextBlockDepartment", Visibility.Collapsed },
                { "TextBlockSection", Visibility.Collapsed },
                { "TextBlockEmployee", Visibility.Collapsed },
                { "TextBlockBuilding", Visibility.Collapsed },
                { "TextBlockFloor", Visibility.Collapsed },
                { "TextBlockRoom", Visibility.Collapsed },
                { "TextBlockItemState", Visibility.Collapsed },
                { "TextBlockDateOfCreation", Visibility.Collapsed },
                { "TextBlockBruttoPrice", Visibility.Collapsed },
                { "TextBlockComment", Visibility.Collapsed },
                { "TextBlockDateOfScrap", Visibility.Collapsed }
            };


            Initialization = InitializeAsync();
        }

        private void OnQuickCommandsCommand()
        {
            var selectedIds = itemList.Where(item => item.IsSelected).Select(item => item.Value.ItemID).ToList();
            _dialogService.ShowDialog<OperationWindow, InventoryOperationWindowViewModel, List<int>, bool>(selectedIds);
        }

        private void OnDeleteFilterCommand()
        {
            //TODO
        }

        private void OnFilterCommand()
        {
            //TODO
        }

        private void OnExportCommand()
        {
            try
            {
                IsLoading = true;

                var itemList = ItemList.Select(item => item.Value).ToList();
                _exportService.ExportToExcel(itemList, ProjectedTypes);
            }
            catch (Exception e)
            {
                _loggingService.LogInformation("Error during InventorySearch", e);
                _dialogService.ShowAlert(e.Message);
            }
            finally
            {
                IsLoading = false;
            }
        }

        private async void OnSearchCommand()
        {
            try
            {
                IsLoading = true;
                if (!string.IsNullOrEmpty(FilteredId))
                {
                    ItemFilterInfo.Id.Add(FilteredId);
                }
                var items = await _inventoryApiService.FilterItems(ItemFilterInfo);
                var itemViewModels = items.Select(item => new InventoryItemSearchListItemViewModel { Value = item }).ToList();
                ItemList = new System.Collections.ObjectModel.ObservableCollection<InventoryItemSearchListItemViewModel>(itemViewModels);
                ItemFilterInfo.Id.Clear();
            }
            catch (Exception e)
            {
                _loggingService.LogInformation("Error during InventorySearch", e);
                _dialogService.ShowAlert(e.Message);
            }
            finally
            {
                IsLoading = false;
            }
        }

        private void OnMouseLabelClickCommand(TextBlock obj)
        {
            if (Visibilities[obj.Name] == Visibility.Collapsed)
                Visibilities[obj.Name] = Visibility.Visible;
            else
            {
                Visibilities[obj.Name] = Visibility.Collapsed;
            }


            var tag = (string)obj.Tag;
            var propertyType = (ItemPropertyType)int.Parse(tag);
            if (!ProjectedTypes.Contains(propertyType))
            {
                obj.FontWeight = FontWeights.Bold;
                obj.TextDecorations.Add(TextDecorations.Underline);
                ProjectedTypes.Add(propertyType);
                RaisePropertyChanged(() => obj.FontWeight);
            }
            else
            {
                ProjectedTypes.Remove(propertyType);
                obj.FontWeight = FontWeights.Normal;
                obj.TextDecorations.Clear();
                RaisePropertyChanged(() => obj.FontWeight);
            }

        }

        protected override async Task InitializeAsync()
        {
            try
            {
                itemList = new System.Collections.ObjectModel.ObservableCollection<InventoryItemSearchListItemViewModel>();

                if (_navigationService.Parameter != null && _navigationService.Parameter is List<ItemDto>)
                {
                    var items = (List<ItemDto>)_navigationService.Parameter;
                    var itemvmlist = items.Select(item => new InventoryItemSearchListItemViewModel { Value = item });
                    ItemList = new System.Collections.ObjectModel.ObservableCollection<InventoryItemSearchListItemViewModel>(itemvmlist);
                }
            }
            catch (Exception e)
            {
                _loggingService.LogFatal("Error during initalization!", e);
                _dialogService.ShowError(e.Message);
            }
        }
    }
}
