using ALP.Service;
using Common.Model.Dto;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ALP.ViewModel.Lookup
{
    /// <summary>
    /// Used to Edit an instance of a BuildingDto
    /// </summary>
    public class LookupBuildingEditorWindowViewModel : LookupEditorWindowViewModel<BuildingDto>
    {
        /// <summary>
        /// Selectable locations
        /// </summary>
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

        /// <summary>
        /// Currently selected location
        /// </summary>
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
                    Parameter.LocationId = value.Id;
                }
            }
        }

        /// <summary>
        /// Api that communicates with the server
        /// Makes Location related requests
        /// </summary>
        private readonly ILookupApiService<LocationDto> _locationApiService;

        /// <summary>
        /// Constructor
        /// Handles Dependency Injection and Initialization
        /// </summary>
        /// <param name="locationApiService"></param>
        public LookupBuildingEditorWindowViewModel(ILookupApiService<LocationDto> locationApiService)
        {
            _locationApiService = locationApiService;
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
