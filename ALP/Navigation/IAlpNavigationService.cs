using GalaSoft.MvvmLight.Views;

namespace ALP.Navigation
{
    /// <summary>
    /// Handles the navigation of pages inside the mainFrame node of the MainWindow
    /// </summary>
    public interface IAlpNavigationService : INavigationService
    {
        /// <summary>
        /// The Navigation Parameter
        /// </summary>
        object Parameter { get; }
    }
}
