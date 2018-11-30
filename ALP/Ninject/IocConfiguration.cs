using ALP.Service;
using ALP.Navigation;
using ALP.Service.Interface;
using ALP.ViewModel;
using ALP.ViewModel.Inventory;
using Ninject.Modules;
using ALP.ViewModel.Lookup;

namespace ALP.Ninject
{
    /// <summary>
    /// Used to configure the service and viewmodel bindings
    /// Loads the services and viewmodels into the kernel
    /// </summary>
    class IocConfiguration : NinjectModule
    {
        /// <summary>
        /// Configures the viewmodel bindings
        /// Loads the viewmodels into the kernel
        /// </summary>
        private void BindViewModels()
        {
            Bind<MainWindowViewModel>().ToSelf().InTransientScope();
            Bind<WelcomeScreenViewModel>().ToSelf().InTransientScope();
            Bind<SettingsViewModel>().ToSelf().InTransientScope();
            Bind<ChangesViewModel>().ToSelf().InTransientScope();
            Bind<ItemEditPageViewModel>().ToSelf().InTransientScope();

            Bind(typeof(LookupListViewModel<>)).ToSelf().InTransientScope();
        }

        /// <summary>
        /// Configures the service bindings
        /// Loads the services into the kernel
        /// </summary>
        private void BindServices()
        {
            Bind<IAlpNavigationService>().To<AlpNavigationService>().InSingletonScope();
            Bind<IAlpApiService>().To<AlpApiService>().InSingletonScope();
            Bind<IAlpDialogService>().To<AlpDialogService>().InSingletonScope();
            Bind<IAlpResourceService>().To<AlpResourceService>().InSingletonScope();
            Bind<IInventoryApiService>().To<InventoryApiService>().InSingletonScope();
            Bind<IEmployeeApiService>().To<EmployeeApiService>().InSingletonScope();
            
            Bind(typeof(ILookupApiService<>)).To(typeof(LookupApiService<>));
        }

        /// <summary>
        /// Loads the modules into the kernel
        /// </summary>
        public override void Load()
        {
            BindViewModels();
            BindServices();
        }
    }
}
