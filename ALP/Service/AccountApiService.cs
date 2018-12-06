using ALP.Service.Interface;
using Model.Model;
using Model.Model.Dto;
using System;
using System.Threading.Tasks;

namespace ALP.Service
{
    public class AccountApiService : IAccountApiService
    {
        private readonly IAlpApiService _apiService;
        private readonly IAlpLoggingService<AccountApiService> _loggingService;

        public AccountApiService(IAlpApiService apiService, IAlpLoggingService<AccountApiService> loggingService)
        {
            _apiService = apiService;
            _loggingService = loggingService;
        }

        public async Task ChangePassword(ChangePasswordRequest changePasswordRequest)
        {
            _loggingService.LogDebug(new
            {
                action = nameof(ChangePassword)
            });

            await _apiService.PostAsync("Account/ChangePassword", changePasswordRequest);
        }

        public async Task<SessionData> Login(string username, string password)
        {
            _loggingService.LogDebug(new
            {
                action = nameof(Login),
                username
            });

            LoginData loginData = new LoginData
            {
                Password = password,
                Username = username
            };
            var user = await _apiService.PostAsync<LoginData, SessionData>("Account/Login", loginData);
            return user;
        }

        public async Task Logout(string encryptedSessionToken)
        {
            _loggingService.LogDebug(new
            {
                action = nameof(Logout)
            });

            var user = await _apiService.PostAsync<string>("Account/Logout", encryptedSessionToken);
        }
    }
}
