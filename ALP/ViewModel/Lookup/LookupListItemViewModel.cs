using Common.Model.Dto;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using GalaSoft.MvvmLight.CommandWpf;

namespace ALP.ViewModel.Lookup
{
    /// <summary>
    /// Used to store a lookupdto element in an observable collection
    /// </summary>
    /// <typeparam name="T">Lookupdto type</typeparam>
    public class LookupListItemViewModel<T> : AlpViewModelBase where T : LookupDtoBase
    {
        //Commands
        public ICommand ListItemDoubleClickCommand { get; set; }
        public ICommand LockCommand { get; set; }

        //Actions that are called during the Commands
        private readonly Action onLockCommand;
        private readonly Action onListItemDoubleClickCommand;

        /// <summary>
        /// The actual value of the dto
        /// </summary>
        public T Value
        {
            get { return value;}
            set
            {
                if (value != this.value)
                {
                    Set(ref this.value, value);
                }
            }
        }
        private T value;

        /// <summary>
        /// The source of the lock button
        /// </summary>
        public ImageSource LockedStateImageSource
        {
            get
            {
                //TODO: kiszervezni resourceservice-be
                return Value.Locked
                    ? new BitmapImage(new Uri("/Resources/locked.png", UriKind.RelativeOrAbsolute))
                    : new BitmapImage(new Uri("/Resources/unlocked.png", UriKind.RelativeOrAbsolute));
            }
        }

        /// <summary>
        /// Constructor
        /// Handles the dependency injection
        /// Sets the commands
        /// Sets the command action
        /// </summary>
        /// <param name="value"></param>
        /// <param name="onListItemDoubleClickCommand"></param>
        /// <param name="onLockCommand"></param>
        public LookupListItemViewModel(T value, Action listItemDoubleClickCommand, Action lockCommand)
        {
            Value = value;
            LockCommand = new RelayCommand(lockCommand);
            ListItemDoubleClickCommand = new RelayCommand(OnListItemDoubleClickCommand);
        }

        /// <summary>
        /// Method called on the ListItemDoubleClickCommand
        /// Uses the onListItemDoubleClickCommand Action
        /// </summary>
        /// <param name="dto">The double clicked dto</param>
        private void OnListItemDoubleClickCommand()
        {
            try
            {
                onListItemDoubleClickCommand();
            }
            catch (Exception)
            {
                //TODO: logging
            }

            RaisePropertyChanged(() => Value);
        }

        /// <summary>
        /// Method called on the LockCommand
        /// Uses the onLockCommand Action
        /// </summary>
        /// <param name="dto">The locked dto</param>
        private void OnLockCommand()
        {
            try
            {
                onLockCommand();
            }
            catch (Exception)
            {
                //TODO: logging
            }

            RaisePropertyChanged(() => LockedStateImageSource);
        }

        /// <summary>
        /// Initializes data
        /// </summary>
        protected async override Task InitializeAsync() { }
    }
}

