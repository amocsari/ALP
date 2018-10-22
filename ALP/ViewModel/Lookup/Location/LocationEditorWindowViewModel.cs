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
    public class LocationEditorWindowViewModel
    {
        public ICommand SaveCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }

        public LocationDto Location { get; set; }

        private bool CanSave { get => !string.IsNullOrEmpty(Location?.Name); }

        public LocationEditorWindowViewModel()
        {
            SaveCommand = new RelayCommand(OnSaveCommand, CanSave);
            CancelCommand = new RelayCommand<Window>(OnCancelCommand);
        }

        private void OnSaveCommand()
        {
            throw new NotImplementedException();
        }

        private void OnCancelCommand(Window window)
        {
            if (window != null)
            {
                window.Close();
            }
        }
    }
}
