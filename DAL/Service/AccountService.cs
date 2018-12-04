using DAL.Context;
using Microsoft.Extensions.Logging;

namespace DAL.Service
{
    public class AccountService: IAccountService
    {
        private readonly IAlpContext _context;
        private readonly ILogger<AccountService> _logger;

        public AccountService(IAlpContext context, ILogger<AccountService> logger)
        {
            _context = context;
            _logger = logger;
        }
    }
}
