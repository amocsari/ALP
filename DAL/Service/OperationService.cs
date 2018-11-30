using DAL.Context;
using DAL.Entity;

namespace DAL.Service
{
    public class OperationService: IOperationService
    {
        private readonly IAlpContext _context;

        public OperationService(IAlpContext context)
        {
            _context = context;
        }
    }
}
