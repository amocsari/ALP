using DAL.Context;
using Microsoft.Extensions.Logging;

namespace API.Service
{
    public class RoleService: IRoleService
    {
        private readonly IAlpContext _context;
        private readonly ILogger<RoleService> _logger;

        public RoleService(IAlpContext context, ILogger<RoleService> logger)
        {
            _context = context;
            _logger = logger;
        }
    }
}
