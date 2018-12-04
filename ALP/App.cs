using System.Windows;
using ALP.Navigation;
using ALP.Ninject;
using log4net;

namespace ALP
{
    public partial class App: Application
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(App));
        protected override void OnStartup(StartupEventArgs args)
        {
            log4net.Config.XmlConfigurator.Configure();
            IocKernel.Initialize(new IocConfiguration());
            ViewModelLocator.SetupNavigation();
            base.OnStartup(args);
        }
    }
}
