using ALP.Service;
using ALP.Navigation;
using ALP.Service.Interface;
using ALP.ViewModel;
using ALP.ViewModel.Inventory;
using Ninject.Modules;
using ALP.ViewModel.Lookup;
using log4net;

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
            Bind<IInventoryApiService>().To<InventoryApiService>().InSingletonScope();
            Bind<IEmployeeApiService>().To<EmployeeApiService>().InSingletonScope();
            Bind<IOperationService>().To<OperationService>().InSingletonScope();
            Bind<IImportService>().To<ImportService>().InSingletonScope();
            Bind<IAccountApiService>().To<AccountApiService>().InSingletonScope();
            Bind<IExportService>().To<ExportService>().InSingletonScope();
        }

        /// <summary>
        /// Configures the generic service bindings
        /// Loads the services into the kernel
        /// </summary>
        private void BindGenericServices()
        {
            Bind(typeof(ILookupApiService<>)).To(typeof(LookupApiService<>)).InSingletonScope();
            Bind(typeof(IAlpLoggingService<>)).To(typeof(AlpLoggingService<>)).InSingletonScope();
        }

        /// <summary>
        /// Loads the modules into the kernel
        /// </summary>
        public override void Load()
        {
            BindViewModels();
            BindServices();
            BindGenericServices();
        }
    }
}
