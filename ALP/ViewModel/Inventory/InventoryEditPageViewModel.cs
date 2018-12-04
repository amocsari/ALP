using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using ALP.Navigation;
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

        private ObservableCollection<DepartmentDto> departments;
        public ObservableCollection<DepartmentDto> Departments
        {
            get { return departments; }
            set
            {
                if (departments != value)
                {
                    Set(ref departments, value);
                }
            }
        }

        private ObservableCollection<SectionDto> sections;
        public ObservableCollection<SectionDto> Sections
        {
            get { return sections; }
            set
            {
                if (sections != value)
                {
                    Set(ref sections, value);
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

        public ItemTypeDto SelectedItemType
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
                    Item.ItemStateID = value.Id;
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

        public DepartmentDto SelectedDepartment
        {
            get { return Item.Department; }
            set
            {
                if (Item.Department != value)
                {
                    Item.Department = value;
                    Item.DepartmentID = value.Id;
                    RaisePropertyChanged();
                }
            }
        }

        public SectionDto SelectedSection
        {
            get { return Item.Section; }
            set
            {
                if (Item.Section != value)
                {
                    Item.Section = value;
                    Item.SectionID = value.Id;
                    RaisePropertyChanged();
                }
            }
        }

        public string EmployeeName { get; set; }

        private readonly IInventoryApiService _inventoryApiService;
        private readonly IEmployeeApiService _employeeApiService;
        private readonly IAlpNavigationService _navigationService;
        private readonly IAlpDialogService _dialogService;
        private readonly IAlpLoggingService<ItemEditPageViewModel> _loggingService;
        private readonly ILookupApiService<ItemNatureDto> _itemNatureApiService;
        private readonly ILookupApiService<ItemTypeDto> _itemTypeApiService;
        private readonly ILookupApiService<ItemStateDto> _itemStateApiService;
        private readonly ILookupApiService<DepartmentDto> _departmentApiService;
        private readonly ILookupApiService<SectionDto> _sectionApiService;
        private readonly ILookupApiService<BuildingDto> _buildingApiService;
        private readonly ILookupApiService<FloorDto> _floorApiService;

        public ICommand SaveCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }

        public ItemEditPageViewModel(IInventoryApiService inventoryApiService,
                                          IEmployeeApiService employeeApiService,
                                          IAlpNavigationService navigationService,
                                          IAlpDialogService dialogService,
                                          IAlpLoggingService<ItemEditPageViewModel> loggingService,
                                          ILookupApiService<ItemNatureDto> itemNatureApiService,
                                          ILookupApiService<ItemTypeDto> itemTypeApiService,
                                          ILookupApiService<ItemStateDto> itemStateApiService,
                                          ILookupApiService<BuildingDto> buildingApiService,
                                          ILookupApiService<FloorDto> floorApiService,
                                          ILookupApiService<DepartmentDto> departmentApiService,
                                          ILookupApiService<SectionDto> sectionApiService)
        {
            _inventoryApiService = inventoryApiService;
            _employeeApiService = employeeApiService;
            _navigationService = navigationService;
            _itemNatureApiService = itemNatureApiService;
            _itemTypeApiService = itemTypeApiService;
            _itemStateApiService = itemStateApiService;
            _buildingApiService = buildingApiService;
            _floorApiService = floorApiService;
            _departmentApiService = departmentApiService;
            _sectionApiService = sectionApiService;
            _loggingService = loggingService;
            _dialogService = dialogService;

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
                    throw new Exception("Hiba az eszköz jellegek lekérése során!");
                }
                ItemNatures = new ObservableCollection<ItemNatureDto>(itemNatureList);

                var itemTypeList = await _itemTypeApiService.GetAvailable();
                if (itemTypeList == null)
                {
                    throw new Exception("Hiba az eszköz típusok lekérése során!");
                }
                ItemTypes = new ObservableCollection<ItemTypeDto>(itemTypeList);

                var itemStateList = await _itemStateApiService.GetAvailable();
                if (itemStateList == null)
                {
                    throw new Exception("Hiba az eszköz állapotok lekérése során!");
                }
                ItemStates = new ObservableCollection<ItemStateDto>(itemStateList);

                var buildingList = await _buildingApiService.GetAvailable();
                if (buildingList == null)
                {
                    throw new Exception("Hiba az épületek lekérése során!");
                }
                Buildings = new ObservableCollection<BuildingDto>(buildingList);

                var floorList = await _floorApiService.GetAvailable();
                if (floorList == null)
                {
                    throw new Exception("Hiba az emeletek lekérése során!");
                }
                Floors = new ObservableCollection<FloorDto>(floorList);

                var departmentList = await _departmentApiService.GetAvailable();
                if (departmentList == null)
                {
                    throw new Exception("Hiba az emeletek lekérése során!");
                }
                Departments = new ObservableCollection<DepartmentDto>(departmentList);

                var sectionList = await _sectionApiService.GetAvailable();
                if (sectionList == null)
                {
                    throw new Exception("Hiba az emeletek lekérése során!");
                }
                Sections = new ObservableCollection<SectionDto>(sectionList);
            }
            catch (Exception e)
            {
                _loggingService.LogError("Error during Initialization", e);
                _dialogService.ShowError(e.Message);
            }
            finally
            {
                IsLoading = false;
            }
        }

        private async void OnSaveCommand()
        {
            var employee = await _employeeApiService.GetEmployeeByName(EmployeeName);
            if (employee != null)
            {
                //TODO: feldobni ablakot
                Item.Employee = employee;
                Item.EmployeeID = employee.Id;
            }
            await _inventoryApiService.AddNewInventoryItem(Item);
            _navigationService.NavigateTo(ViewModelLocator.InventoryItemSearchPage);
        }

        private void OnCancelCommand()
        {
            _navigationService.GoBack();
        }
    }
}
