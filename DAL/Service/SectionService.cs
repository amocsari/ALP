using DAL.Context;
using DAL.Entity;

namespace DAL.Service
{
    public class SectionService: ISectionService
    {
        private readonly IAlpContext _context;

        public SectionService(IAlpContext context)
        {
            _context = context;
        }
    }
}
