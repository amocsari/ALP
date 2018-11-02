using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ALP.View.Lookup;
using ALP.View.Lookup.Building;
using ALP.ViewModel;
using GalaSoft.MvvmLight.Views;
using Model.Dto;

namespace ALP.Service
{
    public class AlpDialogService : IAlpDialogService
    {
        private Window GetLookupWindowByDto(LookupDtoBase dtoBase)
        {
            if (dtoBase is LocationDto)
                return new LookupLocationEditorWindow();
            if (dtoBase is BuildingDto)
                return new LookupBuildingEditorWindow();
            return null;
        }

        public DialogResult<T> ShowDtoEditorWindow<T>(T dto) where T : LookupDtoBase, new()
        {
            var window = GetLookupWindowByDto(dto);
            ((LookupEditorWindowViewModel<T>)window.DataContext).Parameter = (T)dto.Copy();
            if (window.ShowDialog() == true)
            {
                return new DialogResult<T>()
                {
                    Value = ((LookupEditorWindowViewModel<T>)window.DataContext).ReturnParameter,
                    Accepted = true
                };
            }

            return new DialogResult<T>
            {
                Accepted = false,
            };
        }

        public DialogResult<TReturnParameter> ShowDialog<TWindow, TViewModel, TParameter, TReturnParameter>(TParameter parameter) where TWindow : Window, new()
                                                                                                                    where TViewModel : IDialogViewModel<TReturnParameter, TParameter>
        {
            var window = new TWindow();
            ((TViewModel)window.DataContext).Parameter = parameter;
            if (window.ShowDialog() == true)
            {
                return new DialogResult<TReturnParameter>()
                {
                    Value = ((TViewModel)window.DataContext).ReturnParameter,
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
