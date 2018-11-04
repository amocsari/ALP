using System;
using System.Windows.Input;
using ALP.Navigation;
using GalaSoft.MvvmLight.Command;

namespace ALP.ViewModel
{
    public class MainWindowViewModel
    {
        private readonly IAlpNavigationService _navigationService;

        public ICommand LoginCommand { get; private set; }
        public ICommand LogoutCommand { get; private set; }
        public ICommand PasswordChangeCommand { get; private set; }
        public ICommand ChangesCommand { get; private set; }
        public ICommand SystemSettingsCommand { get; private set; }
        public ICommand ExitCommand { get; private set; }
        public ICommand ItemNatureCommand { get; private set; }
        public ICommand ItemTypeCommand { get; private set; }
        public ICommand ItemStateCommand { get; private set; }
        public ICommand FloorCommand { get; private set; }
        public ICommand SectionCommand { get; private set; }
        public ICommand BuildingCommand { get; private set; }
        public ICommand NewItemCommand { get; private set; }
        public ICommand ItemSearchCommand { get; private set; }
        public ICommand RequestsCommand { get; private set; }
        public ICommand ImportCommand { get; private set; }
        public ICommand EmployeeSearchCommand { get; private set; }
        public ICommand DepartmentSearchCommand { get; private set; }
        public ICommand LocationCommand { get; private set; }

        //TODO: moack, ha kész az authentikáció, akkor cserélni
        private bool IsLoggedIn { get => true; }
        //TODO: mock ha a szerepkörök implementálva vannak, akkor cserélni
        private bool IsAdmin { get => true; }

        public MainWindowViewModel(IAlpNavigationService navigationService)
        {
            _navigationService = navigationService;

            LoginCommand = new RelayCommand(OnLoginCommand);
            LogoutCommand = new RelayCommand(OnLogoutCommand, IsLoggedIn);
            PasswordChangeCommand = new RelayCommand(OnPasswordChangeCommand, IsLoggedIn);
            ChangesCommand = new RelayCommand(OnChangesCommand);
            SystemSettingsCommand = new RelayCommand(OnSystemSettingsCommand, IsLoggedIn && IsAdmin);
            ExitCommand = new RelayCommand(OnExitCommand);
            ItemNatureCommand = new RelayCommand(OnItemNatureCommand);
            ItemTypeCommand = new RelayCommand(OnItemTypeCommand, IsLoggedIn && IsAdmin);
            ItemStateCommand = new RelayCommand(OnItemStateCommand, IsLoggedIn && IsAdmin);
            FloorCommand = new RelayCommand(OnFloorCommand, IsLoggedIn && IsAdmin);
            SectionCommand = new RelayCommand(OnSectionCommand, IsLoggedIn && IsAdmin);
            BuildingCommand = new RelayCommand(OnBuildingCommand, IsLoggedIn && IsAdmin);
            NewItemCommand = new RelayCommand(OnNewItemCommand, IsLoggedIn && IsAdmin);
            ItemSearchCommand = new RelayCommand(OnItemSearchCommand, IsLoggedIn);
            RequestsCommand = new RelayCommand(OnRequestsCommand, IsLoggedIn && IsAdmin);
            ImportCommand = new RelayCommand(OnImportCommand, IsLoggedIn && IsAdmin);
            EmployeeSearchCommand = new RelayCommand(OnEmployeeSearchCommand, IsLoggedIn && IsAdmin);
            DepartmentSearchCommand = new RelayCommand(OnDepartmentSearchCommand, IsLoggedIn && IsAdmin);
            LocationCommand = new RelayCommand(OnLocationCommand, IsLoggedIn && IsAdmin);
        }

        private void OnNewItemCommand()
        {
            _navigationService.NavigateTo(ViewModelLocator.InventoryItemEditPage);
        }

        private void OnLocationCommand()
        {
            _navigationService.NavigateTo(ViewModelLocator.LookupLocations);
        }

        private void OnExitCommand()
        {
            throw new NotImplementedException();
        }

        private void OnDepartmentSearchCommand()
        {
            throw new NotImplementedException();
        }

        private void OnEmployeeSearchCommand()
        {
            throw new NotImplementedException();
        }

        private void OnImportCommand()
        {
            throw new NotImplementedException();
        }

        private void OnRequestsCommand()
        {
            throw new NotImplementedException();
        }

        private void OnItemSearchCommand()
        {
            _navigationService.NavigateTo(ViewModelLocator.InventoryItemSearchPage);
        }

        private void OnBuildingCommand()
        {
            _navigationService.NavigateTo(ViewModelLocator.LookupBuildings);
        }

        private void OnSectionCommand()
        {
            throw new NotImplementedException();
        }

        private void OnFloorCommand()
        {
            _navigationService.NavigateTo(ViewModelLocator.LookupFloors);
        }

        private void OnItemNatureCommand()
        {
            _navigationService.NavigateTo(ViewModelLocator.LookupItemNatures);
        }

        private void OnItemStateCommand()
        {
            _navigationService.NavigateTo(ViewModelLocator.LookupItemStates);
        }

        private void OnItemTypeCommand()
        {
            _navigationService.NavigateTo(ViewModelLocator.LookupItemTypes);
        }

        private void OnSystemSettingsCommand()
        {
            _navigationService.NavigateTo(ViewModelLocator.SystemSettings);
        }

        private void OnChangesCommand()
        {
            _navigationService.NavigateTo(ViewModelLocator.SystemRecentChanges);
        }

        private void OnPasswordChangeCommand()
        {
            throw new NotImplementedException();
        }

        private void OnLogoutCommand()
        {
            throw new NotImplementedException();
        }

        private void OnLoginCommand()
        {
            throw new NotImplementedException();
        }
    }
}
