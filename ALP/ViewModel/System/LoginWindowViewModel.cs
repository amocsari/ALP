﻿using GalaSoft.MvvmLight;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using System.Windows;
using ALP.Service.Interface;
using Model.Model;
using System.Threading.Tasks;
using ALP.Service;
using System;

namespace ALP.ViewModel
{
    public class LoginWindowViewModel : AlpViewModelBase, IDialogViewModel<SessionData, object>
    {
        public SessionData ReturnParameter { get; set; }
        public object Parameter { get; set; }

        private readonly IAccountApiService _accountApiService;
        private readonly IAlpDialogService _dialogService;
        
        public ICommand CancelCommand { get; private set; }

        public LoginWindowViewModel(IAccountApiService accountApiService, IAlpDialogService dialogService)
        {
            _accountApiService = accountApiService;
            _dialogService = dialogService;
            
            CancelCommand = new RelayCommand<Window>(OnCancelCommand);
        }

        private void OnCancelCommand(Window window)
        {
            window.Close();
        }

        public async Task Login(string username, string password)
        {
            try
            {
                IsLoading = true;
                ReturnParameter = await _accountApiService.Login(username, password);
            }catch(Exception e)
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
