using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using ALP.API;
using ALP.Service;
using ALP.View.Lookup;
using ALP.ViewModel.Lookup.Location;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Views;
using Model;

namespace ALP.ViewModel.Lookup
{
    public class LocationViewModel : ViewModelBase
    {
        private ObservableCollection<LocationListItemViewModel> locations;
        public ObservableCollection<LocationListItemViewModel> Locations
        {
            get
            {
                return locations;
            }
            set
            {
                if (value != locations)
                {
                    locations = value;
                    RaisePropertyChanged(nameof(Locations));
                }
            }
        }
        public Task Initialization { get; private set; }
        public ICommand NewLocationCommand { get; private set; }
        public ICommand ListItemClickCommand { get; set; }

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
            var reply = await _locationApi.GetAllLocations();

            if (reply != null)
            {
                var result = reply.Select(location => new LocationListItemViewModel(location, OnListItemDoubleClickCommand));
                Locations = new ObservableCollection<LocationListItemViewModel>(result);
            }
            else
            {
                Locations = new ObservableCollection<LocationListItemViewModel>();
            }
        }

        //relay command void-t vár
        private async void OnNewLocationCommand()
        {
            var dialogResult = _dialogService.ShowDialog<LookupLocationEditorWindow, LocationEditorWindowViewModel, LocationDto, LocationDto>(new LocationDto());

            if (dialogResult != null && dialogResult.Accepted && dialogResult.Value != null)
            {
                var newLocation = dialogResult.Value;
                var reply = await _locationApi.AddLocation(newLocation);
                if (reply != null)
                {
                    locations.Add(new LocationListItemViewModel(reply, OnListItemDoubleClickCommand));
                }
            }
        }

        private void OnListItemDoubleClickCommand(LocationDto location)
        {
            var dialogResult = _dialogService.ShowDialog<LookupLocationEditorWindow, LocationEditorWindowViewModel, LocationDto, LocationDto>(location);
            if (dialogResult != null && dialogResult.Accepted && dialogResult.Value != null)
            {
                var updatedLocation = dialogResult.Value;
                
            }
        }
    }
}
