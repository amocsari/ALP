using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ALP.Service;
using Model;

namespace ALP.API
{
    public class LocationService : ILocationService
    {
        private readonly IApiService _apiService;

        public LocationService(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<bool> AddLocation(LocationDto location)
        {
            return await _apiService.PostAsync("Location/AddNewLocation", location);
        }

        public async Task<List<LocationDto>> GetAllLocations()
        {
            return await _apiService.GetAsync<List<LocationDto>>("Location/GetAllLocations");
        }


    }
}
