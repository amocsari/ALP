using DAL.Context;
using DAL.Entity;

namespace DAL.Service
{
    public class OperationTypeService: IOperationTypeService
    {
        private readonly IAlpContext _context;

        public OperationTypeService(IAlpContext context)
        {
            _context = context;
        }
    }
}
