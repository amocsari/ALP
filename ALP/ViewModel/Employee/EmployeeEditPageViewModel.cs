using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using ALP.Navigation;
using ALP.Service;
using ALP.Service.Interface;
using ALP.View.Employee;
using Common.Model.Dto;
using GalaSoft.MvvmLight.CommandWpf;

namespace ALP.ViewModel.Employee
{
    /// <summary>
    /// Handles the data os a single Employee
    /// </summary>
    public class EmployeeEditPageViewModel : AlpViewModelBase
    {
        /// <summary>
        /// the employee
        /// </summary>
        private EmployeeListItemViewModel employee;
        public EmployeeListItemViewModel Employee
        {
            get { return employee; }
            set
            {
                if (employee != value)
                {
                    Set(ref employee, value);
                }
            }
        }

        /// <summary>
        /// departments to chose from in the combobox
        /// </summary>
        private ObservableCollection<DepartmentDto> departments;
        public ObservableCollection<DepartmentDto> Departments
        {
            get { return departments; }
            set
            {
                if (departments != value)
                {
                    Set(ref departments, value);
                }
            }
        }

        /// <summary>
        /// sections to chose from in the combobox
        /// </summary>
        private ObservableCollection<SectionDto> sections;
        public ObservableCollection<SectionDto> Sections
        {
            get { return sections; }
            set
            {
                if (sections != value)
                {
                    Set(ref sections, value);
                }
            }
        }

        /// <summary>
        /// The selected department in the combobox
        /// </summary>
        public DepartmentDto SelectedDepartment
        {
            get { return Employee.Value.Department; }
            set
            {
                if (Employee.Value.Department == null || Employee.Value.Department != value)
                {
                    Employee.Value.Department = value;
                    Employee.Value.DepartmentID = value.Id;
                    RaisePropertyChanged(() => Employee);
                }
            }
        }

        /// <summary>
        /// The selected section in the combobox
        /// </summary>
        public SectionDto SelectedSection
        {
            get { return Employee.Value.Section; }
            set
            {
                if (Employee.Value.Section == null || Employee.Value.Section != value)
                {
                    Employee.Value.Section = value;
                    Employee.Value.SectionID = value.Id;
                    RaisePropertyChanged(() => Employee);
                }
            }
        }

        /// <summary>
        /// Commands
        /// </summary>
        public ICommand SaveCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }
        public ICommand ListItemsCommand { get; private set; }
        public ICommand CreateAccountCommand { get; private set; }
        public ICommand RetireCommand { get; private set; }

        /// <summary>
        /// Injected services
        /// </summary>
        private readonly ILookupApiService<DepartmentDto> _departmentApiService;
        private readonly ILookupApiService<SectionDto> _sectionApiService;
        private readonly IAlpNavigationService _navigationService;
        private readonly IEmployeeApiService _employeeApiService;
        private readonly IAlpLoggingService<EmployeeEditPageViewModel> _loggingService;
        private readonly IAlpDialogService _dialogService;
        private readonly IInventoryApiService _inventoryApiService;


        public EmployeeEditPageViewModel(ILookupApiService<DepartmentDto> departmentApiService, ILookupApiService<SectionDto> sectionApiService, IAlpNavigationService navigationService, IEmployeeApiService employeeApiService, IAlpLoggingService<EmployeeEditPageViewModel> loggingService, IAlpDialogService dialogService, IInventoryApiService inventoryApiService)
        {
            Employee = new EmployeeListItemViewModel(new EmployeeDto());

            _departmentApiService = departmentApiService;
            _sectionApiService = sectionApiService;
            _navigationService = navigationService;
            _employeeApiService = employeeApiService;
            _loggingService = loggingService;
            _dialogService = dialogService;
            _inventoryApiService = inventoryApiService;

            SaveCommand = new RelayCommand(OnSaveCommand);
            CancelCommand = new RelayCommand(OnCancelCommand);
            ListItemsCommand = new RelayCommand(OnListItemsCommand);
            RetireCommand = new RelayCommand(OnRetireCommand);
            CreateAccountCommand = new RelayCommand(OnCreateAccountCommand);

            Initialization = InitializeAsync();
        }

