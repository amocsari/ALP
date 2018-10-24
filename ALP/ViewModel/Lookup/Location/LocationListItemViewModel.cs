using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using Model;

namespace ALP.ViewModel.Lookup.Location
{
    public class LocationListItemViewModel
    {
        public ICommand ListItemDoubleClickCommand { get; set; }
        public LocationDto Location { get; set; }

        public LocationListItemViewModel(LocationDto location, Action<LocationDto> OnListItemDoubleClickCommand)
        {
            Location = location;
            ListItemDoubleClickCommand = new RelayCommand<LocationDto>(OnListItemDoubleClickCommand);
        }
    }
}
