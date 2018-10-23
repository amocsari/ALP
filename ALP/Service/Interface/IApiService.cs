using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Service
{
    public interface IApiService
    {
        Task<TResponse> GetAsync<TResponse>(string path);
        Task<TResponse> PostAsync<TResponse, TParameter>(string path, TParameter parameter);
        Task<bool> PostAsync<TParameter>(string path, TParameter parameter);
        Task<TResponse> DeleteAsync<TResponse>(string path, int id);
        Task<bool> DeleteAsync(string path, int id);
    }
}
