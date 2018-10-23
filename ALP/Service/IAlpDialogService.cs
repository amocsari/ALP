using ALP.ViewModel;
using System.Windows;

namespace ALP.Service
{
    public interface IAlpDialogService
    {
        DialogResult<TReturnParameter> ShowDialog<TWindow, TViewModel, TParameter, TReturnParameter>(TParameter parameter) where TWindow : Window, new()
            where TViewModel : IDialogViewModel<TReturnParameter, TParameter>;
    }
}