using ALP.Service;
using Common.Model.Dto;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ALP.ViewModel.Lookup
{
    public class LookupFloorEditorWindowViewModel : LookupEditorWindowViewModel<FloorDto>
    {
        private ObservableCollection<BuildingDto> buildings;
        public ObservableCollection<BuildingDto> Buildings
        {
            get { return buildings; }
            set
            {
                if (value != buildings)
                {
                    Set(ref buildings, value);
                }
            }
        }

        public BuildingDto SelectedBuilding
        {
            get
            {
                return Parameter.Building;
            }
            set
            {
                if (Parameter.Building == null || !Parameter.Building.Equals(value))
                {
                    Parameter.Building = value;
                }
            }
        }

        private readonly ILookupApiService<BuildingDto> _buildingApiService;

        public LookupFloorEditorWindowViewModel(ILookupApiService<BuildingDto> buildingApiService)
        {
            _buildingApiService = buildingApiService;
            Initialization = InitializeAsync();
        }

        protected override async Task InitializeAsync()
        {
            try
            {
                IsLoading = true;
                var buildingList = await _buildingApiService.GetAll();
                Buildings = new ObservableCollection<BuildingDto>(buildingList);
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
