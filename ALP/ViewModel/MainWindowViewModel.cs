using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ALP.Navigation;
using ALP.Service;
using ALP.Service.Interface;
using ALP.View;
using GalaSoft.MvvmLight.Command;
using Model.Enum;
using Model.Model;

namespace ALP.ViewModel
{
    /// <summary>
    /// Used to react to the events of the MainWindow
    /// </summary>
    public class MainWindowViewModel : AlpViewModelBase
    {
        /// <summary>
        /// injected services
        /// </summary>
        private readonly IAlpNavigationService _navigationService;
        private readonly IAlpDialogService _dialogService;
        private readonly IImportService _importService;
        private readonly IInventoryApiService _inventoryApiService;
        private readonly IAccountApiService _accountApiService;

        //Commands bound to the menu items
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

        /// <summary>
        /// Data containing the current session
        /// </summary>
        public static string SessionToken { get; set; }
        public static int? RoleId { get; set; }
        private SessionData sessionData;
        public SessionData SessionData
        {
            get { return sessionData; }
            set
            {
                if (sessionData != value)
                {
                    Set(ref sessionData, value);
                    RaisePropertyChanged(() => IsLoggedIn);
                    RaisePropertyChanged(() => IsLoggedOut);
                    RaisePropertyChanged(() => IsAdmin);
                    SessionToken = SessionData?.Token;
                    RoleId = SessionData?.RoleId;
                }
            }
        }
        public bool IsLoggedIn { get => !string.IsNullOrEmpty(SessionData?.Token); }
        public bool IsLoggedOut { get => !IsLoggedIn; }
        public bool IsAdmin { get => SessionData?.RoleId == (int)RoleType.Admin; }

        /// <summary>
        /// Constructor
        /// Sets the injected service
        /// Sets the commands
        /// </summary>
        /// <param name="navigationService">injected navigationservice</param>
        public MainWindowViewModel(IAlpNavigationService navigationService, IAlpDialogService dialogService, IImportService importService, IInventoryApiService inventoryApiService, IAccountApiService accountApiService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;
            _importService = importService;
            _inventoryApiService = inventoryApiService;
            _accountApiService = accountApiService;

            LoginCommand = new RelayCommand(OnLoginCommand);
            LogoutCommand = new RelayCommand(OnLogoutCommand);
            PasswordChangeCommand = new RelayCommand(OnPasswordChangeCommand);
            ChangesCommand = new RelayCommand(OnChangesCommand);
            SystemSettingsCommand = new RelayCommand(OnSystemSettingsCommand);
            ExitCommand = new RelayCommand<Window>(OnExitCommand);
            ItemNatureCommand = new RelayCommand(OnItemNatureCommand);
            ItemTypeCommand = new RelayCommand(OnItemTypeCommand);
            ItemStateCommand = new RelayCommand(OnItemStateCommand);
            FloorCommand = new RelayCommand(OnFloorCommand);
            SectionCommand = new RelayCommand(OnSectionCommand);
            BuildingCommand = new RelayCommand(OnBuildingCommand);
            NewItemCommand = new RelayCommand(OnNewItemCommand);
            ItemSearchCommand = new RelayCommand(OnItemSearchCommand);
            RequestsCommand = new RelayCommand(OnRequestsCommand);
            ImportCommand = new RelayCommand(OnImportCommand);
            EmployeeSearchCommand = new RelayCommand(OnEmployeeSearchCommand);
            DepartmentSearchCommand = new RelayCommand(OnDepartmentSearchCommand);
            LocationCommand = new RelayCommand(OnLocationCommand);
        }

        /// <summary>
        /// Initializes data
        /// </summary>
        protected override async Task InitializeAsync() { }

        //Command functions
        private void OnNewItemCommand()
        {
            _navigationService.NavigateTo(ViewModelLocator.InventoryItemEditPage);
        }

        private void OnLocationCommand()
        {
            _navigationService.NavigateTo(ViewModelLocator.LookupLocations);
        }

        private async void OnExitCommand(Window window)
        {
            var result = _dialogService.ShowConfirmDialog("Biztosan kilép az alkalmazásból?", "Kilépés");
            if (result)
            {
                try
                {
                    IsLoading = true;
                    if (SessionData?.Token != null)
                    {
                        await _accountApiService.Logout(SessionData.Token);
                    }
                    window.Close();
                }
                catch (Exception e)
                {
                    _dialogService.ShowError(e.Message);
                }
                finally
                {
                    IsLoading = false;
                }
            }
        }

        private void OnDepartmentSearchCommand()
        {
            _navigationService.NavigateTo(ViewModelLocator.LookupDepartments);
        }

        private void OnEmployeeSearchCommand()
        {
            _navigationService.NavigateTo(ViewModelLocator.EmployeeSearchPage);
        }

        private async void OnImportCommand()
        {
            try
            {
                IsLoading = true;
                var importedRows = _importService.ImportFromXls();
                if (importedRows != null)
                {
                    var confirmResult =
                        _dialogService.ShowConfirmDialog(
                            $"{importedRows.Count} rekord importálása lehetséges!\nImportálja?", "Importálás");
                    if (confirmResult)
                    {
                        var result = await _inventoryApiService.ImportItems(importedRows);
                        _navigationService.NavigateTo(ViewModelLocator.InventoryItemSearchPage, result);
                    }
                }
            }
            catch (Exception e)
            {
                _dialogService.ShowError(e.Message);
            }
            finally
            {
                IsLoading = false;
            }
        }

        private void OnRequestsCommand()
        {
            //TODO
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
            _navigationService.NavigateTo(ViewModelLocator.LookupSections);
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
            try
            {
                IsLoading = true;
                var result = _dialogService.ShowDialog<PasswordChangeWindow, PasswordChangeWindowViewModel, object, object>(null);
                if (result != null && result.Accepted)
                {
                    _dialogService.ShowAlert("Sikeres jelszómegváltoztatás", "Jelszómegváltoztatás");
                }
            }
            catch (Exception e)
            {
                _dialogService.ShowError(e.Message);
            }
            finally
            {
                IsLoading = false;
            }
        }

        private async void OnLogoutCommand()
        {
            try
            {
                IsLoading = true;
                await _accountApiService.Logout(SessionData.Token);
                SessionData = null;
                _navigationService.NavigateTo(ViewModelLocator.SystemWelcomeScreen);
                _dialogService.ShowAlert("Sikeres kijelentkezés", "Kijelentkezés");
            }
            catch (Exception e)
            {
                _dialogService.ShowError(e.Message);
            }
            finally
            {
                IsLoading = false;
            }
        }

        private void OnLoginCommand()
        {
            try
            {
                IsLoading = true;
                var result = _dialogService.ShowDialog<LoginWindow, LoginWindowViewModel, object, SessionData>(null);
                if (result != null && result.Accepted)
                {
                    SessionData = result.Value;
                }
            }
            catch (Exception e)
            {
                _dialogService.ShowError(e.Message);
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}