        /// <summary>
        /// Creates an account for the selected employee
        /// </summary>
        private void OnCreateAccountCommand()
        {
            try
            {
                var result = _dialogService.ShowDialog<NewEmployeeRegisterWindow, NewEmployeeRegisterWindowViewModel, int, object>(Employee.Value.Id);
                if (result.Accepted)
                {
                    _dialogService.ShowAlert($"{Employee.Value.Name} dolgozó felhasználója sikeresen létrehozva!");
                    _navigationService.GoBack();
                }
            }
            catch (Exception e)
            {
                _loggingService.LogInformation("Error during creation of new account!", e);
                _dialogService.ShowWarning(e.Message);
            }
        }

        /// <summary>
        /// Tells the server to set the retirement date
        /// </summary>
        private async void OnRetireCommand()
        {
            try
            {
                IsLoading = true;
                var result = _dialogService.ShowConfirmDialog($"Biztosan felveszi {Employee.Value.Name} dolgozó munkaviszonyának megszűnését?", "Munkaviszony megszűnése");
                if (result)
                {
                    await _employeeApiService.RetireEmployeeById(Employee.Value.Id);
                    _dialogService.ShowAlert($"{Employee.Value.Name} munkaviszony megszűnése sikeresen felvéve!");
                    _navigationService.GoBack();
                }
            }
            catch (Exception e)
            {
                _loggingService.LogInformation("Error during retiring of new Employee", e);
                _dialogService.ShowWarning(e.Message);
            }
            finally
            {
                IsLoading = false;
            }
        }

        /// <summary>
        /// Updates or adds a new user
        /// </summary>
        private async void OnSaveCommand()
        {
            try
            {
                await _employeeApiService.AddOrEditEmployee(Employee.Value);
                _navigationService.GoBack();
            }
            catch (Exception e)
            {
                _loggingService.LogInformation("Error during insertion of new Employee", e);
                _dialogService.ShowWarning(e.Message);
            }
        }

        /// <summary>
        /// Returns without saving
        /// </summary>
        private void OnCancelCommand()
        {
            _navigationService.GoBack();
        }

        /// <summary>
        /// Gets the items that the employee has from the server
        /// </summary>
        private async void OnListItemsCommand()
        {
            try
            {
                IsLoading = true;
                var itemList = await _inventoryApiService.GetItemsByEmployeeId(Employee.Value.Id);
                _navigationService.NavigateTo(ViewModelLocator.InventoryItemSearchPage, itemList);
            }
            catch (Exception e)
            {
                _loggingService.LogInformation("Error during query of employee's items!", e);
                _dialogService.ShowWarning(e.Message);
            }
            finally
            {
                IsLoading = false;
            }
        }

        protected async override Task InitializeAsync()
        {
            try
            {
                IsLoading = true;
                var departments = await _departmentApiService.GetAvailable();
                if (departments != null)
                {
                    Departments = new ObservableCollection<DepartmentDto>(departments);
                }
                else
                {
                    Departments = new ObservableCollection<DepartmentDto>();
                }

                var sections = await _sectionApiService.GetAvailable();
                if (sections != null)
                {
                    Sections = new ObservableCollection<SectionDto>(sections);
                }
                else
                {
                    Sections = new ObservableCollection<SectionDto>();
                }

                if (_navigationService.Parameter != null)
                {
                    if (_navigationService.Parameter is EmployeeDto param)
                    {
                        Employee.Value = param.Copy();
                        RaisePropertyChanged(() => Employee);
                    }
                }
            }
            catch (Exception e)
            {
                _loggingService.LogFatal("Error during initialization of Employee list!", e);
                _dialogService.ShowError(e.Message);
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}
