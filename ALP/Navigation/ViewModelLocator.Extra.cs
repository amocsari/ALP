using ALP.Ninject;
using ALP.ViewModel;

namespace ALP.Navigation
{
    public partial class ViewModelLocator
    {
        public const string SystemWelcomeScreen = "System_WelcomeScreen";
        public const string SystemSettings = "System_Settings";
        public const string SystemRecentChanges = "System_Changes";

        public MainWindowViewModel MainWindowViewModel { get => IocKernel.Get<MainWindowViewModel>(); }
        public WelcomeScreenViewModel WelcomeScreenViewModel { get => IocKernel.Get<WelcomeScreenViewModel>(); }
        public SettingsViewModel SettingsViewModel { get => IocKernel.Get<SettingsViewModel>(); }
        public ChangesViewModel ChangesViewModel { get => IocKernel.Get<ChangesViewModel>(); }
    }
}
