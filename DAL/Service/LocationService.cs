using System.Collections.Generic;
using System.Linq;
using DAL.Context;
using DAL.Entity;

namespace DAL.Service
{
    public class LocationService : ILocationService
    {
        private readonly IAlpContext _context;

        public LocationService(IAlpContext context)
        {
            _context = context;
        }

        public void AddLocation()
        {
            _context.Location.Add(new Location
            {
                LocationName = "Budapest"
            });
            _context.SaveChanges();
        }

        public List<Location> GetAllLocations()
        {
            return _context.Location.ToList();
        }
    }
}
