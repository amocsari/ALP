using System;
using System.Threading.Tasks;
using DAL.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Model.Model;
using Newtonsoft.Json;
using System.Web.Security;

namespace API.Service
{
    public class AccountService : IAccountService
    {
        private readonly IAlpContext _context;
        private readonly ILogger<AccountService> _logger;

        public AccountService(IAlpContext context, ILogger<AccountService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<AlpApiResponse<string>> Login(string username, string password)
        {
            var response = new AlpApiResponse<string>();
            try
            {
                _logger.LogDebug(new
                {
                    action = nameof(Login),
                    username
                }.ToString());

                var user = await _context.Account.FirstOrDefaultAsync(account => account.UserName == username && account.Password == password);

                if (user == null)
                {
                    response.Success = false;
                    response.Message = "Hibás felhasználónév vagy jelszó!";
                }

                var session = new SessionTokenData
                {
                    SessionStart = DateTime.UtcNow,
                    UserName = user.UserName,
                    RoleId = user.RoleId,
                    AccountId = user.AccountId
                };


            }
            catch (Exception e)
            {
                _logger.LogError(new
                {
                    exception = e,
                    message = e.Message,
                    innerException = e,
                    innerExceptionMessage = e.InnerException?.Message
                }.ToString());
                response.Message = e.Message;
                response.Success = false;
            }

            return response;
        }

        private string EncriptSessionTokenData(SessionTokenData tokenData)
        {
            string unEncriptedToken = JsonConvert.SerializeObject(tokenData);
            EncryptString
        }
    }
}
