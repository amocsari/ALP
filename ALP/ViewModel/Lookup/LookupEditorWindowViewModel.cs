using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Model.Dto;

namespace ALP.ViewModel
{
    public class LookupEditorWindowViewModel<T> : AlpViewModelBase, IDialogViewModel<T, T> where T: LookupDtoBase, new()
    {
        public ICommand SaveCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }

        public T dto;
        public T Parameter { get => dto; set { Set(ref dto, value); } }
        public T ReturnParameter { get => dto; set { dto = value; } }


        private bool CanSave { get => !string.IsNullOrEmpty(Parameter?.Name); }

        public LookupEditorWindowViewModel()
        {
            SaveCommand = new RelayCommand<Window>(OnSaveCommand, CanSave);
            CancelCommand = new RelayCommand<Window>(OnCancelCommand);

            dto = new T();
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

        protected override Task InitializeAsync()
        {
            return Task.CompletedTask;
        }
    }
}
