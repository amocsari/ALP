using ALP.Service;
using ALP.Navigation;
using ALP.ViewModel;
using Ninject.Modules;
using ALP.ViewModel.Lookup;

namespace ALP.Ninject
{
    class IocConfiguration : NinjectModule
    {
        private void BindViewModels()
        {
            Bind<MainWindowViewModel>().ToSelf().InTransientScope();
            Bind<WelcomeScreenViewModel>().ToSelf().InTransientScope();
            Bind<SettingsViewModel>().ToSelf().InTransientScope();
            Bind<ChangesViewModel>().ToSelf().InTransientScope();

            Bind(typeof(LookupListViewModel<>)).ToSelf().InTransientScope();
        }

        private void BindServices()
        {
            Bind<IAlpNavigationService>().To<AlpNavigationService>().InSingletonScope();
            Bind<IApiService>().To<ApiService>().InSingletonScope();
            Bind<IAlpDialogService>().To<AlpDialogService>().InSingletonScope();
            Bind<IAlpResourceService>().To<AlpResourceService>().InSingletonScope();
            
            Bind(typeof(ILookupApiService<>)).To(typeof(LookupApiService<>));
        }

        public override void Load()
        {
            BindViewModels();
            BindServices();
        }
    }
}
