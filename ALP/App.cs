using System.Windows;
using ALP.Navigation;
using ALP.Ninject;

namespace ALP
{
    public partial class App: Application
    {
        protected override void OnStartup(StartupEventArgs args)
        {
            IocKernel.Initialize(new IocConfiguration());
            ViewModelLocator.SetupNavigation();
            base.OnStartup(args);
        }
    }
}
