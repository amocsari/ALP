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
using ALP.ViewModel;
using CommonServiceLocator;
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

            SimpleIoc.Default.Register<MainWindowViewModel>();
        }

        /// <summary>
        /// Registres the pages into the AlpNavigationService's dictionary
        /// </summary>
        public static void SetupNavigation()
        {
            //AlpNavigationService.RegisterPage(SystemSettings, new Uri("../View/System/System_SettingsPage.xaml", UriKind.Relative))
            //AlpNavigationService.RegisterPage(SystemRecentChanges, new Uri("../View/System/System_RecentChangesPage.xaml", UriKind.Relative))
            AlpNavigationService.RegisterPage(SystemWelcomeScreen, new Uri("../View/System/System_WelcomePage.xaml", UriKind.Relative));
            AlpNavigationService.RegisterPage(LookupLocations, new Uri("../View/Lookup/Location/Lookup_LocationsPage.xaml", UriKind.Relative));
            AlpNavigationService.RegisterPage(LookupBuildings, new Uri("../View/Lookup/Building/Lookup_BuildingsPage.xaml", UriKind.Relative));
            AlpNavigationService.RegisterPage(LookupFloors, new Uri("../View/Lookup/Floor/Lookup_FloorsPage.xaml", UriKind.Relative));
            AlpNavigationService.RegisterPage(LookupItemNatures, new Uri("../View/Lookup/ItemNature/Lookup_ItemNaturesPage.xaml", UriKind.Relative));
            AlpNavigationService.RegisterPage(LookupItemStates, new Uri("../View/Lookup/ItemState/Lookup_ItemStatesPage.xaml", UriKind.Relative));
            AlpNavigationService.RegisterPage(LookupItemTypes, new Uri("../View/Lookup/ItemType/Lookup_ItemTypesPage.xaml", UriKind.Relative));
            AlpNavigationService.RegisterPage(LookupSections, new Uri("../View/Lookup/Section/Lookup_SectionsPage.xaml", UriKind.Relative));
            AlpNavigationService.RegisterPage(LookupDepartments, new Uri("../View/Lookup/Department/Lookup_DepartmentsPage.xaml", UriKind.Relative));
            AlpNavigationService.RegisterPage(InventoryItemEditPage, new Uri("../View/Inventory/Inventory_EditPage.xaml", UriKind.Relative));
            AlpNavigationService.RegisterPage(InventoryItemSearchPage, new Uri("../View/Inventory/Inventory_SearchPage.xaml", UriKind.Relative));
            AlpNavigationService.RegisterPage(EmployeeSearchPage, new Uri("../View/Employee/Employee_SearchPage.xaml", UriKind.Relative));
            AlpNavigationService.RegisterPage(EmployeeEditPage, new Uri("../View/Employee/Employee_EditPage.xaml", UriKind.Relative));
        }

        /// <summary>
        /// Performs cleanup operations after the application finishes.
        /// </summary>
        //public static void Cleanup()
        //{
        //    // TODO Clear the ViewModels
        //}
    }
}