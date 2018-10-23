using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ALP.API;
using ALP.Navigation;
using ALP.Service;
using ALP.ViewModel;
using ALP.ViewModel.Lookup;
using GalaSoft.MvvmLight.Views;
using Ninject.Modules;

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
            Bind<LocationViewModel>().ToSelf().InTransientScope();
        }

        private void BindServices()
        {
            Bind<IAlpNavigationService>().To<AlpNavigationService>().InSingletonScope();
            Bind<ILocationService>().To<LocationService>().InSingletonScope();
            Bind<IApiService>().To<ApiService>().InSingletonScope();
            Bind<IAlpDialogService>().To<AlpDialogService>().InSingletonScope();
        }

        public override void Load()
        {
            BindViewModels();
            BindServices();
        }
    }
}
