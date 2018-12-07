using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using Common.Model.Dto;

namespace ALP.ViewModel
{
    /// <summary>
    /// Handles the data edit of a lookup dto
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class LookupEditorWindowViewModel<T> : AlpViewModelBase, IDialogViewModel<T, T> where T: LookupDtoBase, new()
    {
        /// <summary>
        /// Commands
        /// </summary>
        public ICommand SaveCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }

        /// <summary>
        /// Dto parameter
        /// </summary>
        public T dto;
        public T Parameter { get => dto; set { Set(ref dto, value); } }
        public T ReturnParameter { get => dto; set { dto = value; } }

        public LookupEditorWindowViewModel()
        {
            SaveCommand = new RelayCommand<Window>(OnSaveCommand);
            CancelCommand = new RelayCommand<Window>(OnCancelCommand);

            dto = new T();
        }

        /// <summary>
        /// closes the window
        /// returns with positive dialogresult
        /// </summary>
        /// <param name="window"></param>
        private void OnSaveCommand(Window window)
        {
            if (window != null)
            {
                window.DialogResult = true;
                window.Close();
            }
        }

        /// <summary>
        /// closes the window
        /// returns with negative dialogresult
        /// </summary>
        /// <param name="window"></param>
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
