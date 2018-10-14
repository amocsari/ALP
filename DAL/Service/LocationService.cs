﻿using DAL.Context;
using DAL.Entity;

namespace DAL.Service
{
    public class LocationService : BaseService<Location>, ILocationService
    {
        public LocationService(IAlpContext context)
        {
            _context = context;
        }
    }
}
