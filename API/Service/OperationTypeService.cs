using DAL.Context;
using Microsoft.Extensions.Logging;

namespace API.Service
{
    public class OperationTypeService: IOperationTypeService
    {
        private readonly IAlpContext _context;
        private readonly ILogger<OperationTypeService> _logger;

        public OperationTypeService(IAlpContext context, ILogger<OperationTypeService> logger)
        {
            _context = context;
            _logger = logger;
        }
    }
}
