using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using ALP.Service;
using ALP.View.Lookup;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Model.Dto;

namespace ALP.ViewModel.Lookup
{
    public class LocationListViewModel : ViewModelBase
    {
        private ObservableCollection<LookupListItemViewModel<LocationDto>> locations;
        public ObservableCollection<LookupListItemViewModel<LocationDto>> Locations
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
        public ICommand NewLocationCommand { get; private set; }

        private readonly ILocationApiService _locationApiService;
        private readonly IAlpDialogService _dialogService;
        private Task Initialization { get; set; }

        public LocationListViewModel(ILocationApiService locationApiService, IAlpDialogService dialogService)
        {
            _locationApiService = locationApiService;
            _dialogService = dialogService;

            NewLocationCommand = new RelayCommand(OnNewLocationCommand);
            Initialization = InitializeAsync();
        }

        private async Task InitializeAsync()
        {
            var reply = await _locationApiService.GetAllLocations();

            if (reply != null)
            {
                var result = reply.Select(location => new LookupListItemViewModel<LocationDto>(location, OnListItemDoubleClickCommand, OnLockCommand));
                Locations = new ObservableCollection<LookupListItemViewModel<LocationDto>>(result);
            }
            else
            {
                Locations = new ObservableCollection<LookupListItemViewModel<LocationDto>>();
            }
        }

        //relay command void-t vár
        private async void OnNewLocationCommand()
        {
            var dialogResult = _dialogService.ShowDialog<LookupLocationEditorWindow, LookupEditorWindowViewModel<LocationDto>, LocationDto, LocationDto>(new LocationDto());

            if (dialogResult != null && dialogResult.Accepted && dialogResult.Value != null)
            {
                var newLocation = dialogResult.Value;
                var reply = await _locationApiService.AddLocation(newLocation);
                if (reply != null)
                {
                    locations.Add(new LookupListItemViewModel<LocationDto>(reply, OnListItemDoubleClickCommand, OnLockCommand));
                }
            }
        }

        private void OnListItemDoubleClickCommand(LocationDto location)
        {
            var dialogResult = _dialogService.ShowDialog<LookupLocationEditorWindow, LookupEditorWindowViewModel<LocationDto>, LocationDto, LocationDto>(location);
            if (dialogResult != null && dialogResult.Accepted && dialogResult.Value != null)
            {
                var updatedLocation = dialogResult.Value;
                if (updatedLocation.Equals(location))
                {
                    _locationApiService.UpdateLocation(updatedLocation);
                }
            }
        }

        private void OnLockCommand(LocationDto location)
        {
            _locationApiService.ToggleLocationLockStateById(location.Id);
            location.Locked = !location.Locked;
        }
    }
}
