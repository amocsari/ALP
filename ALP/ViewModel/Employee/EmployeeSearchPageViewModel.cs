using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using ALP.Navigation;
using ALP.Service;
using ALP.Service.Interface;
using Common.Model.Dto;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Views;
using Model.Model;

namespace ALP.ViewModel.Employee
{
    public class EmployeeSearchPageViewModel : AlpViewModelBase
    {
        private ObservableCollection<EmployeeListItemViewModel> employees;
        public ObservableCollection<EmployeeListItemViewModel> Employees
        {
            get { return employees; }
            set
            {
                if (employees != value)
                {
                    Set(ref employees, value);
                }
            }
        }

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

        private string nameFilter;
        public string NameFilter
        {
            get { return nameFilter; }
            set
            {
                if (nameFilter != value)
                {
                    Set(ref nameFilter, value);
                }
            }
        }

        private DepartmentDto selectedDepartment;
        public DepartmentDto SelectedDepartment
        {
            get { return selectedDepartment; }
            set
            {
                if (selectedDepartment != value)
                {
                    Set(ref selectedDepartment, value);
                }
            }
        }

        private SectionDto selectedSection;
        public SectionDto SelectedSection
        {
            get { return selectedSection; }
            set
            {
                if (selectedSection != value)
                {
                    Set(ref selectedSection, value);
                }
            }
        }

        private EmployeeListItemViewModel selectedEmployee;
        public EmployeeListItemViewModel SelectedEmpolyee
        {
            get { return selectedEmployee; }
            set
            {
                if (selectedEmployee != value)
                {
                    Set(ref selectedEmployee, value);
                }
            }
        }

        public ICommand SearchCommand { get; private set; }
        public ICommand NewEmployeeCommand { get; private set; }
        public ICommand ListItemDoubleClickCommand { get; private set; }

        private readonly IEmployeeApiService _employeeApiService;
        private readonly IAlpNavigationService _navigationService;
        private readonly ILookupApiService<DepartmentDto> _departmentApiService;
        private readonly ILookupApiService<SectionDto> _sectionApiService;
        private readonly IAlpDialogService _dialogService;
        private readonly IAlpLoggingService<EmployeeSearchPageViewModel> _loggingService;

        public EmployeeSearchPageViewModel(IEmployeeApiService employeeApiService, IAlpNavigationService navigationService, ILookupApiService<DepartmentDto> departmentApiService, ILookupApiService<SectionDto> sectionApiService, IAlpDialogService dialogService, IAlpLoggingService<EmployeeSearchPageViewModel> loggingService)
        {
            _employeeApiService = employeeApiService;
            _navigationService = navigationService;
            _departmentApiService = departmentApiService;
            _sectionApiService = sectionApiService;
            _dialogService = dialogService;
            _loggingService = loggingService;

            SearchCommand = new RelayCommand(OnSearchCommand);
            NewEmployeeCommand = new RelayCommand(OnNewEmployeeCommand);
            ListItemDoubleClickCommand = new RelayCommand(OnListItemDoubleClickCommand);

            Initialization = InitializeAsync();
        }

        private void OnListItemDoubleClickCommand()
        {
            _navigationService.NavigateTo(ViewModelLocator.EmployeeEditPage, SelectedEmpolyee.Value);
        }

        private void OnNewEmployeeCommand()
        {
            _navigationService.NavigateTo(ViewModelLocator.EmployeeEditPage);
        }

        private async void OnSearchCommand()
        {
            try
            {
                IsLoading = true;
                var filterInfo = new EmployeeFilterInfo
                {
                    Name = NameFilter,
                    DepartmentId = SelectedDepartment?.Id,
                    SectionId = SelectedSection?.Id
                };
                var reply = await _employeeApiService.FilterEmployees(filterInfo);
                if (reply != null)
                {
                    var result = reply.Select(employee => new EmployeeListItemViewModel(employee)).ToList();
                    Employees = new ObservableCollection<EmployeeListItemViewModel>(result);
                }
                else
                {
                    Employees = new ObservableCollection<EmployeeListItemViewModel>();
                }
            }
            catch (Exception e)
            {
                _loggingService.LogError("Error during Employee search", e);
                _dialogService.ShowError(e.Message);
            }
            finally
            {
                IsLoading = false;
            }
        }

        protected override async Task InitializeAsync()
        {
            try
            {
                IsLoading = true;
                var employees = await _employeeApiService.GetAll();
                if (employees != null)
                {
                    var result = employees.Select(employee => new EmployeeListItemViewModel(employee)).ToList();
                    Employees = new ObservableCollection<EmployeeListItemViewModel>(result);
                }
                else
                {
                    Employees = new ObservableCollection<EmployeeListItemViewModel>();
                }

                var departmentList = await _departmentApiService.GetAll();
                if (departmentList != null)
                {
                    Departments = new ObservableCollection<DepartmentDto>(departmentList);
                }
                else
                {
                    Departments = new ObservableCollection<DepartmentDto>();
                }

                var sectionList = await _sectionApiService.GetAll();
                if (sectionList != null)
                {
                    Sections = new ObservableCollection<SectionDto>(sectionList);
                }
                else
                {
                    Sections = new ObservableCollection<SectionDto>();
                }
            }
            catch (Exception e)
            {
                _loggingService.LogFatal("Error during Employee list initialization", e);
                _dialogService.ShowError(e.Message);
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}
