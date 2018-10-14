using DAL.Context;
using DAL.Entity;

namespace DAL.Service
{
    public class SectionService: BaseService<Section>, ISectionService
    {
        public SectionService(IAlpContext context)
        {
            _context = context;
        }
    }
}
