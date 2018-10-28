using Model.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ALP.Service
{
    public interface IBuildingApiService
    {
        Task<List<BuildingDto>> GetAllBuildings();
    }
}
