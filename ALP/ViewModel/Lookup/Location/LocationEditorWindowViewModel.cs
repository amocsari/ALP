using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Model;

namespace ALP.ViewModel.Lookup
{
    public class LocationEditorWindowViewModel : IDialogViewModel<LocationDto, LocationDto>
    {
        public ICommand SaveCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }

        public LocationDto Parameter { get; set; }
        public LocationDto ReturnParameter { get => Location; set {} }

        public LocationDto Location { get; set; }

        private bool CanSave { get => !string.IsNullOrEmpty(Parameter?.Name); }

        public LocationEditorWindowViewModel()
        {
            SaveCommand = new RelayCommand<Window>(OnSaveCommand, CanSave);
            CancelCommand = new RelayCommand<Window>(OnCancelCommand);

            Location = new LocationDto();
        }

        private void OnSaveCommand(Window window)
        {
            if (window != null)
            {
                window.DialogResult = true;
                window.Close();
            }
        }

        private void OnCancelCommand(Window window)
        {
            if (window != null)
            {
                window.DialogResult = false;
                window.Close();
            }
        }
    }
}
