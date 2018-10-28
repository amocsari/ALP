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
        private ObservableCollection<LookupListItemViewModel<T>> values;
        public ObservableCollection<LookupListItemViewModel<T>> Values
        {
            get { return values; }
            set
            {
                if (value != values)
                {
                    Set(ref values, value);
                }
            }
        }
        public ICommand NewCommand { get; private set; }

        private readonly ILookupApiService<T> _lookupApiService;
        private readonly IAlpDialogService _dialogService;
        private Task Initialization { get; set; }

        public LookupListViewModel(ILookupApiService<T> lookupApiService, IAlpDialogService dialogService)
        {
            _lookupApiService = lookupApiService;
            _dialogService = dialogService;

            NewCommand = new RelayCommand(OnNewCommand);
            Initialization = InitializeAsync();
        }

        private async Task InitializeAsync()
        {
            var reply = await _lookupApiService.GetAll();

            if (reply != null)
            {
                var result = reply.Select(value => new LookupListItemViewModel<T>(value, OnListItemDoubleClickCommand, OnLockCommand));
                Values = new ObservableCollection<LookupListItemViewModel<T>>(result);
            }
            else
            {
                Values = new ObservableCollection<LookupListItemViewModel<T>>();
            }
        }

        //relay command void-t vár
        private async void OnNewCommand()
        {
            //var dialogResult = _dialogService.ShowDialog<LookupLocationEditorWindow, LookupEditorWindowViewModel<T>, T, T>(new T());
            var dialogResult = _dialogService.ShowDtoEditorWindow(new T());

            if (dialogResult != null && dialogResult.Accepted && dialogResult.Value != null)
            {
                var newdto = dialogResult.Value;
                var reply = await _lookupApiService.AddNew(newdto);
                if (reply != null)
                {
                    values.Add(new LookupListItemViewModel<T>(reply, OnListItemDoubleClickCommand, OnLockCommand));
                }
            }
        }

        private void OnListItemDoubleClickCommand(T dto)
        {
            //var dialogResult = _dialogService.ShowDialog<LookupLocationEditorWindow, LookupEditorWindowViewModel<T>, T, T>(dto);
            var dialogResult = _dialogService.ShowDtoEditorWindow(dto);
            if (dialogResult != null && dialogResult.Accepted && dialogResult.Value != null)
            {
                var updateddto = dialogResult.Value;
                if (updateddto.Equals(dto))
                {
                    _lookupApiService.Update(updateddto);
                }
            }
        }

        private void OnLockCommand(T dto)
        {
            _lookupApiService.ToggleLockStateById(dto.Id);
            dto.Locked = !dto.Locked;
        }
    }
}
