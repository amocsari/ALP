using DAL.Context;
using DAL.Entity;

namespace DAL.Service
{
    public class OperationService: BaseService<Operation>, IOperationService
    {
        public OperationService(IAlpContext context)
        {
            _context = context;
        }
    }
}
