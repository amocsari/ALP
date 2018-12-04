using System.Windows;
using ALP.Service.Interface;
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
        private readonly IAlpLoggingService<AlpDialogService> _loggingService;

        public AlpDialogService(IAlpLoggingService<AlpDialogService> loggingService)
        {
            _loggingService = loggingService;
        }

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
            _loggingService.LogDebug(new
            {
                action = nameof(ShowDtoEditorWindow),
                dto = dto.ToString()
            });

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
            _loggingService.LogDebug(new
            {
                action = nameof(ShowDialog),
                parameter = parameter.ToString()
            });

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

        public bool ShowConfirmDialog(string message, string title)
        {
            _loggingService.LogDebug(new
            {
                action = nameof(ShowConfirmDialog),
                message,
                title
            });

            MessageBoxResult result =  MessageBox.Show(message, title, MessageBoxButton.YesNo, MessageBoxImage.Question);

            return result == MessageBoxResult.Yes;
        }

        public void ShowAlert(string message, string title)
        {
            _loggingService.LogDebug(new
            {
                action = nameof(ShowAlert),
                message,
                title
            });

            MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.None);
        }

        public void ShowWarning(string message, string title)
        {
            _loggingService.LogDebug(new
            {
                action = nameof(ShowWarning),
                message,
                title
            });

            MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        public void ShowError(string message, string title)
        {
            _loggingService.LogDebug(new
            {
                action = nameof(ShowError),
                message,
                title
            });

            MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
