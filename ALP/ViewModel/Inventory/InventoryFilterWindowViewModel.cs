using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ALP.Service;
using ALP.Service.Interface;
using Common.Model;
using Common.Model.Dto;
using GalaSoft.MvvmLight.CommandWpf;

namespace ALP.ViewModel.Inventory
{
    /// <summary>
    /// Handles the background work of the filter window
    /// </summary>
    public class InventoryFilterWindowViewModel : AlpViewModelBase, IDialogViewModel<InventoryItemFilterInfo, InventoryItemFilterInfo>
    {
        /// <summary>
        /// Filter object that the window received
        /// </summary>
        private InventoryItemFilterInfo parameter;
        public InventoryItemFilterInfo Parameter
        {
            get { return parameter; }
            set
            {
                if (parameter != value)
                {
                    Set(ref parameter, value);
                    Initialization = InitializeAsync();
                }
            }
        }

        /// <summary>
        /// Filter object that the window gives back
        /// </summary>
        public InventoryItemFilterInfo ReturnParameter { get; set; }

        /// <summary>
        /// Lists that contain the selected and the not yet selected items
        /// + References to the currently selected members of the list
        /// </summary>
        private ObservableCollection<BuildingDto> notSelectedBuildings;
        public ObservableCollection<BuildingDto> NotSelectedBuildings
        {
            get { return notSelectedBuildings; }
            set
            {
                if (notSelectedBuildings != value)
                {
                    Set(ref notSelectedBuildings, value);
                }
            }
        }
        private ObservableCollection<BuildingDto> selectedBuildings;
        public ObservableCollection<BuildingDto> SelectedBuildings
        {
            get { return selectedBuildings; }
            set
            {
                if (selectedBuildings != value)
                {
                    Set(ref selectedBuildings, value);
                }
            }
        }
        public BuildingDto SelectedSelectedBuilding { get; set; }
        public BuildingDto SelectedNotSelectedBuilding { get; set; }

        private ObservableCollection<DepartmentDto> notSelectedDepartments;
        public ObservableCollection<DepartmentDto> NotSelectedDepartments
        {
            get { return notSelectedDepartments; }
            set
            {
                if (notSelectedDepartments != value)
                {
                    Set(ref notSelectedDepartments, value);
                }
            }
        }
        private ObservableCollection<DepartmentDto> selectedDepartments;
        public ObservableCollection<DepartmentDto> SelectedDepartments
        {
            get { return selectedDepartments; }
            set
            {
                if (selectedDepartments != value)
                {
                    Set(ref selectedDepartments, value);
                }
            }
        }
        public DepartmentDto SelectedSelectedDepartment { get; set; }
        public DepartmentDto SelectedNotSelectedDepartment { get; set; }

        private ObservableCollection<FloorDto> notSelectedFloors;
        public ObservableCollection<FloorDto> NotSelectedFloors
        {
            get { return notSelectedFloors; }
            set
            {
                if (notSelectedFloors != value)
                {
                    Set(ref notSelectedFloors, value);
                }
            }
        }
        private ObservableCollection<FloorDto> selectedFloors;
        public ObservableCollection<FloorDto> SelectedFloors
        {
            get { return selectedFloors; }
            set
            {
                if (selectedFloors != value)
                {
                    Set(ref selectedFloors, value);
                }
            }
        }
        public FloorDto SelectedSelectedFloor { get; set; }
        public FloorDto SelectedNotSelectedFloor { get; set; }

        private ObservableCollection<ItemNatureDto> notSelectedItemNatures;
        public ObservableCollection<ItemNatureDto> NotSelectedItemNatures
        {
            get { return notSelectedItemNatures; }
            set
            {
                if (notSelectedItemNatures != value)
                {
                    Set(ref notSelectedItemNatures, value);
                }
            }
        }
        private ObservableCollection<ItemNatureDto> selectedItemNatures;
        public ObservableCollection<ItemNatureDto> SelectedItemNatures
        {
            get { return selectedItemNatures; }
            set
            {
                if (selectedItemNatures != value)
                {
                    Set(ref selectedItemNatures, value);
                }
            }
        }
        public ItemNatureDto SelectedSelectedItemNature { get; set; }
        public ItemNatureDto SelectedNotSelectedItemNature { get; set; }

