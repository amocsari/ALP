using DAL.Context;
using DAL.Entity;

namespace DAL.Service
{
    public class DepartmentService: IDepartmentService
    {
        private readonly IAlpContext _context;

        public DepartmentService(IAlpContext context)
        {
            _context = context;
        }
    }
}
