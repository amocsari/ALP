using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ALP.Service;
using ALP.Service.Interface;
using Common.Model.Dto;
using GalaSoft.MvvmLight.CommandWpf;

namespace ALP.ViewModel.Inventory
{
    public class InventoryOperationWindowViewModel : AlpViewModelBase, IDialogViewModel<List<ItemDto>, List<int>>
    {
        public List<int> parameter;
        public List<int> Parameter
        {
            get { return parameter;}
            set
            {
                if (parameter != value)
                {
                    Set(ref parameter, value);
                    RaisePropertyChanged(() => NumItems);
                }
            }
        }
        public List<ItemDto> ReturnParameter { get; set; }

        private ObservableCollection<EmployeeDto> employeeList;
        public ObservableCollection<EmployeeDto> EmployeeList
        {
            get { return employeeList; }
            set
            {
                if (employeeList != value)
                {
                    Set(ref employeeList, value);
                }
            }
        }

        public EmployeeDto SelectedEmployee { get; set; }

        private ObservableCollection<DepartmentDto> departmentList;
        public ObservableCollection<DepartmentDto> DepartmentList
        {
            get { return departmentList; }
            set
            {
                if (departmentList != value)
                {
                    Set(ref departmentList, value);
                }
            }
        }

        public DepartmentDto SelectedDepartment { get; set; }

        public bool Priority { get; set; }

        public string NumItems { get { return Parameter.Count.ToString(); } }

        public ICommand ScrapCommand { get; private set; }
        public ICommand ChangeDepartmentCommand { get; private set; }
        public ICommand ChangeOwnerCommand { get; private set; }
        public ICommand ChangeOwnerToDepartmentChiefCommand { get; private set; }

        private readonly IOperationService _operationApiService;
        private readonly IAlpLoggingService<InventoryOperationWindowViewModel> _loggingService;
        private readonly IAlpDialogService _dialogService;
        private readonly IEmployeeApiService _employeeApiService;
        private readonly ILookupApiService<DepartmentDto> _departmentApiService;

        public InventoryOperationWindowViewModel(IOperationService operationApiService, IAlpLoggingService<InventoryOperationWindowViewModel> loggingService, IAlpDialogService dialogService, IEmployeeApiService employeeApiService, ILookupApiService<DepartmentDto> departmentApiService)
        {
            _operationApiService = operationApiService;
            _loggingService = loggingService;
            _dialogService = dialogService;
            _employeeApiService = employeeApiService;
            _departmentApiService = departmentApiService;

            ScrapCommand = new RelayCommand<Window>(OnScrapCommand);
            ChangeDepartmentCommand = new RelayCommand<Window>(OnChangeDepartmentCommand);
            ChangeOwnerCommand = new RelayCommand<Window>(OnChangeOwnerCommand);
            ChangeOwnerToDepartmentChiefCommand = new RelayCommand<Window>(OnChangeOwnerToDepartmentChiefCommand);

            Initialization = InitializeAsync();
        }

        private async void OnChangeOwnerToDepartmentChiefCommand(Window window)
        {
            try
            {
                IsLoading = true;
                ReturnParameter = await _operationApiService.ChangeOwnerToDepartmentChief(Parameter, Priority);
                window.Close();
            }
            catch (Exception e)
            {
                window.Close();
                throw e;
                IsLoading = false;
            }
        }

        private async void OnChangeOwnerCommand(Window window)
        {
            try
            {
                IsLoading = true;
                ReturnParameter = await _operationApiService.ChangeOwner(Parameter, SelectedEmployee.Id, Priority);
                window.Close();
            }
            catch (Exception e)
            {
                window.Close();
                throw e;
                IsLoading = false;
            }
        }

        private async void OnChangeDepartmentCommand(Window window)
        {
            try
            {
                IsLoading = true;
                ReturnParameter = await _operationApiService.ChangeDepartment(Parameter, SelectedDepartment.Id, Priority);
                window.Close();
            }
            catch (Exception e)
            {
                window.Close();
                throw e;
                IsLoading = false;
            }
        }

        private async void OnScrapCommand(Window window)
        {
            try
            {
                IsLoading = true;
                ReturnParameter = await _operationApiService.Scrap(Parameter, Priority);
                window.Close();
            }
            catch (Exception e)
            {
                throw e;
                IsLoading = false;
            }
        }

        protected override async Task InitializeAsync()
        {
            try
            {
                IsLoading = true;
                Parameter = new List<int>();

                var departemnts = await _departmentApiService.GetAvailable();
                DepartmentList = new ObservableCollection<DepartmentDto>(departemnts);

                var employees = await _employeeApiService.GetAvailable();
                EmployeeList = new ObservableCollection<EmployeeDto>(employees);
            }
            catch (Exception e)
            {
                _loggingService.LogFatal("Error during initalization!", e);
                _dialogService.ShowError(e.Message);
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}