        private ObservableCollection<ItemStateDto> notSelectedItemStates;
        public ObservableCollection<ItemStateDto> NotSelectedItemStates
        {
            get { return notSelectedItemStates; }
            set
            {
                if (notSelectedItemStates != value)
                {
                    Set(ref notSelectedItemStates, value);
                }
            }
        }
        private ObservableCollection<ItemStateDto> selectedItemStates;
        public ObservableCollection<ItemStateDto> SelectedItemStates
        {
            get { return selectedItemStates; }
            set
            {
                if (selectedItemStates != value)
                {
                    Set(ref selectedItemStates, value);
                }
            }
        }
        public ItemStateDto SelectedSelectedItemState { get; set; }
        public ItemStateDto SelectedNotSelectedItemState { get; set; }

        private ObservableCollection<ItemTypeDto> notSelectedItemTypes;
        public ObservableCollection<ItemTypeDto> NotSelectedItemTypes
        {
            get { return notSelectedItemTypes; }
            set
            {
                if (notSelectedItemTypes != value)
                {
                    Set(ref notSelectedItemTypes, value);
                }
            }
        }
        private ObservableCollection<ItemTypeDto> selectedItemTypes;
        public ObservableCollection<ItemTypeDto> SelectedItemTypes
        {
            get { return selectedItemTypes; }
            set
            {
                if (selectedItemTypes != value)
                {
                    Set(ref selectedItemTypes, value);
                }
            }
        }
        public ItemTypeDto SelectedSelectedItemType { get; set; }
        public ItemTypeDto SelectedNotSelectedItemType { get; set; }

        private ObservableCollection<LocationDto> notSelectedLocations;
        public ObservableCollection<LocationDto> NotSelectedLocations
        {
            get { return notSelectedLocations; }
            set
            {
                if (notSelectedLocations != value)
                {
                    Set(ref notSelectedLocations, value);
                }
            }
        }
        private ObservableCollection<LocationDto> selectedLocations;
        public ObservableCollection<LocationDto> SelectedLocations
        {
            get { return selectedLocations; }
            set
            {
                if (selectedLocations != value)
                {
                    Set(ref selectedLocations, value);
                }
            }
        }
        public LocationDto SelectedSelectedLocation { get; set; }
        public LocationDto SelectedNotSelectedLocation { get; set; }

        private ObservableCollection<SectionDto> notSelectedSections;
        public ObservableCollection<SectionDto> NotSelectedSections
        {
            get { return notSelectedSections; }
            set
            {
                if (notSelectedSections != value)
                {
                    Set(ref notSelectedSections, value);
                }
            }
        }
        private ObservableCollection<SectionDto> selectedSections;
        public ObservableCollection<SectionDto> SelectedSections
        {
            get { return selectedSections; }
            set
            {
                if (selectedSections != value)
                {
                    Set(ref selectedSections, value);
                }
            }
        }
        public SectionDto SelectedSelectedSection { get; set; }
        public SectionDto SelectedNotSelectedSection { get; set; }

        /// <summary>
        /// Injected services
        /// </summary>
        private readonly ILookupApiService<BuildingDto> _buildingApiService;
        private readonly ILookupApiService<DepartmentDto> _departmentApiService;
        private readonly ILookupApiService<FloorDto> _floorApiService;
        private readonly ILookupApiService<ItemNatureDto> _itemNatureApiService;
        private readonly ILookupApiService<ItemStateDto> _itemStateApiService;
        private readonly ILookupApiService<ItemTypeDto> _itemTypeApiService;
        private readonly ILookupApiService<LocationDto> _locationApiService;
        private readonly ILookupApiService<SectionDto> _sectionApiService;
        private readonly IAlpLoggingService<InventoryFilterWindowViewModel> _loggingService;
        private readonly IAlpDialogService _dialogService;

