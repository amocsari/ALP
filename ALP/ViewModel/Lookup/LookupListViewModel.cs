using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using ALP.Service;
using GalaSoft.MvvmLight.CommandWpf;
using Common.Model.Dto;

namespace ALP.ViewModel.Lookup
{
    public class LookupListViewModel<T> : AlpViewModelBase where T : LookupDtoBase, new()
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

        public LookupListViewModel(ILookupApiService<T> lookupApiService, IAlpDialogService dialogService)
        {
            _lookupApiService = lookupApiService;
            _dialogService = dialogService;

            NewCommand = new RelayCommand(OnNewCommand);
            Initialization = InitializeAsync();
        }

        protected override async Task InitializeAsync()
        {
            try
            {
                IsLoading = true;
                var reply = await _lookupApiService.GetAll();

                if (reply != null)
                {
                    var result = reply.Select(value =>
                        new LookupListItemViewModel<T>(value, OnListItemDoubleClickCommand, OnLockCommand));
                    Values = new ObservableCollection<LookupListItemViewModel<T>>(result);
                }
                else
                {
                    Values = new ObservableCollection<LookupListItemViewModel<T>>();
                }
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

        //relay command void-t vár
        private async void OnNewCommand()
        {
            try
            {
                IsLoading = true;
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
            catch (Exception)
            {
                //TODO: logging
            }
            finally
            {
                IsLoading = false;
            }

        }

        private void OnListItemDoubleClickCommand(T dto)
        {
            try
            {
                IsLoading = true;
                var dialogResult = _dialogService.ShowDtoEditorWindow(dto);
                if (dialogResult != null && dialogResult.Accepted && dialogResult.Value != null)
                {
                    var updateddto = dialogResult.Value;
                    if (!updateddto.Equals(dto))
                    {
                        _lookupApiService.Update(updateddto);
                        dto.UpdateByDto(updateddto);
                    }
                }
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

        private void OnLockCommand(T dto)
        {
            try
            {
                IsLoading = true;
                _lookupApiService.ToggleLockStateById(dto.Id);
                dto.Locked = !dto.Locked;
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
