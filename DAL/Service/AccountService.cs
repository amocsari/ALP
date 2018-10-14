using DAL.Context;

namespace DAL.Service
{
    public class AccountService: IAccountService
    {
        private readonly IAlpContext _context;

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
