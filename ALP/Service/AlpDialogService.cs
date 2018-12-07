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
    /// <summary>
    /// Handles the showing of dialogs to the user
    /// </summary>
    public class AlpDialogService : IAlpDialogService
    {
        private readonly IAlpLoggingService<AlpDialogService> _loggingService;

        public AlpDialogService(IAlpLoggingService<AlpDialogService> loggingService)
        {
            _loggingService = loggingService;
        }

        /// <summary>
        /// Returns the editorwindow corresponding to the dto
        /// </summary>
        /// <param name="dtoBase"></param>
        /// <returns></returns>
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

        /// <summary>
        /// shows the editor window of the given dto
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dto"></param>
        /// <returns></returns>
        public DialogResult<T> ShowDtoEditorWindow<T>(T dto) where T : LookupDtoBase, new()
        {
            _loggingService.LogDebug(new
            {
                action = nameof(ShowDtoEditorWindow),
                dto = dto?.ToString()
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

        /// <summary>
        /// Shows a custom window that implements IDialogViewModel
        /// Can have a parameter and a return parameter as well
        /// </summary>
        /// <typeparam name="TWindow"></typeparam>
        /// <typeparam name="TViewModel"></typeparam>
        /// <typeparam name="TParameter"></typeparam>
        /// <typeparam name="TReturnParameter"></typeparam>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public DialogResult<TReturnParameter> ShowDialog<TWindow, TViewModel, TParameter, TReturnParameter>(TParameter parameter) where TWindow : Window, new()
                                                                                                                    where TViewModel : IDialogViewModel<TReturnParameter, TParameter>
        {
            _loggingService.LogDebug(new
            {
                action = nameof(ShowDialog),
                parameter = parameter?.ToString()
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

        /// <summary>
        /// Opens a confirmdialog with texts received in the parameters
        /// </summary>
        /// <param name="message"></param>
        /// <param name="title"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Opens an alert window with texts received in the parameters
        /// </summary>
        /// <param name="message"></param>
        /// <param name="title"></param>
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

        /// <summary>
        /// Opens an warning window with texts received in the parameters
        /// </summary>
        /// <param name="message"></param>
        /// <param name="title"></param>
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

        /// <summary>
        /// Opens an error window with texts received in the parameters
        /// </summary>
        /// <param name="message"></param>
        /// <param name="title"></param>
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
