using ALP.Service.Interface;
using Model.Model;
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

        public async Task<SessionData> Login(string username, string password)
        {
            try
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
            catch (Exception e)
            {
                //TODO logging
            }

            return null;
        }
    }
}
