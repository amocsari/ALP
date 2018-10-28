using Model.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ALP.Service
{
    public interface ILocationApiService
    {
        Task<List<LocationDto>> GetAllLocations();
        Task<LocationDto> AddLocation(LocationDto location);
        Task<LocationDto> UpdateLocation(LocationDto location);
        Task ToggleLocationLockStateById(int locationId);
    }
}
