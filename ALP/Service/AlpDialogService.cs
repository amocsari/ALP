using System.Windows;
using ALP.View.Lookup;
using ALP.View.Lookup.Building;
using ALP.View.Lookup.Department;
using ALP.View.Lookup.Floor;
using ALP.View.Lookup.ItemType;
using ALP.View.Lookup.Section;
using ALP.ViewModel;
using Common.Model.Dto;

namespace ALP.Service
{
    public class AlpDialogService : IAlpDialogService
    {
        private Window GetLookupWindowByDto(LookupDtoBase dtoBase)
        {
            if (dtoBase is LocationDto)
                return new LocationEditorWindow();
            if (dtoBase is BuildingDto)
                return new BuildingEditorWindow();
            if(dtoBase is FloorDto)
                return new FloorEditorWindow();
            if(dtoBase is ItemNatureDto)
                return new ItemNatureEditorWindow();
            if(dtoBase is ItemStateDto)
                return new ItemStateEditorWindow();
            if(dtoBase is ItemTypeDto)
                return new ItemTypeEditorWindow();
            if (dtoBase is SectionDto)
                return new SectionEditorWindow();
            if (dtoBase is DepartmentDto)
                return new DepartmentEditorWindow();
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
