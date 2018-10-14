using DAL.Context;
using DAL.Entity;

namespace DAL.Service
{
    public class DepartmentService: BaseService<Department>, IDepartmentService
    {
        public DepartmentService(IAlpContext context)
        {
            _context = context;
        }
    }
}
