using DAL.Entity;
using DAL.Context;

namespace DAL.Service
{
    public class ItemNatureService: BaseService<ItemNature>, IItemNatureService
    {
        public ItemNatureService(IAlpContext context)
        {
            _context = context;
        }
    }
}
