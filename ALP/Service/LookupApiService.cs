using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ALP.Service;
using Model;
using Model.Dto;

namespace ALP.Service
{
    public class LookupApiService<T> : ILookupApiService<T> where T : LookupDtoBase
    {
        private readonly IApiService _apiService;
        private const string dtoPostFix = "Dto";
        private string ControllerPrefix { get => typeof(T).Name.Replace(dtoPostFix,""); }

        public LookupApiService(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<T> AddNew(T dto)
        {
            return await _apiService.PostAsync<T, T>(CreateUrl(nameof(AddNew)), dto);
        }

        public async Task<List<T>> GetAll()
        {
            return await _apiService.GetAsync<List<T>>(CreateUrl(nameof(GetAll)));
        }

        public async Task ToggleLockStateById(int dtoId)
        {
            await _apiService.PostAsync(CreateUrl(nameof(ToggleLockStateById)), dtoId);
        }

        public async Task<T> Update(T dto)
        {
            return await _apiService.PostAsync<T, T>(CreateUrl(nameof(Update)), dto);
        }

        private string CreateUrl(string action)
        {
            StringBuilder builder = new StringBuilder(ControllerPrefix);
            builder.Append("/");
            builder.Append(action);
            builder.Append(ControllerPrefix);
            return builder.ToString();
        }
    }
}
