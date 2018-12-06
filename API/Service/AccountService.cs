using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DAL.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Model.Model;

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

        public async Task<AlpApiResponse<User>> Login(string username, string password)
        {
            var response = new AlpApiResponse<User>();
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

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes("afasdfasd");
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, user.AccountId.ToString()),
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                user.Token = tokenHandler.WriteToken(token);
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
    }
}