        /// <summary>
        /// Command
        /// </summary>
        public ICommand ApplyCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }
        public ICommand SelectedBuildingDoubleClickCommand { get; private set; }
        public ICommand NotSelectedBuildingDoubleClickCommand { get; private set; }
        public ICommand SelectedDepartmentDoubleClickCommand { get; private set; }
        public ICommand NotSelectedDepartmentDoubleClickCommand { get; private set; }
        public ICommand SelectedFloorDoubleClickCommand { get; private set; }
        public ICommand NotSelectedFloorDoubleClickCommand { get; private set; }
        public ICommand SelectedItemNatureDoubleClickCommand { get; private set; }
        public ICommand NotSelectedItemNatureDoubleClickCommand { get; private set; }
        public ICommand SelectedItemStateDoubleClickCommand { get; private set; }
        public ICommand NotSelectedItemStateDoubleClickCommand { get; private set; }
        public ICommand SelectedItemTypeDoubleClickCommand { get; private set; }
        public ICommand NotSelectedItemTypeDoubleClickCommand { get; private set; }
        public ICommand SelectedLocationDoubleClickCommand { get; private set; }
        public ICommand NotSelectedLocationDoubleClickCommand { get; private set; }
        public ICommand SelectedSectionDoubleClickCommand { get; private set; }
        public ICommand NotSelectedSectionDoubleClickCommand { get; private set; }

        public InventoryFilterWindowViewModel(ILookupApiService<BuildingDto> buildingApiService,
            ILookupApiService<DepartmentDto> departmentApiService, ILookupApiService<FloorDto> floorApiService,
            ILookupApiService<ItemNatureDto> itemNatureApiService, ILookupApiService<ItemStateDto> itemStateApiService,
            ILookupApiService<ItemTypeDto> itemTypeApiService, ILookupApiService<LocationDto> locationApiService,
            ILookupApiService<SectionDto> sectionApiService, IAlpLoggingService<InventoryFilterWindowViewModel> loggingService,
            IAlpDialogService dialogService)
        {
            _buildingApiService = buildingApiService;
            _departmentApiService = departmentApiService;
            _floorApiService = floorApiService;
            _itemNatureApiService = itemNatureApiService;
            _itemStateApiService = itemStateApiService;
            _itemTypeApiService = itemTypeApiService;
            _locationApiService = locationApiService;
            _sectionApiService = sectionApiService;
            _dialogService = dialogService;
            _loggingService = loggingService;

            ApplyCommand = new RelayCommand<Window>(OnApplyCommand);
            CancelCommand = new RelayCommand<Window>(OnCancelCommand);
            SelectedBuildingDoubleClickCommand = new RelayCommand(OnSelectedBuildingDoubleClickCommand);
            NotSelectedBuildingDoubleClickCommand = new RelayCommand(OnNotSelectedBuildingDoubleClickCommand);
            SelectedDepartmentDoubleClickCommand = new RelayCommand(OnSelectedDepartmentDoubleClickCommand);
            NotSelectedDepartmentDoubleClickCommand = new RelayCommand(OnNotSelectedDepartmentDoubleClickCommand);
            SelectedFloorDoubleClickCommand = new RelayCommand(OnSelectedFloorDoubleClickCommand);
            NotSelectedFloorDoubleClickCommand = new RelayCommand(OnNotSelectedFloorDoubleClickCommand);
            SelectedItemNatureDoubleClickCommand = new RelayCommand(OnSelectedItemNatureDoubleClickCommand);
            NotSelectedItemNatureDoubleClickCommand = new RelayCommand(OnNotSelectedItemNatureDoubleClickCommand);
            SelectedItemStateDoubleClickCommand = new RelayCommand(OnSelectedItemStateDoubleClickCommand);
            NotSelectedItemStateDoubleClickCommand = new RelayCommand(OnNotSelectedItemStateDoubleClickCommand);
            SelectedItemTypeDoubleClickCommand = new RelayCommand(OnSelectedItemTypeDoubleClickCommand);
            NotSelectedItemTypeDoubleClickCommand = new RelayCommand(OnNotSelectedItemTypeDoubleClickCommand);
            SelectedLocationDoubleClickCommand = new RelayCommand(OnSelectedLocationDoubleClickCommand);
            NotSelectedLocationDoubleClickCommand = new RelayCommand(OnNotSelectedLocationDoubleClickCommand);
            SelectedSectionDoubleClickCommand = new RelayCommand(OnSelectedSectionDoubleClickCommand);
            NotSelectedSectionDoubleClickCommand = new RelayCommand(OnNotSelectedSectionDoubleClickCommand);

            //Initialization = InitializeAsync();
        }

