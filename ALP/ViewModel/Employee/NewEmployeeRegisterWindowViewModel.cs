using ALP.Service;
using ALP.Service.Interface;
using GalaSoft.MvvmLight.Command;
using Model.Model;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ALP.ViewModel.Employee
{
    /// <summary>
    /// Registers a new account for an employee
    /// </summary>
    public class NewEmployeeRegisterWindowViewModel : AlpViewModelBase, IDialogViewModel<object, int>
    {
        /// <summary>
        /// employeeid as parameter
        /// </summary>
        public int Parameter { get; set; }
        public object ReturnParameter { get; set; }

        /// <summary>
        /// injected services
        /// </summary>
        private readonly IAccountApiService _accountApiService;
        private readonly IAlpDialogService _dialogService;

        /// <summary>
        /// command
        /// </summary>
        public ICommand CancelCommand { get; private set; }

        public NewEmployeeRegisterWindowViewModel(IAccountApiService accountApiService, IAlpDialogService dialogService)
        {
            _accountApiService = accountApiService;
            _dialogService = dialogService;

            CancelCommand = new RelayCommand<Window>(OnCancelCommand);

        }

        /// <summary>
        /// closes the window
        /// </summary>
        /// <param name="window"></param>
        private void OnCancelCommand(Window window)
        {
            window.Close();
        }

        /// <summary>
        /// sends a registration request to the server
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="newPassword"></param>
        /// <param name="newPasswordRe"></param>
        /// <returns></returns>
        public async Task RegisterUser(string userName, string newPassword, string newPasswordRe)
        {
            try
            {
                IsLoading = true;
                if (newPassword != newPasswordRe)
                {
                    throw new Exception("A két jelszó értéke nem egyezik meg!");
                }

                var registerAccountRequest = new RegisterAccountRequest
                {
                    EmployeeId = Parameter,
                    Password = newPassword,
                    Username = userName
                };

                await _accountApiService.RegisterAccount(registerAccountRequest);

                                
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
            throw new System.NotImplementedException();
        }
    }
}
