using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Service
{
    public class ApiService : IApiService
    {
        private static readonly Uri baseAddress = new Uri("http://localhost:1707/api/");

        public async Task<TResponse> DeleteAsync<TResponse>(string path, int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = baseAddress;
                client.DefaultRequestHeaders.Clear();

                var response = await client.DeleteAsync(path + "/" + id.ToString());

                if (!response.IsSuccessStatusCode)
                    //TODO: exception
                    throw new Exception();

                return await response.Content.ReadAsAsync<TResponse>();
            }
        }

        public async Task<bool> DeleteAsync(string path, int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = baseAddress;
                client.DefaultRequestHeaders.Clear();

                var response = await client.DeleteAsync(path + "/" + id.ToString());
                return response.IsSuccessStatusCode;
            }
        }

        public async Task<TResponse> GetAsync<TResponse>(string path)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = baseAddress;
                client.DefaultRequestHeaders.Clear();

                var response = await client.GetAsync(path);

                if (!response.IsSuccessStatusCode)
                    //TODO: exception
                    throw new Exception();

                return await response.Content.ReadAsAsync<TResponse>();
            }
        }

        public async Task<TResponse> PostAsync<TResponse, TParameter>(string path, TParameter parameter)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = baseAddress;
                client.DefaultRequestHeaders.Clear();

                var response = await client.PostAsJsonAsync(path, parameter);

                if (!response.IsSuccessStatusCode)
                    //TODO: exception
                    throw new Exception();

                return await response.Content.ReadAsAsync<TResponse>();
            }
        }

        public async Task<bool> PostAsync<TParameter>(string path, TParameter parameter)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = baseAddress;
                client.DefaultRequestHeaders.Clear();

                var response = await client.PostAsJsonAsync(path, parameter);

                return response.IsSuccessStatusCode;
            }
        }
    }
}