        /// <summary>
        /// Command handlers that move an item from a list to another
        /// </summary>
        private void OnSelectedBuildingDoubleClickCommand()
        {
            var building = SelectedSelectedBuilding;
            if (building == null)
            {
                return;
            }
            SelectedBuildings.Remove(building);
            NotSelectedBuildings.Add(building);
            RaisePropertyChanged(() => SelectedBuildings);
            RaisePropertyChanged(() => NotSelectedBuildings);
        }

        private void OnNotSelectedBuildingDoubleClickCommand()
        {
            var building = SelectedNotSelectedBuilding;
            if (building == null)
            {
                return;
            }
            NotSelectedBuildings.Remove(building);
            SelectedBuildings.Add(building);
            RaisePropertyChanged(() => SelectedBuildings);
            RaisePropertyChanged(() => NotSelectedBuildings);
        }

        private void OnSelectedDepartmentDoubleClickCommand()
        {
            var department = SelectedSelectedDepartment;
            if (department == null)
            {
                return;
            }
            SelectedDepartments.Remove(department);
            NotSelectedDepartments.Add(department);
            RaisePropertyChanged(() => SelectedDepartments);
            RaisePropertyChanged(() => NotSelectedDepartments);
        }

        private void OnNotSelectedDepartmentDoubleClickCommand()
        {
            var department = SelectedNotSelectedDepartment;
            if (department == null)
            {
                return;
            }
            NotSelectedDepartments.Remove(department);
            SelectedDepartments.Add(department);
            RaisePropertyChanged(() => SelectedDepartments);
            RaisePropertyChanged(() => NotSelectedDepartments);
        }

        private void OnSelectedFloorDoubleClickCommand()
        {
            var floor = SelectedSelectedFloor;
            if (floor == null)
            {
                return;
            }
            SelectedFloors.Remove(floor);
            NotSelectedFloors.Add(floor);
            RaisePropertyChanged(() => SelectedFloors);
            RaisePropertyChanged(() => NotSelectedFloors);
        }

        private void OnNotSelectedFloorDoubleClickCommand()
        {
            var floor = SelectedNotSelectedFloor;
            if (floor == null)
            {
                return;
            }
            NotSelectedFloors.Remove(floor);
            SelectedFloors.Add(floor);
            RaisePropertyChanged(() => SelectedFloors);
            RaisePropertyChanged(() => NotSelectedFloors);
        }

        private void OnSelectedItemNatureDoubleClickCommand()
        {
            var itemNature = SelectedSelectedItemNature;
            if (itemNature == null)
            {
                return;
            }
            SelectedItemNatures.Remove(itemNature);
            NotSelectedItemNatures.Add(itemNature);
            RaisePropertyChanged(() => SelectedItemNatures);
            RaisePropertyChanged(() => NotSelectedItemNatures);
        }

        private void OnNotSelectedItemNatureDoubleClickCommand()
        {
            var itemNature = SelectedNotSelectedItemNature;
            if (itemNature == null)
            {
                return;
            }
            NotSelectedItemNatures.Remove(itemNature);
            SelectedItemNatures.Add(itemNature);
            RaisePropertyChanged(() => SelectedItemNatures);
            RaisePropertyChanged(() => NotSelectedItemNatures);
        }

        private void OnSelectedItemTypeDoubleClickCommand()
        {
            var itemType = SelectedSelectedItemType;
            if (itemType == null)
            {
                return;
            }
            SelectedItemTypes.Remove(itemType);
            NotSelectedItemTypes.Add(itemType);
            RaisePropertyChanged(() => SelectedItemTypes);
            RaisePropertyChanged(() => NotSelectedItemTypes);
        }

