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
    /// <summary>
    /// The viewmodel of lookuplistviews
    /// </summary>
    /// <typeparam name="T">The Dto of the chosen lookup data</typeparam>
    public class LookupListViewModel<T> : AlpViewModelBase where T : LookupDtoBase, new()
    {
        /// <summary>
        /// The list of visible dtos
        /// </summary>
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

        public LookupListItemViewModel<T> SelectedItem { get; set; }

        //Commands
        public ICommand NewCommand { get; private set; }
        public ICommand ListItemDoubleClickCommand { get; private set; }
        public ICommand LockCommand { get; private set; }

        //Dependency injected services
        private readonly ILookupApiService<T> _lookupApiService;
        private readonly IAlpDialogService _dialogService;

        /// <summary>
        /// Constructor
        /// Handles Dependency Injection
        /// Sets commands
        /// Initializes data
        /// </summary>
        public LookupListViewModel(ILookupApiService<T> lookupApiService, IAlpDialogService dialogService)
        {
            _lookupApiService = lookupApiService;
            _dialogService = dialogService;

            NewCommand = new RelayCommand(OnNewCommand);
            ListItemDoubleClickCommand = new RelayCommand(OnListItemDoubleClickCommand);
            LockCommand = new RelayCommand(OnLockCommand);
            Initialization = InitializeAsync();
        }

        /// <summary>
        /// Loads data from the server
        /// </summary>
        protected override async Task InitializeAsync()
        {
            try
            {
                await ReloadList();
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

        /// <summary>
        /// Creates a new dto and sends it to the server to add it to the database
        /// Returns void because RelayCommand expects void as return value
        /// </summary>
        private async void OnNewCommand()
        {
            try
            {
                IsLoading = true;
                var dialogResult = _dialogService.ShowDtoEditorWindow(new T());

                if (dialogResult != null && dialogResult.Accepted && dialogResult.Value != null)
                {
                    var newdto = dialogResult.Value;
                    var success = await _lookupApiService.AddNew(newdto);
                    if (success)
                    {
                        await ReloadList();
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

        /// <summary>
        /// Edits a dto and sends it to the server to change its value in the database
        /// Returns void because RelayCommand expects void as return value
        /// </summary>
        /// <param name="dto">The dto in the double clicked line</param>
        private async void OnListItemDoubleClickCommand()
        {
            try
            {
                IsLoading = true;
                var dialogResult = _dialogService.ShowDtoEditorWindow(SelectedItem.Value);
                if (dialogResult != null && dialogResult.Accepted && dialogResult.Value != null)
                {
                    var updateddto = dialogResult.Value;
                    if (!updateddto.Equals(SelectedItem))
                    {
                        await _lookupApiService.Update(updateddto);
                        await ReloadList();
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

        /// <summary>
        /// Toggles the value of the dto's locked state
        /// Returns void because RelayCommand expects void as return value
        /// </summary>
        /// <param name="dto">The dto, the lock button belongs to</param>
        private async void OnLockCommand()
        {
            try
            {
                IsLoading = true;
                await _lookupApiService.ToggleLockStateById(SelectedItem.Value.Id);
                SelectedItem.Value.Locked = !SelectedItem.Value.Locked;
                await ReloadList();
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

        private async Task ReloadList()
        {
            IsLoading = true;
            var reply = await _lookupApiService.GetAll();

            if (reply != null)
            {
                var list = reply.Select(value => new LookupListItemViewModel<T>(value, OnListItemDoubleClickCommand, OnLockCommand)).ToList();
                Values = new ObservableCollection<LookupListItemViewModel<T>>(list);
            }
            else
            {
                Values = new ObservableCollection<LookupListItemViewModel<T>>();
            }
        }
    }
}
