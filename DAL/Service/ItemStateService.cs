using DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Context;

namespace DAL.Service
{
    public class ItemStateService: BaseService<ItemState>, IItemStateService
    {
        public ItemStateService(IAlpContext context)
        {
            _context = context;
        }
    }
}
