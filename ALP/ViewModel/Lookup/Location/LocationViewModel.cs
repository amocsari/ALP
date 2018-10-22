using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ALP.View.Lookup;
using GalaSoft.MvvmLight.CommandWpf;
using Model;

namespace ALP.ViewModel.Lookup
{
    public class LocationViewModel
    {
        public ObservableCollection<LocationDto> Locations { get; set; }
        public ICommand NewLocationCommand { get; private set; }

        public LocationViewModel()
        {
            NewLocationCommand = new RelayCommand(OnNewLocationCommand);
            Locations = new ObservableCollection<LocationDto>
            {
                new LocationDto
                {
                    Name =  "ÁDI",
                },
                new LocationDto
                {
                    Name = "DÁI",
                },
                new LocationDto
                {
                    Name = "KÁI",
                }
            };
        }

        private void OnNewLocationCommand()
        {
            var locationEditorWindow = new LookupLocationEditorWindow();
            if (locationEditorWindow.ShowDialog() == true)
            {

            }
        }
    }
}
