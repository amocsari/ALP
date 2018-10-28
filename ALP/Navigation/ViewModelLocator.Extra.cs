using ALP.Ninject;
using ALP.ViewModel;
using ALP.ViewModel.Lookup;
using ALP.ViewModel.Lookup.Building;
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

        public MainWindowViewModel MainWindowViewModel { get => IocKernel.Get<MainWindowViewModel>(); }
        public WelcomeScreenViewModel WelcomeScreenViewModel { get => IocKernel.Get<WelcomeScreenViewModel>(); }
        public SettingsViewModel SettingsViewModel { get => IocKernel.Get<SettingsViewModel>(); }
        public ChangesViewModel ChangesViewModel { get => IocKernel.Get<ChangesViewModel>(); }
        public LocationListViewModel LocationListViewModel { get => IocKernel.Get<LocationListViewModel>(); }
        public LookupEditorWindowViewModel<LocationDto> LocationEditorWindowViewModel { get => IocKernel.Get<LookupEditorWindowViewModel<LocationDto>>(); }
        public BuildingListViewModel BuildingListViewModel { get => IocKernel.Get<BuildingListViewModel>(); }
    }
}