        private void OnNotSelectedItemTypeDoubleClickCommand()
        {
            var itemType = SelectedNotSelectedItemType;
            if (itemType == null)
            {
                return;
            }
            NotSelectedItemTypes.Remove(itemType);
            SelectedItemTypes.Add(itemType);
            RaisePropertyChanged(() => SelectedItemTypes);
            RaisePropertyChanged(() => NotSelectedItemTypes);
        }

        private void OnSelectedItemStateDoubleClickCommand()
        {
            var itemState = SelectedSelectedItemState;
            if (itemState == null)
            {
                return;
            }
            SelectedItemStates.Remove(itemState);
            NotSelectedItemStates.Add(itemState);
            RaisePropertyChanged(() => SelectedItemStates);
            RaisePropertyChanged(() => NotSelectedItemStates);
        }

        private void OnNotSelectedItemStateDoubleClickCommand()
        {
            var itemState = SelectedNotSelectedItemState;
            if (itemState == null)
            {
                return;
            }
            NotSelectedItemStates.Remove(itemState);
            SelectedItemStates.Add(itemState);
            RaisePropertyChanged(() => SelectedItemStates);
            RaisePropertyChanged(() => NotSelectedItemStates);
        }

        private void OnSelectedLocationDoubleClickCommand()
        {
            var location = SelectedSelectedLocation;
            if (location == null)
            {
                return;
            }
            SelectedLocations.Remove(location);
            NotSelectedLocations.Add(location);
            RaisePropertyChanged(() => SelectedLocations);
            RaisePropertyChanged(() => NotSelectedLocations);
        }

        private void OnNotSelectedLocationDoubleClickCommand()
        {
            var location = SelectedNotSelectedLocation;
            if (location == null)
            {
                return;
            }
            NotSelectedLocations.Remove(location);
            SelectedLocations.Add(location);
            RaisePropertyChanged(() => SelectedLocations);
            RaisePropertyChanged(() => NotSelectedLocations);
        }

        private void OnSelectedSectionDoubleClickCommand()
        {
            var section = SelectedSelectedSection;
            if (section == null)
            {
                return;
            }
            SelectedSections.Remove(section);
            NotSelectedSections.Add(section);
            RaisePropertyChanged(() => SelectedSections);
            RaisePropertyChanged(() => NotSelectedSections);
        }

        private void OnNotSelectedSectionDoubleClickCommand()
        {
            var section = SelectedNotSelectedSection;
            if (section == null)
            {
                return;
            }
            NotSelectedSections.Remove(section);
            SelectedSections.Add(section);
            RaisePropertyChanged(() => SelectedSections);
            RaisePropertyChanged(() => NotSelectedSections);
        }

        /// <summary>
        /// Closes without returning the updated filter
        /// </summary>
        /// <param name="window"></param>
        private void OnCancelCommand(Window window)
        {
            window.DialogResult = false;
            window.Close();
        }

        /// <summary>
        /// sets the returnparameter before closing
        /// </summary>
        /// <param name="window"></param>
        private void OnApplyCommand(Window window)
        {
            ReturnParameter = Parameter;
            ReturnParameter.Buildings = SelectedBuildings.ToList();
            ReturnParameter.Departments = SelectedDepartments.ToList();
            ReturnParameter.ItemStates = SelectedItemStates.ToList();
            ReturnParameter.ItemNatures = SelectedItemNatures.ToList();
            ReturnParameter.ItemTypes = SelectedItemTypes.ToList();
            ReturnParameter.Locations = SelectedLocations.ToList();
            ReturnParameter.Sections = SelectedSections.ToList();
            ReturnParameter.Floors = SelectedFloors.ToList();
            window.DialogResult = true;
            window.Close();
        }

