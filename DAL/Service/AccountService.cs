using DAL.Context;
using DAL.Entity;

namespace DAL.Service
{
    public class AccountService: BaseService<Account>, IAccountService
    {
        public AccountService(IAlpContext context)
        {
            _context = context;
        }
        
        public void AddAccount()
        {
            _context.SaveChanges();
        }
    }
}
