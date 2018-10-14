using DAL.Context;
using DAL.Entity;

namespace DAL.Service
{
    public class FloorService: BaseService<Floor>, IFloorService
    {
        public FloorService(IAlpContext context)
        {
            _context = context;
        }
    }
}
