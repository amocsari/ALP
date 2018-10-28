using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Model.Dto;

namespace ALP.ViewModel
{
    public class LookupEditorWindowViewModel<T> : ViewModelBase, IDialogViewModel<T, T> where T: LookupDtoBase, new()
    {
        public ICommand SaveCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }

        public T location;
        public T Parameter { get => location; set { Set(ref location, value); } }
        public T ReturnParameter { get => location; set { } }


        private bool CanSave { get => !string.IsNullOrEmpty(Parameter?.Name); }

        public LookupEditorWindowViewModel()
        {
            SaveCommand = new RelayCommand<Window>(OnSaveCommand, CanSave);
            CancelCommand = new RelayCommand<Window>(OnCancelCommand);

            location = new T();
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
