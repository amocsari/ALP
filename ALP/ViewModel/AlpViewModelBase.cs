using GalaSoft.MvvmLight;
using System.Threading.Tasks;

namespace ALP.ViewModel
{
    /// <summary>
    /// BaseViewModel, from which every other viewModel inherits
    /// </summary>
    public abstract class AlpViewModelBase : ViewModelBase
    {
        /// <summary>
        /// Used to run InitalizeAsync
        /// </summary>
        protected Task Initialization { get; set; }

        /// <summary>
        /// Bound to the loadingIndicator's visibility
        /// </summary>
        private bool isLoading = false;
        public bool IsLoading
        {
            get { return isLoading; }
            set
            {
                if (isLoading != value)
                {
                    Set(ref isLoading, value);
                }
            }
        }

        /// <summary>
        /// Used for async data inizalization
        /// </summary>
        protected abstract Task InitializeAsync();
    }
}
