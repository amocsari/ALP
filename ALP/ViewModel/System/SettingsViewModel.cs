using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;

namespace ALP.ViewModel
{
    public class SettingsViewModel
    {
        public ICommand SaveCommand { get; private set; }

        public int AmortizationMultiplier { get; set; }
        public int DataPreservationYear { get; set; }

        //TODO: bl-t tisztázni rá
        private bool CanSave { get => AmortizationMultiplier > 0 && DataPreservationYear > 0; }

        public SettingsViewModel()
        {
            SaveCommand = new RelayCommand(OnSaveCommand, CanSave);
        }

        private void OnSaveCommand()
        {
            throw new NotImplementedException();
        }
    }
}
