using API.Service;
using Microsoft.AspNetCore.Mvc;
using Model.Enum;
using Model.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Controller
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        public Task<AlpApiResponse<SessionData>> Login([FromBody] LoginData loginData)
        {
            return _accountService.Login(loginData);
        }

        [HttpPost]
        public Task<AlpApiResponse> Logout([FromBody]string encryptedSessionToken)
        {
            return _accountService.Logout(encryptedSessionToken);
        }

        [HttpPost]
        public Task<AlpApiResponse> ChangePassword([FromBody]ChangePasswordRequest changePasswordRequest)
        {
            var sessionToken = HttpContext.Request.Headers["sessiontoken"];
            return _accountService.ChangePassword(changePasswordRequest, sessionToken);
        }

        [HttpPost]
        public Task<AlpApiResponse> RegisterAccount([FromBody]RegisterAccountRequest registerAccountRequest)
        {
            var sessionToken = HttpContext.Request.Headers["sessiontoken"];
            if (!_accountService.Authorize(sessionToken, new List<RoleType> { RoleType.Admin }))
            {
                return Task.FromResult(new AlpApiResponse { Success = false, Message = "Nincs jogosultsága ehhez a művelethez!" });
            }
            return _accountService.RegisterAccount(registerAccountRequest);
        }
    }
}