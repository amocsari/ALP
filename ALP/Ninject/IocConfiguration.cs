using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ALP.Navigation;
using ALP.ViewModel;
using GalaSoft.MvvmLight.Views;
using Ninject.Modules;

namespace ALP.Ninject
{
    class IocConfiguration : NinjectModule
    {
        public override void Load()
        {
            Bind<IAlpNavigationService>().To<AlpNavigationService>().InSingletonScope();
            Bind<MainWindowViewModel>().ToSelf().InTransientScope();
        }
    }
}
