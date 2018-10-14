using DAL.Context;
using DAL.Entity;

namespace DAL.Service
{
    public class ItemTypeService: BaseService<ItemType>, IItemTypeService
    {
        public ItemTypeService(IAlpContext context)
        {
            _context = context;
        }
    }
}
