﻿using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ALP.Navigation;
using ALP.Service;
using ALP.Service.Interface;
using Common.Model.Dto;
using GalaSoft.MvvmLight.CommandWpf;

namespace ALP.ViewModel.Employee
{
    public class EmployeeEditPageViewModel : AlpViewModelBase
    {
        private EmployeeListItemViewModel employee;

        public EmployeeListItemViewModel Employee
        {
            get { return employee; }
            set
            {
                if(employee != value)
                {
                    Set(ref employee, value);
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

        public ICommand SaveCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }
        public ICommand ListItemsCommand { get; private set; }

        private readonly ILookupApiService<DepartmentDto> _departmentApiService;
        private readonly ILookupApiService<SectionDto> _sectionApiService;
        private readonly IAlpNavigationService _navigationService;
        private readonly IEmployeeApiService _employeeApiService;
        private readonly IAlpLoggingService<EmployeeEditPageViewModel> _loggingService;
        private readonly IAlpDialogService _dialogService;

        public EmployeeEditPageViewModel(ILookupApiService<DepartmentDto> departmentApiService, ILookupApiService<SectionDto> sectionApiService, IAlpNavigationService navigationService, IEmployeeApiService employeeApiService, IAlpLoggingService<EmployeeEditPageViewModel> loggingService, IAlpDialogService dialogService)
        {
            Employee = new EmployeeListItemViewModel(new EmployeeDto());

            _departmentApiService = departmentApiService;
            _sectionApiService = sectionApiService;
            _navigationService = navigationService;
            _employeeApiService = employeeApiService;
            _loggingService = loggingService;
            _dialogService = dialogService;

            SaveCommand = new RelayCommand(OnSaveCommand);
            CancelCommand = new RelayCommand(OnCancelCommand);
            ListItemsCommand = new RelayCommand(OnListItemsCommand);

            Initialization = InitializeAsync();
        }

        private async void OnSaveCommand()
        {
            try
            {
                await _employeeApiService.AddNewEmployee(Employee.Value);
                _navigationService.GoBack();
            }
            catch (Exception e)
            {
                _loggingService.LogInformation("Error during insertion of new Employee", e);
                _dialogService.ShowWarning(e.Message);
            }
        }

        private void OnCancelCommand()
        {
            _navigationService.GoBack();
        }

        private void OnListItemsCommand()
        {
            throw new NotImplementedException();
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
