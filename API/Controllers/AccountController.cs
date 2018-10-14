using System;
using System.Threading.Tasks;
using API.Entity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        [HttpGet]
        public User GetAllUsers()
        {
            return new User();
        }
    }
}