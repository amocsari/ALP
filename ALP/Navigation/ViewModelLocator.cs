/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:ALP"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using System;
using ALP.Navigation;
using ALP.View;
using ALP.ViewModel;
using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;

namespace ALP.Navigation
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public partial class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
            ////}
            ////else
            ////{
            ////    // Create run time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DataService>();
            ////}

            SimpleIoc.Default.Register<MainWindowViewModel>();
        }

        public MainWindowViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainWindowViewModel>();
            }
        }

        public static void SetupNavigation()
        {
            var navigationService = new AlpNavigationService()
                .RegisterPage(SystemSettings, new Uri("../View/System/System_SettingsPage.xaml", UriKind.Relative))
                .RegisterPage(SystemRecentChanges, new Uri("../View/System/System_RecentChangesPage.xaml", UriKind.Relative))
                .RegisterPage(LookupLocations, new Uri("../View/Lookup/Location/Lookup_LocationsPage.xaml", UriKind.Relative))
                .RegisterPage(LookupBuildings, new Uri("../View/Lookup/Building/Lookup_BuildingsPage.xaml", UriKind.Relative));
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}