using GalaSoft.MvvmLight;
using System;
using System.Windows.Input;
using ALP.Navigation;
using GalaSoft.MvvmLight.Views;

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
        public ICommand ItemTypeCommand { get; private set; }
        public ICommand ItemStateCommand { get; private set; }
        public ICommand FloorCommand { get; private set; }
        public ICommand SectionCommand { get; private set; }
        public ICommand BuildingCommand { get; private set; }
        public ICommand ItemSearchCommand { get; private set; }
        public ICommand RequestsCommand { get; private set; }
        public ICommand ImportCommand { get; private set; }
        public ICommand EmployeeSearchCommand { get; private set; }
        public ICommand DepartmentSearchCommand { get; private set; }

        //TODO: moack, ha kész az authentikáció, akkor cserélni
        private bool IsLoggedIn { get => true; }
        //TODO: mock ha a szerepkörök implementálva vannak, akkor cserélni
        private bool IsAdmin { get => true; }

        public MainWindowViewModel(IAlpNavigationService navigationService)
        {
            _navigationService = navigationService;
            LoginCommand = new Command(OnLoginCommand);
            LogoutCommand = new Command(OnLogoutCommand, IsLoggedIn);
            PasswordChangeCommand = new Command(OnPasswordChangeCommand, IsLoggedIn);
            ChangesCommand = new Command(OnChangesCommand);
            SystemSettingsCommand = new Command(OnSystemSettingsCommand, IsLoggedIn && IsAdmin);
            ExitCommand = new Command(OnExitCommand);
            ItemTypeCommand = new Command(OnItemTypeCommand, IsLoggedIn && IsAdmin);
            ItemStateCommand = new Command(OnItemStateCommand, IsLoggedIn && IsAdmin);
            FloorCommand = new Command(OnFloorCommand, IsLoggedIn && IsAdmin);
            SectionCommand = new Command(OnSectionCommand, IsLoggedIn && IsAdmin);
            BuildingCommand = new Command(OnBuildingCommand, IsLoggedIn && IsAdmin);
            ItemSearchCommand = new Command(OnItemSearchCommand, IsLoggedIn);
            RequestsCommand = new Command(OnRequestsCommand, IsLoggedIn && IsAdmin);
            ImportCommand = new Command(OnImportCommand, IsLoggedIn && IsAdmin);
            EmployeeSearchCommand = new Command(OnEmployeeSearchCommand, IsLoggedIn && IsAdmin);
            DepartmentSearchCommand = new Command(OnDepartmentSearchCommand, IsLoggedIn && IsAdmin);
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
            throw new NotImplementedException();
        }

        private void OnBuildingCommand()
        {
            throw new NotImplementedException();
        }

        private void OnSectionCommand()
        {
            throw new NotImplementedException();
        }

        private void OnFloorCommand()
        {
            throw new NotImplementedException();
        }

        private void OnItemStateCommand()
        {
            throw new NotImplementedException();
        }

        private void OnItemTypeCommand()
        {
            throw new NotImplementedException();
        }

        private void OnSystemSettingsCommand()
        {
            _navigationService.NavigateTo(ViewModelLocator.SystemSettings);
        }

        private void OnChangesCommand()
        {
            throw new NotImplementedException();
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
