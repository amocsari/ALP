using ALP.Service;
using ALP.Service.Interface;
using GalaSoft.MvvmLight.Command;
using Model.Model.Dto;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ALP.ViewModel
{
    public class PasswordChangeWindowViewModel : AlpViewModelBase, IDialogViewModel<object, object>
    {
        public object Parameter { get; set; }
        public object ReturnParameter { get; set; }


        private readonly IAccountApiService _accountApiService;
        private readonly IAlpDialogService _dialogService;

        public ICommand CancelCommand { get; private set; }

        public PasswordChangeWindowViewModel(IAccountApiService accountApiService, IAlpDialogService dialogService)
        {
            _accountApiService = accountApiService;
            _dialogService = dialogService;

            CancelCommand = new RelayCommand<Window>(OnCancelCommand);
        }

        private void OnCancelCommand(Window window)
        {
            window.Close();
        }

        public async Task ChangePassword(string oldPassword, string newPassword, string newPasswordRe)
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
            }
            catch (Exception e)
            {
                _dialogService.ShowWarning(e.Message);
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
