using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using ALP.API;
using ALP.Service;
using ALP.View.Lookup;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Views;
using Model;

namespace ALP.ViewModel.Lookup
{
    public class LocationViewModel: ViewModelBase
    {
        private ObservableCollection<LocationDto> locations;
        public ObservableCollection<LocationDto> Locations
        {
            get
            {
                return locations;
            }
            set
            {
                if(value != locations)
                {
                    locations = value;
                    RaisePropertyChanged(nameof(Locations));
                }
            }
        }
        public Task Initialization { get; private set; }
        public ICommand NewLocationCommand { get; private set; }

        private readonly ILocationService _locationApi;
        private readonly IAlpDialogService _dialogService;

        public LocationViewModel(ILocationService locationApi, IAlpDialogService dialogService)
        {
            _locationApi = locationApi;
            _dialogService = dialogService;
            
            NewLocationCommand = new RelayCommand(OnNewLocationCommand);
            Initialization = InitializeAsync();
        }

        private async Task InitializeAsync()
        {
            var result = await _locationApi.GetAllLocations();

            Locations = new ObservableCollection<LocationDto>(result);
        }

        //relay command void-t vár
        private async void OnNewLocationCommand()
        {
            var result = _dialogService.ShowDialog<LookupLocationEditorWindow, LocationEditorWindowViewModel, LocationDto, LocationDto>(new LocationDto {Name = "asd"});
            
            if (result != null && result.Accepted && result.Value != null)
            {
                var newLocation = result.Value;
                if (await _locationApi.AddLocation(newLocation))
                    locations.Add(newLocation);
            }
        }
    }
}
