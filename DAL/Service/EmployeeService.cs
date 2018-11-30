using DAL.Context;
using DAL.Entity;

namespace DAL.Service
{
    public class EmployeeService: IEmployeeService
    {
        private readonly IAlpContext _context;

        public EmployeeService(IAlpContext context)
        {
            _context = context;
        }
    }
}
