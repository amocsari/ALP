using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ALP.Service;
using ALP.Navigation;
using ALP.Service;
using ALP.ViewModel;
using ALP.ViewModel.Lookup;
using GalaSoft.MvvmLight.Views;
using Ninject.Modules;
using ALP.ViewModel.Lookup.Building;

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
            Bind<LocationListViewModel>().ToSelf().InTransientScope();
            Bind<BuildingListViewModel>().ToSelf().InTransientScope();
        }

        private void BindServices()
        {
            Bind<IAlpNavigationService>().To<AlpNavigationService>().InSingletonScope();
            Bind<ILocationApiService>().To<LocationApiService>().InSingletonScope();
            Bind<IApiService>().To<ApiService>().InSingletonScope();
            Bind<IAlpDialogService>().To<AlpDialogService>().InSingletonScope();
            Bind<IAlpResourceService>().To<AlpResourceService>().InSingletonScope();
            Bind<IBuildingApiService>().To<BuildingApiService>().InSingletonScope();
        }

        public override void Load()
        {
            BindViewModels();
            BindServices();
        }
    }
}
