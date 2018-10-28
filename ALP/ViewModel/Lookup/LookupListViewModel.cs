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
    public class LookupListViewModel<T> : ViewModelBase where T : LookupDtoBase, new()
    {
        private ObservableCollection<LookupListItemViewModel<T>> locations;
        public ObservableCollection<LookupListItemViewModel<T>> Locations
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

        private readonly ILookupApiService<T> _lookupApiService;
        private readonly IAlpDialogService _dialogService;
        private Task Initialization { get; set; }

        public LookupListViewModel(ILookupApiService<T> locationApiService, IAlpDialogService dialogService)
        {
            _lookupApiService = locationApiService;
            _dialogService = dialogService;

            NewLocationCommand = new RelayCommand(OnNewLocationCommand);
            Initialization = InitializeAsync();
        }

        private async Task InitializeAsync()
        {
            var reply = await _lookupApiService.GetAll();

            if (reply != null)
            {
                var result = reply.Select(location => new LookupListItemViewModel<T>(location, OnListItemDoubleClickCommand, OnLockCommand));
                Locations = new ObservableCollection<LookupListItemViewModel<T>>(result);
            }
            else
            {
                Locations = new ObservableCollection<LookupListItemViewModel<T>>();
            }
        }

        //relay command void-t vár
        private async void OnNewLocationCommand()
        {
            var dialogResult = _dialogService.ShowDialog<LookupLocationEditorWindow, LookupEditorWindowViewModel<T>, T, T>(new T());

            if (dialogResult != null && dialogResult.Accepted && dialogResult.Value != null)
            {
                var newLocation = dialogResult.Value;
                var reply = await _lookupApiService.Add(newLocation);
                if (reply != null)
                {
                    locations.Add(new LookupListItemViewModel<T>(reply, OnListItemDoubleClickCommand, OnLockCommand));
                }
            }
        }

        private void OnListItemDoubleClickCommand(T location)
        {
            var dialogResult = _dialogService.ShowDialog<LookupLocationEditorWindow, LookupEditorWindowViewModel<T>, T, T>(location);
            if (dialogResult != null && dialogResult.Accepted && dialogResult.Value != null)
            {
                var updatedLocation = dialogResult.Value;
                if (updatedLocation.Equals(location))
                {
                    _lookupApiService.Update(updatedLocation);
                }
            }
        }

        private void OnLockCommand(T location)
        {
            _lookupApiService.ToggleLockStateById(location.Id);
            location.Locked = !location.Locked;
        }
    }
}
