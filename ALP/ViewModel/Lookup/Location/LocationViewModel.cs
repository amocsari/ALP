using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using ALP.API;
using ALP.View.Lookup;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
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
        public ICommand NewLocationCommand { get; private set; }

        private readonly ILocationApi _locationApi;
        public Task Initialization { get; private set; }

        public LocationViewModel(ILocationApi locationApi)
        {
            NewLocationCommand = new RelayCommand(OnNewLocationCommand);
            _locationApi = locationApi;

            Initialization = InitializeAsync();
        }

        private async Task InitializeAsync()
        {
            var result = await _locationApi.GetAllLocations();

            Locations = new ObservableCollection<LocationDto>(result);
        }

        private void OnNewLocationCommand()
        {
            var locationEditorWindow = new LookupLocationEditorWindow();
            if (locationEditorWindow.ShowDialog() == true)
            {

            }
        }
    }
}
