using DAL.Context;
using Microsoft.Extensions.Logging;

namespace API.Service
{
    public class OperationService: IOperationService
    {
        private readonly IAlpContext _context;
        private readonly ILogger<OperationService> _logger;

        public OperationService(IAlpContext context, ILogger<OperationService> logger)
        {
            _context = context;
            _logger = logger;
        }
    }
}