        /// <summary>
        /// Initialization
        /// </summary>
        /// <returns></returns>
        protected override async Task InitializeAsync()
        {
            try
            {
                IsLoading = true;

                if (Parameter != null)
                {
                    SelectedBuildings = new ObservableCollection<BuildingDto>(Parameter.Buildings);
                    SelectedDepartments = new ObservableCollection<DepartmentDto>(Parameter.Departments);
                    SelectedFloors = new ObservableCollection<FloorDto>(Parameter.Floors);
                    SelectedItemNatures = new ObservableCollection<ItemNatureDto>(Parameter.ItemNatures);
                    SelectedItemStates = new ObservableCollection<ItemStateDto>(Parameter.ItemStates);
                    SelectedItemTypes = new ObservableCollection<ItemTypeDto>(Parameter.ItemTypes);
                    SelectedLocations = new ObservableCollection<LocationDto>(Parameter.Locations);
                    SelectedSections = new ObservableCollection<SectionDto>(Parameter.Sections);
                }
                else
                {
                    SelectedBuildings = new ObservableCollection<BuildingDto>();
                    SelectedDepartments = new ObservableCollection<DepartmentDto>();
                    SelectedFloors = new ObservableCollection<FloorDto>();
                    SelectedItemNatures = new ObservableCollection<ItemNatureDto>();
                    SelectedItemStates = new ObservableCollection<ItemStateDto>();
                    SelectedItemTypes = new ObservableCollection<ItemTypeDto>();
                    SelectedLocations = new ObservableCollection<LocationDto>();
                    SelectedSections = new ObservableCollection<SectionDto>();
                }

                var buildingList = await _buildingApiService.GetAll();
                var departmentList = await _departmentApiService.GetAll();
                var floorList = await _floorApiService.GetAll();
                var itemNatureList = await _itemNatureApiService.GetAll();
                var itemStateList = await _itemStateApiService.GetAll();
                var itemTypeList = await _itemTypeApiService.GetAll();
                var locationList = await _locationApiService.GetAll();
                var sectionList = await _sectionApiService.GetAll();

                if (buildingList != null)
                {
                    var buildings = buildingList.Where(building => SelectedBuildings.FirstOrDefault(building2 => building.Id == building2.Id) == null).ToList();
                    NotSelectedBuildings = new ObservableCollection<BuildingDto>(buildings);
                }
                if (departmentList != null)
                {
                    var departments = departmentList.Where(department => SelectedDepartments.FirstOrDefault(department2 => department.Id == department2.Id) == null).ToList();
                    NotSelectedDepartments = new ObservableCollection<DepartmentDto>(departments);
                }
                if (floorList != null)
                {
                    var floors = floorList.Where(floor => !SelectedFloors.Contains(floor)).ToList();
                    NotSelectedFloors = new ObservableCollection<FloorDto>(floors);
                }
                if (itemNatureList != null)
                {
                    var itemNatures = itemNatureList.Where(itemNature => SelectedItemNatures.FirstOrDefault(itemNature2 => itemNature.Id == itemNature2.Id) == null).ToList();
                    NotSelectedItemNatures = new ObservableCollection<ItemNatureDto>(itemNatures);
                }
                if (itemStateList != null)
                {
                    var itemStates = itemStateList.Where(itemState => SelectedItemStates.FirstOrDefault(itemState2 => itemState.Id == itemState2.Id) == null).ToList();
                    NotSelectedItemStates = new ObservableCollection<ItemStateDto>(itemStates);
                }
                if (itemTypeList != null)
                {
                    var itemTypes = itemTypeList.Where(itemType => SelectedItemTypes.FirstOrDefault(itemType2 => itemType.Id == itemType2.Id) == null).ToList();
                    NotSelectedItemTypes = new ObservableCollection<ItemTypeDto>(itemTypes);
                }
                if (locationList != null)
                {
                    var locations = locationList.Where(location => SelectedLocations.FirstOrDefault(location2 => location.Id == location2.Id) == null).ToList();
                    NotSelectedLocations = new ObservableCollection<LocationDto>(locations);
                }
                if (sectionList != null)
                {
                    var sections = sectionList.Where(section => SelectedSections.FirstOrDefault(section2 => section.Id == section2.Id) == null).ToList();
                    NotSelectedSections = new ObservableCollection<SectionDto>(sections);
                }
            }
            catch (Exception e)
            {
                _loggingService.LogFatal("Error during Initalization!", e);
                _dialogService.ShowError(e.Message);
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}
