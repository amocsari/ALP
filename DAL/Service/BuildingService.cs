using DAL.Entity;
using DAL.Context;

namespace DAL.Service
{
    public class BuildingService: BaseService<Building>, IBuildingService
    {
        public BuildingService(IAlpContext context)
        {
            _context = context;
        }
    }
}
