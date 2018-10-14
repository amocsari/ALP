using DAL.Entity;
using DAL.Context;

namespace DAL.Service
{
    public class ItemService: BaseService<Item>, IItemService
    {
        public ItemService(IAlpContext context)
        {
            _context = context;
        }
    }
}
