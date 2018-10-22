using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Model;

namespace ALP.API
{
    public class LocationService : ILocationService
    {
        public async Task AddLocation(LocationDto location)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:1707/");
                client.DefaultRequestHeaders.Accept.Clear();
                var response = await client.PostAsJsonAsync("api/Location/AddLocation", location);

                if (response.IsSuccessStatusCode)
                {
                    //return await response.Content.ReadAsAsync<List<LocationDto>>();
                }
                else
                {
                    //TODO: create exception
                    throw new Exception();
                }
            }
        }

        public async Task<List<LocationDto>> GetAllLocations()
        {
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:1707/");
                client.DefaultRequestHeaders.Accept.Clear();

                var response = await client.GetAsync("api/Location/GetAllLocations");

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<List<LocationDto>>();
                }
                else
                {
                    //TODO: create exception
                    throw new Exception();
                }
            }
        }


    }
}
