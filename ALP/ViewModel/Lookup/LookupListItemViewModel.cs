using GalaSoft.MvvmLight.Command;
using Model.Dto;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ALP.ViewModel.Lookup
{
    public class LookupListItemViewModel<T> : AlpViewModelBase where T : LookupDtoBase
    {
        public ICommand ListItemDoubleClickCommand { get; set; }
        public ICommand LockCommand { get; set; }

        private readonly Action<T> onLockCommand;
        private readonly Action<T> onListItemDoubleClickCommand;

        public T Value { get; set; }

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

        public LookupListItemViewModel(T value, Action<T> onListItemDoubleClickCommand, Action<T> onLockCommand)
        {
            Value = value;
            ListItemDoubleClickCommand = new RelayCommand<T>(OnListItemDoubleClickCommand);
            LockCommand = new RelayCommand<T>(OnLockCommand);
            this.onLockCommand = onLockCommand;
            this.onListItemDoubleClickCommand = onListItemDoubleClickCommand;
        }

        private void OnListItemDoubleClickCommand(T dto)
        {
            try
            {
                onListItemDoubleClickCommand(dto);
            }
            catch (Exception)
            {
                //TODO: logging
            }

            RaisePropertyChanged(() => Value);
        }

        private void OnLockCommand(T dto)
        {
            try
            {
                onLockCommand(dto);
            }
            catch (Exception)
            {
                //TODO: logging
            }

            RaisePropertyChanged(() => LockedStateImageSource);
        }

        protected override Task InitializeAsync()
        {
            return Task.CompletedTask;
        }
    }
}

