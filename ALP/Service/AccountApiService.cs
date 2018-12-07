using ALP.Service.Interface;
using Model.Model;
using System.Threading.Tasks;

namespace ALP.Service
{
    /// <summary>
    /// sends account relates api requests
    /// </summary>
    public class AccountApiService : IAccountApiService
    {
        private readonly IAlpApiService _apiService;
        private readonly IAlpLoggingService<AccountApiService> _loggingService;

        public AccountApiService(IAlpApiService apiService, IAlpLoggingService<AccountApiService> loggingService)
        {
            _apiService = apiService;
            _loggingService = loggingService;
        }

        /// <summary>
        /// requests password change
        /// </summary>
        /// <param name="changePasswordRequest"></param>
        /// <returns></returns>
        public async Task ChangePassword(ChangePasswordRequest changePasswordRequest)
        {
            _loggingService.LogDebug(new
            {
                action = nameof(ChangePassword)
            });

            await _apiService.PostAsync("Account/ChangePassword", changePasswordRequest);
        }

        /// <summary>
        /// resuests login
        /// returns sessiontoken
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
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

        /// <summary>
        /// requests logout, removes sessiontoken
        /// </summary>
        /// <param name="encryptedSessionToken"></param>
        /// <returns></returns>
        public async Task Logout(string encryptedSessionToken)
        {
            _loggingService.LogDebug(new
            {
                action = nameof(Logout)
            });

            var user = await _apiService.PostAsync<string>("Account/Logout", encryptedSessionToken);
        }

        /// <summary>
        /// registers a new account
        /// </summary>
        /// <param name="registerAccountRequest"></param>
        /// <returns></returns>
        public async Task RegisterAccount(RegisterAccountRequest registerAccountRequest)
        {
            _loggingService.LogDebug(new
            {
                action = nameof(registerAccountRequest)
            });

            await _apiService.PostAsync("Account/RegisterAccount", registerAccountRequest);
        }
    }
}
