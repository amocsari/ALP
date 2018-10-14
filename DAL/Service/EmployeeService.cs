using DAL.Context;
using DAL.Entity;

namespace DAL.Service
{
    public class EmployeeService: BaseService<Employee>, IEmployeeService
    {
        public EmployeeService(IAlpContext context)
        {
            _context = context;
        }
    }
}
