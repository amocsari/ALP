using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Context;
using DAL.Entity;
using Model;

namespace DAL.Service
{
    public class LocationService : BaseService<Location>, ILocationService
    {
        public LocationService(IAlpContext context)
        {
            _context = context;
        }

        public async Task<LocationDto> AddNewLocation(LocationDto location)
        {
            return (await InsertNew(location.DtoToEntity())).EntityToDto();
        }

        public async Task DeleteLocationById(int locationId)
        {
            await Remove(location => location.LocationID == locationId);
        }

        public async Task<List<LocationDto>> GetAllLocations()
        {
            return (await GetAll()).ToList().Select(location => location.EntityToDto()).ToList();
        }

        public async Task<LocationDto> GetLocationById(int locationId)
        {
            return (await GetSingle(location => location.LocationID == locationId)).EntityToDto();
        }

        public async Task UpdateLocation(LocationDto dto)
        {
            await Update(location => location.LocationID == dto.Id, dto.DtoToEntity());
        }

        Task<LocationDto> ILocationService.UpdateLocation(LocationDto location)
        {
            throw new System.NotImplementedException();
        }
    }
}
