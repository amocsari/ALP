using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALP.API
{
    public interface ILocationService
    {
        Task<List<LocationDto>> GetAllLocations();
        Task<LocationDto> AddLocation(LocationDto location);
        Task<LocationDto> UpdateLocation(LocationDto location);
    }
}
