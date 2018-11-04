using ALP.Service;
using Common.Model.Dto;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ALP.ViewModel.Lookup
{
    public class LookupBuildingEditorWindowViewModel : LookupEditorWindowViewModel<BuildingDto>
    {
        private ObservableCollection<LocationDto> locations;
        public ObservableCollection<LocationDto> Locations
        {
            get { return locations; }
            set
            {
                if (value != locations)
                {
                    Set(ref locations, value);
                }
            }
        }

        public LocationDto SelectedLocation
        {
            get
            {
                return Parameter.Location;
            }
            set
            {
                if (Parameter.Location == null || !Parameter.Location.Equals(value))
                {
                    Parameter.Location = value;
                }
            }
        }

        private readonly ILookupApiService<LocationDto> _locationApiService;

        public LookupBuildingEditorWindowViewModel(ILookupApiService<LocationDto> locationApiService)
        {
            _locationApiService = locationApiService;
            Initialization = InitializeAsync();
        }

        protected override async Task InitializeAsync()
        {
            try
            {
                IsLoading = true;
                var locationList = await _locationApiService.GetAll();
                Locations = new ObservableCollection<LocationDto>(locationList);
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
