using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ALP.ViewModel;
using GalaSoft.MvvmLight.Views;

namespace ALP.Service
{
    public class AlpDialogService: IAlpDialogService
    {
        public DialogResult<TReturnParameter> ShowDialog<TWindow, TViewModel, TParameter, TReturnParameter>(TParameter parameter) where TWindow : Window, new()
                                                                                                                    where TViewModel : IDialogViewModel<TReturnParameter, TParameter>
        {
            var window = new TWindow();
            ((TViewModel) window.DataContext).Parameter = parameter;
            if (window.ShowDialog() == true)
            {
                return new DialogResult<TReturnParameter>()
                {
                    Value = ((TViewModel) window.DataContext).ReturnParameter,
                    Accepted = true,
                };
            }

            return new DialogResult<TReturnParameter>
            {
                Accepted = false,
            };
        }
    }
}
