﻿using System;
using System.Windows.Input;
using ALP.Navigation;
using GalaSoft.MvvmLight.Command;
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

            LoginCommand = new RelayCommand(OnLoginCommand);
            LogoutCommand = new RelayCommand(OnLogoutCommand, IsLoggedIn);
            PasswordChangeCommand = new RelayCommand(OnPasswordChangeCommand, IsLoggedIn);
            ChangesCommand = new RelayCommand(OnChangesCommand);
            SystemSettingsCommand = new RelayCommand(OnSystemSettingsCommand, IsLoggedIn && IsAdmin);
            ExitCommand = new RelayCommand(OnExitCommand);
            ItemTypeCommand = new RelayCommand(OnItemTypeCommand, IsLoggedIn && IsAdmin);
            ItemStateCommand = new RelayCommand(OnItemStateCommand, IsLoggedIn && IsAdmin);
            FloorCommand = new RelayCommand(OnFloorCommand, IsLoggedIn && IsAdmin);
            SectionCommand = new RelayCommand(OnSectionCommand, IsLoggedIn && IsAdmin);
            BuildingCommand = new RelayCommand(OnBuildingCommand, IsLoggedIn && IsAdmin);
            ItemSearchCommand = new RelayCommand(OnItemSearchCommand, IsLoggedIn);
            RequestsCommand = new RelayCommand(OnRequestsCommand, IsLoggedIn && IsAdmin);
            ImportCommand = new RelayCommand(OnImportCommand, IsLoggedIn && IsAdmin);
            EmployeeSearchCommand = new RelayCommand(OnEmployeeSearchCommand, IsLoggedIn && IsAdmin);
            DepartmentSearchCommand = new RelayCommand(OnDepartmentSearchCommand, IsLoggedIn && IsAdmin);
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
