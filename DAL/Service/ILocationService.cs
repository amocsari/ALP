using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Entity;

namespace DAL.Service
{
    public interface ILocationService
    {
        void AddLocation();
        List<Location> GetAllLocations();
    }
}
