using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ALP.Service;
using Common.Model.Dto;

namespace ALP.ViewModel.Lookup
{
    public class LookupSectionEditorWindowViewModel: LookupEditorWindowViewModel<SectionDto>
    {
        /// <summary>
        /// Selectable floors
        /// </summary>
        private ObservableCollection<FloorDto> floors;
        public ObservableCollection<FloorDto> Floors
        {
            get { return floors; }
            set
            {
                if (value != floors)
                {
                    Set(ref floors, value);
                }
            }
        }

        /// <summary>
        /// Selectable departments
        /// </summary>
        private ObservableCollection<DepartmentDto> departments;
        public ObservableCollection<DepartmentDto> Departments
        {
            get { return departments; }
            set
            {
                if (value != departments)
                {
                    Set(ref departments, value);
                }
            }
        }

        /// <summary>
        /// Currently selected floor
        /// </summary>
        public FloorDto SelectedFloor
        {
            get
            {
                return Parameter.Floor;
            }
            set
            {
                if (Parameter.Floor == null || !Parameter.Floor.Equals(value))
                {
                    Parameter.Floor = value;
                    Parameter.FloorId = value.Id;
                }
            }
        }

        /// <summary>
        /// Currently selected department
        /// </summary>
        public DepartmentDto SelectedDepartment
        {
            get
            {
                return Parameter.Department;
            }
            set
            {
                if (Parameter.Department == null || !Parameter.Department.Equals(value))
                {
                    Parameter.Department = value;
                    Parameter.DepartmentId = value.Id;
                }
            }
        }

        /// <summary>
        /// Api that communicates with the server
        /// Makes FloorDto related requests
        /// </summary>
        private readonly ILookupApiService<FloorDto> _floorApiService;
        /// <summary>
        /// Api that communicates with the server
        /// Makes DepartmentDto related requests
        /// </summary>
        private readonly ILookupApiService<DepartmentDto> _departmentApiService;

        /// <summary>
        /// Constructor
        /// Handles Dependency Injection and Initialization
        /// </summary>
        /// <param name="floorApiService"></param>
        /// <param name="departmentApiService"></param>
        public LookupSectionEditorWindowViewModel(ILookupApiService<FloorDto> floorApiService, ILookupApiService<DepartmentDto> departmentApiService)
        {
            _floorApiService = floorApiService;
            _departmentApiService = departmentApiService;
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
                var floorList = await _floorApiService.GetAll();
                Floors = new ObservableCollection<FloorDto>(floorList);
                var departmentList = await _departmentApiService.GetAll();
                Departments = new ObservableCollection<DepartmentDto>(departmentList);
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
