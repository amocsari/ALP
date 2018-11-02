using ALP.Ninject;
using ALP.ViewModel;
using ALP.ViewModel.Lookup;
using Model.Dto;

namespace ALP.Navigation
{
    public partial class ViewModelLocator
    {
        public static readonly string SystemWelcomeScreen = "System_WelcomeScreen";
        public static readonly string SystemSettings = "System_Settings";
        public static readonly string SystemRecentChanges = "System_Changes";
        public static readonly string LookupLocations = "Lookup_Locations";
        public static readonly string LookupBuildings = "Lookup_Buildings";
        public static readonly string LookupFloors = "Lookup_Floors";

        public MainWindowViewModel MainWindowViewModel { get => IocKernel.Get<MainWindowViewModel>(); }
        public WelcomeScreenViewModel WelcomeScreenViewModel { get => IocKernel.Get<WelcomeScreenViewModel>(); }
        public SettingsViewModel SettingsViewModel { get => IocKernel.Get<SettingsViewModel>(); }
        public ChangesViewModel ChangesViewModel { get => IocKernel.Get<ChangesViewModel>(); }
        public LookupListViewModel<LocationDto> LocationListViewModel { get => IocKernel.Get<LookupListViewModel<LocationDto>>(); }
        public LookupEditorWindowViewModel<LocationDto> LocationEditorWindowViewModel { get => IocKernel.Get<LookupEditorWindowViewModel<LocationDto>>(); }
        public LookupListViewModel<BuildingDto> BuildingListViewModel { get => IocKernel.Get<LookupListViewModel<BuildingDto>>(); }
        public LookupBuildingEditorWindowViewModel BuildingEditorWindowViewModel { get => IocKernel.Get<LookupBuildingEditorWindowViewModel>(); }
        public LookupListViewModel<FloorDto> FloorListViewModel { get => IocKernel.Get<LookupListViewModel<FloorDto>>(); }
        public LookupFloorEditorWindowViewModel FloorEditorWindowViewModel { get => IocKernel.Get<LookupFloorEditorWindowViewModel>(); }
    }
}
