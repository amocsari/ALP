using DAL.Entity;
using DAL.Context;

namespace DAL.Service
{
    public class RoleService: IRoleService
    {
        private readonly IAlpContext _context;

        public RoleService(IAlpContext context)
        {
            _context = context;
        }
    }
}
