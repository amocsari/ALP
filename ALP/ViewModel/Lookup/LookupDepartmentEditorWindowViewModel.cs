using ALP.Service;
using Common.Model.Dto;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ALP.Service.Interface;

namespace ALP.ViewModel.Lookup
{
    /// <summary>
    /// Used to Edit an instance of a DepartmentDto
    /// </summary>
    public class LookupDepartmentEditorWindowViewModel : LookupEditorWindowViewModel<DepartmentDto>
    {
        /// <summary>
        /// Selectable Employees
        /// </summary>
        private ObservableCollection<EmployeeDto> employees;
        public ObservableCollection<EmployeeDto> Employees
        {
            get { return employees; }
            set
            {
                if (value != employees)
                {
                    Set(ref employees, value);
                }
            }
        }

        /// <summary>
        /// Currently selected Employee
        /// </summary>
        public EmployeeDto SelectedEmployee
        {
            get
            {
                return Parameter.Employee;
            }
            set
            {
                if (Parameter.Employee == null || !Parameter.Employee.Equals(value))
                {
                    Parameter.Employee = value;
                    Parameter.EmployeeId = value.Id;
                }
            }
        }

        /// <summary>
        /// Api that communicates with the server
        /// Makes Employee related requests
        /// </summary>
        private readonly IEmployeeApiService _employeeApiService;

        /// <summary>
        /// Constructor
        /// Handles Dependency Injection and Initialization
        /// </summary>
        /// <param name="employeeApiService"></param>
        public LookupDepartmentEditorWindowViewModel(IEmployeeApiService employeeApiService)
        {
            _employeeApiService = employeeApiService;
            Initialization = InitializeAsync();
        }

        /// <summary>
        /// Async data loading during initialization
        /// </summary>
        /// <returns></returns>
        protected override async Task InitializeAsync()
        {
            try
            {
                IsLoading = true;
                var EmployeeList = await _employeeApiService.GetAll();
                Employees = new ObservableCollection<EmployeeDto>(EmployeeList);
            }
            catch (Exception)
            {
                //TODO: logging
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}
