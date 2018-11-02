using GalaSoft.MvvmLight;
using System.Threading.Tasks;

namespace ALP.ViewModel
{
    public abstract class AlpViewModelBase : ViewModelBase
    {
        protected Task Initialization { get; set; }

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

        protected abstract Task InitializeAsync();
    }
}
