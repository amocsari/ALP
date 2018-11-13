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

        /// <summary>
        /// The command bound to the new button
        /// </summary>
        public ICommand NewCommand { get; private set; }

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
            Initialization = InitializeAsync();
        }

        /// <summary>
        /// Loads data from the server
        /// </summary>
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

        /// <summary>
        /// Edits a dto and sends it to the server to change its value in the database
        /// Returns void because RelayCommand expects void as return value
        /// </summary>
        /// <param name="dto">The dto in the double clicked line</param>
        private async void OnListItemDoubleClickCommand(T dto)
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
                        await _lookupApiService.Update(updateddto);
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

        /// <summary>
        /// Toggles the value of the dto's locked state
        /// Returns void because RelayCommand expects void as return value
        /// </summary>
        /// <param name="dto">The dto, the lock button belongs to</param>
        private async void OnLockCommand(T dto)
        {
            try
            {
                IsLoading = true;
                await _lookupApiService.ToggleLockStateById(dto.Id);
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
