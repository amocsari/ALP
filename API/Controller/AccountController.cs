using API.Service;
using Microsoft.AspNetCore.Mvc;
using Model.Model;
using Model.Model.Dto;
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
            var a = HttpContext.Request.Headers;
            var sessionToken = HttpContext.Request.Headers["sessiontoken"];
            return _accountService.ChangePassword(changePasswordRequest, sessionToken);
        }
    }
}