using ALP.Service;
using Model.Dto;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ALP.ViewModel.Lookup
{
    public class LookupBuildingEditorWindowViewModel: LookupEditorWindowViewModel<BuildingDto>
    {
        private ObservableCollection<LocationDto> locations;
        public ObservableCollection<LocationDto> Locations
        {
            get { return locations; }
            set
            {
                if(value != locations)
                {
                    Set(ref locations, value);
                }
            }
        }

        private readonly ILookupApiService<LocationDto> _locationApiService;
        private Task Initialization { get; set; }

        public LookupBuildingEditorWindowViewModel(ILookupApiService<LocationDto> locationApiService)
        {
            _locationApiService = locationApiService;
            Initialization = InitializeAsync();
        }

        private async Task InitializeAsync()
        {
            var locationList = await _locationApiService.GetAll();
            Locations = new ObservableCollection<LocationDto>(locationList);
        }
    }
}
