using System;
using DAL.Service;
using Microsoft.AspNetCore.Mvc;

namespace DAL.Controller
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public void AddAccount()
        {
            _accountService.AddAccount();
        }
    }
}