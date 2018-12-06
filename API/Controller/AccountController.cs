using API.Service;
using Microsoft.AspNetCore.Mvc;
using Model.Model;
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
        public Task<AlpApiResponse<User>> Login([FromBody] LoginData loginData)
        {
            return _accountService.Login(loginData.Username, loginData.Password);
        }
    }
}