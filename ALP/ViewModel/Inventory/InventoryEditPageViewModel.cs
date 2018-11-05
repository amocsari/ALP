using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using ALP.Service;
using ALP.Service.Interface;
using GalaSoft.MvvmLight.CommandWpf;
using Common.Model.Dto;

namespace ALP.ViewModel.Inventory
{
    public class ItemEditPageViewModel : AlpViewModelBase
    {
        private ObservableCollection<ItemNatureDto> itemNatures;
        public ObservableCollection<ItemNatureDto> ItemNatures
        {
            get { return itemNatures; }
            set
            {
                if (itemNatures != value)
                {
                    Set(ref itemNatures, value);
                }
            }
        }

        private ObservableCollection<ItemTypeDto> itemTypes;
        public ObservableCollection<ItemTypeDto> ItemTypes
        {
            get { return itemTypes; }
            set
            {
                if (itemTypes != value)
                {
                    Set(ref itemTypes, value);
                }
            }
        }

        private ObservableCollection<ItemStateDto> itemStates;
        public ObservableCollection<ItemStateDto> ItemStates
        {
            get { return itemStates; }
            set
            {
                if (itemStates != value)
                {
                    Set(ref itemStates, value);
                }
            }
        }

        private ObservableCollection<BuildingDto> buildings;
        public ObservableCollection<BuildingDto> Buildings
        {
            get { return buildings; }
            set
            {
                if (buildings != value)
                {
                    Set(ref buildings, value);
                }
            }
        }

        private ObservableCollection<FloorDto> floors;
        public ObservableCollection<FloorDto> Floors
        {
            get { return floors; }
            set
            {
                if (floors != value)
                {
                    Set(ref floors, value);
                }
            }
        }

        public ItemDto Item { get; set; }

        public ItemNatureDto SelectedItemNature
        {
            get { return Item.ItemNature; }
            set
            {
                if (Item.ItemNature != value)
                {
                    Item.ItemNature = value;
                    Item.ItemNatureID = value.Id;
                    RaisePropertyChanged();
                }
            }
        }

        public ItemTypeDto SelectedItemtype
        {
            get { return Item.ItemType; }
            set
            {
                if (Item.ItemType != value)
                {
                    Item.ItemType = value;
                    Item.ItemTypeID = value.Id;
                    RaisePropertyChanged();
                }
            }
        }

        public ItemStateDto SelectedItemState
        {
            get { return Item.ItemState; }
            set
            {
                if (Item.ItemState != value)
                {
                    Item.ItemState = value;
                    Item.ItemState.Id = value.Id;
                    RaisePropertyChanged();
                }
            }
        }

        public BuildingDto SelectedBuilding
        {
            get { return Item.Building; }
            set
            {
                if (Item.Building != value)
                {
                    Item.Building = value;
                    Item.BuildingID = value.Id;
                    RaisePropertyChanged();
                }
            }
        }

        public FloorDto SelectedFloor
        {
            get { return Item.Floor; }
            set
            {
                if (Item.Floor != value)
                {
                    Item.Floor = value;
                    Item.FloorID = value.Id;
                    RaisePropertyChanged();
                }
            }
        }

        private readonly IInventoryApiService _inventoryApiService;
        private readonly ILookupApiService<ItemNatureDto> _itemNatureApiService;
        private readonly ILookupApiService<ItemTypeDto> _itemTypeApiService;
        private readonly ILookupApiService<ItemStateDto> _itemStateApiService;
        //TODO
        //private readonly ILookupApiService<DepartmentDto> _departmentApiService;
        //private readonly ILookupApiService<SectionDto> _sectionApiService;
        private readonly ILookupApiService<BuildingDto> _buildingApiService;
        private readonly ILookupApiService<FloorDto> _floorApiService;

        public ICommand SaveCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }

        public ItemEditPageViewModel(IInventoryApiService inventoryApiService,
                                          ILookupApiService<ItemNatureDto> itemNatureApiService,
                                          ILookupApiService<ItemTypeDto> itemTypeApiService,
                                          ILookupApiService<ItemStateDto> itemStateApiService,
                                          ILookupApiService<BuildingDto> buildingApiService,
                                          ILookupApiService<FloorDto> floorApiService)
        {
            _inventoryApiService = inventoryApiService;
            _itemNatureApiService = itemNatureApiService;
            _itemTypeApiService = itemTypeApiService;
            _itemStateApiService = itemStateApiService;
            _buildingApiService = buildingApiService;
            _floorApiService = floorApiService;

            SaveCommand = new RelayCommand(OnSaveCommand);
            CancelCommand = new RelayCommand(OnCancelCommand);

            Item = new ItemDto();

            Initialization = InitializeAsync();
        }

        protected override async Task InitializeAsync()
        {
            try
            {
                IsLoading = true;

                var itemNatureList = await _itemNatureApiService.GetAvailable();
                if (itemNatureList == null)
                {
                    //TODO: hibakezelés
                    throw new Exception("Hiba az eszköz jellegek lekérése során!");
                }
                ItemNatures = new ObservableCollection<ItemNatureDto>(itemNatureList);

                var itemTypeList = await _itemTypeApiService.GetAvailable();
                if (itemTypeList == null)
                {
                    //TODO: hibakezelés
                    throw new Exception("Hiba az eszköz típusok lekérése során!");
                }
                ItemTypes = new ObservableCollection<ItemTypeDto>(itemTypeList);

                var itemStateList = await _itemStateApiService.GetAvailable();
                if (itemStateList == null)
                {
                    //TODO: hibakezelés
                    throw new Exception("Hiba az eszköz állapotok lekérése során!");
                }
                ItemStates = new ObservableCollection<ItemStateDto>(itemStateList);

                var buildingList = await _buildingApiService.GetAvailable();
                if (buildingList == null)
                {
                    //TODO: hibakezelés
                    throw new Exception("Hiba az épületek lekérése során!");
                }
                Buildings = new ObservableCollection<BuildingDto>(buildingList);

                var floorList = await _floorApiService.GetAvailable();
                if (floorList == null)
                {
                    //TODO: hibakezelés
                    throw new Exception("Hiba az emeletek lekérése során!");
                }
                Floors = new ObservableCollection<FloorDto>(floorList);
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

        private void OnSaveCommand()
        {
            _inventoryApiService.AddNewInventoryItem(Item);
        }

        private void OnCancelCommand()
        {
            //TODO
        }
    }
}
