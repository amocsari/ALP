using DAL.Context;
using DAL.Entity;

namespace DAL.Service
{
    public class OperationTypeService: BaseService<OperationType>, IOperationTypeService
    {
        public OperationTypeService(IAlpContext context)
        {
            _context = context;
        }
    }
}
