using ALP.Service;
using ALP.Service.Interface;
using GalaSoft.MvvmLight.Command;
using Model.Model;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ALP.ViewModel
{
    /// <summary>
    /// Handles the backgroundwork of password change window
    /// </summary>
    public class PasswordChangeWindowViewModel : AlpViewModelBase, IDialogViewModel<object, object>
    {
        /// <summary>
        /// not used parameters
        /// </summary>
        public object Parameter { get; set; }
        public object ReturnParameter { get; set; }


        /// <summary>
        /// injected services
        /// </summary>
        private readonly IAccountApiService _accountApiService;
        private readonly IAlpDialogService _dialogService;

        /// <summary>
        /// Command
        /// </summary>
        public ICommand CancelCommand { get; private set; }

        public PasswordChangeWindowViewModel(IAccountApiService accountApiService, IAlpDialogService dialogService)
        {
            _accountApiService = accountApiService;
            _dialogService = dialogService;

            CancelCommand = new RelayCommand<Window>(OnCancelCommand);
        }

        /// <summary>
        /// Closes the window
        /// </summary>
        /// <param name="window"></param>
        private void OnCancelCommand(Window window)
        {
            window.Close();
        }

        /// <summary>
        /// checks if data is valid, then sends a password change request
        /// </summary>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <param name="newPasswordRe"></param>
        /// <returns></returns>
        public async Task<bool> ChangePassword(string oldPassword, string newPassword, string newPasswordRe)
        {
            try
            {
                IsLoading = true;
                if (newPassword != newPasswordRe)
                {
                    throw new Exception("A két új jelszó értéke nem egyezik meg!");
                }
                ChangePasswordRequest changePasswordRequest = new ChangePasswordRequest
                {
                    OldPassword = oldPassword,
                    NewPassword = newPassword
                };
                await _accountApiService.ChangePassword(changePasswordRequest);
                return true;
            }
            catch (Exception e)
            {
                _dialogService.ShowWarning(e.Message);
                return false;
            }
            finally
            {
                IsLoading = false;
            }
        }

        protected override Task InitializeAsync()
        {
            throw new NotImplementedException();
        }
    }
}
