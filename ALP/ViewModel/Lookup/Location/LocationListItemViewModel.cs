using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Model;

namespace ALP.ViewModel.Lookup.Location
{
    public class LocationListItemViewModel : ViewModelBase
    {
        //private static ImageSource LockedImageSource { get; } = new BitmapImage(new Uri("/Resources/locked.png"));
        //private static ImageSource UnLockedImageSource { get; } = new BitmapImage(new Uri("/Resources/unlocked.png"));

        public ICommand ListItemDoubleClickCommand { get; set; }
        public ICommand LockCommand { get; set; }
        
        private readonly Action<LocationDto> onLockCommand;

        public LocationDto Location { get; set; }

        public ImageSource LockedStateImageSource {
            get
            {
                return Location.Locked
                    ? new BitmapImage(new Uri("/Resources/locked.png", UriKind.RelativeOrAbsolute))
                    : new BitmapImage(new Uri("/Resources/unlocked.png", UriKind.RelativeOrAbsolute));
            }
        }

        public LocationListItemViewModel(LocationDto location, Action<LocationDto> OnListItemDoubleClickCommand, Action<LocationDto> onLockCommand)
        {
            Location = location;
            ListItemDoubleClickCommand = new RelayCommand<LocationDto>(OnListItemDoubleClickCommand);
            LockCommand = new RelayCommand<LocationDto>(OnLockCommand);
            this.onLockCommand = onLockCommand;
        }

        private void OnLockCommand(LocationDto dto)
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
