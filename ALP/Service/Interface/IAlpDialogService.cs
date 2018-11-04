using ALP.ViewModel;
using Common.Model.Dto;
using System.Windows;

namespace ALP.Service
{
    public interface IAlpDialogService
    {
        DialogResult<T> ShowDtoEditorWindow<T>(T dto) where T : LookupDtoBase, new();

        DialogResult<TReturnParameter> ShowDialog<TWindow, TViewModel, TParameter, TReturnParameter>(TParameter parameter) where TWindow : Window, new()
            where TViewModel : IDialogViewModel<TReturnParameter, TParameter>;
    }
}