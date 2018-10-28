using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Dto;

namespace ALP.Service
{
    public class BuildingApiService: IBuildingApiService
    {
        private readonly IApiService _apiService;

        public BuildingApiService(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<List<BuildingDto>> GetAllBuildings()
        {
            return await _apiService.GetAsync<List<BuildingDto>>("Building/GetAllBuildings");
        }
    }
}
