using System.Threading.Tasks;

namespace ALP.Service
{
    public interface IAlpApiService
    {
        Task<TResponse> GetAsync<TResponse>(string path);
        Task<TResponse> PostAsync<TParameter, TResponse>(string path, TParameter parameter);
        Task<bool> PostAsync<TParameter>(string path, TParameter parameter);
        Task<TResponse> DeleteAsync<TResponse>(string path, int id);
        Task<bool> DeleteAsync(string path, int id);
    }
}
