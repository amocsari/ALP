using DAL.Entity;
using DAL.Context;

namespace DAL.Service
{
    public class RoleService: BaseService<Role>, IRoleService
    {
        public RoleService(IAlpContext context)
        {
            _context = context;
        }
    }
}
