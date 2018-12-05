using ALP.Ninject;
using ALP.ViewModel;
using ALP.ViewModel.Employee;
using ALP.ViewModel.Inventory;
using ALP.ViewModel.Lookup;
using Common.Model.Dto;

namespace ALP.Navigation
{
    public partial class ViewModelLocator
    {
        //Key values of the pages
        public static readonly string SystemWelcomeScreen = "System_WelcomeScreen";
        public static readonly string SystemSettings = "System_Settings";
        public static readonly string SystemRecentChanges = "System_Changes";
        public static readonly string LookupLocations = "Lookup_Locations";
        public static readonly string LookupBuildings = "Lookup_Buildings";
        public static readonly string LookupFloors = "Lookup_Floors";
        public static readonly string LookupItemNatures = "Lookup_ItemNatures";
        public static readonly string LookupItemStates = "Lookup_ItemStates";
        public static readonly string LookupItemTypes = "Lookup_ItemTypes";
        public static readonly string LookupSections = "Lookup_Sections";
        public static readonly string LookupDepartments = "Lookup_Departments";
        public static readonly string InventoryItemEditPage = "Inventory_ItemEdit";
        public static readonly string InventoryItemSearchPage = "Inventory_ItemSearch";
        public static readonly string EmployeeSearchPage = "Employee_SearchPage";
        public static readonly string EmployeeEditPage = "Employee_EditPage";


        //ViewModels bound to view DataContexts in XAML code
        public MainWindowViewModel MainWindowViewModel { get => IocKernel.Get<MainWindowViewModel>(); }

        public WelcomeScreenViewModel WelcomeScreenViewModel { get => IocKernel.Get<WelcomeScreenViewModel>(); }

        public SettingsViewModel SettingsViewModel { get => IocKernel.Get<SettingsViewModel>(); }

        public ChangesViewModel ChangesViewModel { get => IocKernel.Get<ChangesViewModel>(); }

        public ItemEditPageViewModel InventoryEditPageViewModel { get => IocKernel.Get<ItemEditPageViewModel>(); }
        public InventoryOperationWindowViewModel InventoryOperationWindowViewModel { get => IocKernel.Get<InventoryOperationWindowViewModel>(); }

        public InventorySearchPageViewModel InventorySearchPageViewModel { get => IocKernel.Get<InventorySearchPageViewModel>(); }

        public EmployeeSearchPageViewModel EmployeeSearchPageViewModel { get => IocKernel.Get<EmployeeSearchPageViewModel>(); }
        public EmployeeEditPageViewModel EmployeeEditPageViewModel { get => IocKernel.Get<EmployeeEditPageViewModel>(); }

        public LookupListViewModel<LocationDto> LocationListViewModel { get => IocKernel.Get<LookupListViewModel<LocationDto>>(); }
        public LookupEditorWindowViewModel<LocationDto> LocationEditorWindowViewModel { get => IocKernel.Get<LookupEditorWindowViewModel<LocationDto>>(); }

        public LookupListViewModel<BuildingDto> BuildingListViewModel { get => IocKernel.Get<LookupListViewModel<BuildingDto>>(); }
        public LookupBuildingEditorWindowViewModel BuildingEditorWindowViewModel { get => IocKernel.Get<LookupBuildingEditorWindowViewModel>(); }

        public LookupListViewModel<FloorDto> FloorListViewModel { get => IocKernel.Get<LookupListViewModel<FloorDto>>(); }
        public LookupFloorEditorWindowViewModel FloorEditorWindowViewModel { get => IocKernel.Get<LookupFloorEditorWindowViewModel>(); }

        public LookupListViewModel<ItemNatureDto> ItemNatureListViewModel { get => IocKernel.Get<LookupListViewModel<ItemNatureDto>>(); }
        public LookupEditorWindowViewModel<ItemNatureDto> ItemNatureEditorWindowViewModel { get => IocKernel.Get<LookupEditorWindowViewModel<ItemNatureDto>>(); }

        public LookupListViewModel<ItemStateDto> ItemStateListViewModel { get => IocKernel.Get<LookupListViewModel<ItemStateDto>>(); }
        public LookupEditorWindowViewModel<ItemStateDto> ItemStateEditorWindowViewModel { get => IocKernel.Get<LookupEditorWindowViewModel<ItemStateDto>>(); }

        public LookupListViewModel<ItemTypeDto> ItemTypeListViewModel { get => IocKernel.Get<LookupListViewModel<ItemTypeDto>>(); }
        public LookupItemTypeEditorWindowViewModel ItemTypeEditorWindowViewModel { get => IocKernel.Get<LookupItemTypeEditorWindowViewModel>(); }

        public LookupListViewModel<SectionDto> SectionListViewModel { get => IocKernel.Get<LookupListViewModel<SectionDto>>(); }
        public LookupSectionEditorWindowViewModel SectionEditorWindowViewModel { get => IocKernel.Get<LookupSectionEditorWindowViewModel>(); }

        public LookupListViewModel<DepartmentDto> DepartmentListViewModel { get => IocKernel.Get<LookupListViewModel<DepartmentDto>>(); }
        public LookupDepartmentEditorWindowViewModel DepartmentEditorWindowViewModel { get => IocKernel.Get<LookupDepartmentEditorWindowViewModel>(); }
    }
}
