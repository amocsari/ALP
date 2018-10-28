using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Model.Dto;
using System;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ALP.ViewModel.Lookup
{
    public class LookupListItemViewModel<T> : ViewModelBase where T : LookupDtoBase
    {
        public ICommand ListItemDoubleClickCommand { get; set; }
        public ICommand LockCommand { get; set; }

        private readonly Action<T> onLockCommand;

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

        public LookupListItemViewModel(T value, Action<T> OnListItemDoubleClickCommand, Action<T> onLockCommand)
        {
            Value = value;
            ListItemDoubleClickCommand = new RelayCommand<T>(OnListItemDoubleClickCommand);
            LockCommand = new RelayCommand<T>(OnLockCommand);
            this.onLockCommand = onLockCommand;
        }

        private void OnLockCommand(T dto)
        {
            try
            {
                onLockCommand(dto);
            }
            catch (Exception e)
            {
                return;
            }

            RaisePropertyChanged(() => LockedStateImageSource);
        }
    }
}

