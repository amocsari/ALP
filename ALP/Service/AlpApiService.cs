using Model.Model;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ALP.Service
{
    /// <summary>
    /// Used to send http requests to the server
    /// </summary>
    public class AlpApiService : IAlpApiService
    {
        /// <summary>
        /// The server's address
        /// Needs to be updated
        /// </summary>
        private static readonly Uri baseAddress = new Uri("http://localhost:1707/api/");

        /// <summary>
        /// Sends a DELETE request to the server
        /// Returns the parsed content of the response
        /// </summary>
        /// <typeparam name="TResponse">Expected type of the response</typeparam>
        /// <param name="path">path of the deletable resource</param>
        /// <param name="id">Identification of the deletable resource</param>
        /// <returns>Content of the server's response</returns>
        public async Task<TResponse> DeleteAsync<TResponse>(string path, int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = baseAddress;
                client.DefaultRequestHeaders.Clear();

                var response = await client.DeleteAsync(path + "/" + id.ToString());

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Error during DELETE request!");
                }

                var apiResponse = await response.Content.ReadAsAsync<AlpApiResponse<TResponse>>();
                if (!apiResponse.Success)
                {
                    throw new Exception(apiResponse.Message);
                }

                return apiResponse.Value;
            }
        }

        /// <summary>
        /// Sends a DELETE request to the server
        /// Returns a boolean depending on the success of the request
        /// </summary>
        /// <param name="path">the path of the deletable resource</param>
        /// <param name="id">the identification of the deletable resource</param>
        /// <returns>The success of the deletion</returns>
        public async Task<bool> DeleteAsync(string path, int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = baseAddress;
                client.DefaultRequestHeaders.Clear();

                var response = await client.DeleteAsync(path + "/" + id.ToString());

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Error during DELETE request!");
                }

                var apiResponse = await response.Content.ReadAsAsync<AlpApiResponse>();
                if (!apiResponse.Success)
                {
                    throw new Exception(apiResponse.Message);
                }

                return apiResponse.Success;
            }
        }

        /// <summary>
        /// Sends a GET request to the server
        /// </summary>
        /// <typeparam name="TResponse">The expected type of the response</typeparam>
        /// <param name="path">The path of the desired resource</param>
        /// <returns>The content of the response, desired resource</returns>
        public async Task<TResponse> GetAsync<TResponse>(string path)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = baseAddress;
                client.DefaultRequestHeaders.Clear();

                var response = await client.GetAsync(path);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Error during GET request!");
                }

                var apiResponse = await response.Content.ReadAsAsync<AlpApiResponse<TResponse>>();
                if (!apiResponse.Success)
                {
                    throw new Exception(apiResponse.Message);
                }

                return apiResponse.Value;
            }
        }

        /// <summary>
        /// Sends a POST request to the server
        /// Returns the parsed content of the response
        /// </summary>
        /// <typeparam name="TParameter">The type of the sent parameter</typeparam>
        /// <typeparam name="TResponse">The expected type of the response</typeparam>
        /// <param name="path">The path of the desired resource</param>
        /// <param name="parameter">The sent parameter</param>
        /// <returns>The parsed content of the response</returns>
        public async Task<TResponse> PostAsync<TParameter, TResponse>(string path, TParameter parameter)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = baseAddress;
                client.DefaultRequestHeaders.Clear();

                var response = await client.PostAsJsonAsync(path, parameter);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Error during POST request!");
                }
                
                var apiResponse = await response.Content.ReadAsAsync<AlpApiResponse<TResponse>>();
                if (!apiResponse.Success)
                {
                    throw new Exception(apiResponse.Message);
                }

                return apiResponse.Value;
            }
        }

        /// <summary>
        /// Sends a POST request to the server
        /// Returns a bool depending on the result of the request
        /// </summary>
        /// <typeparam name="TParameter">The type of the sent parameter</typeparam>
        /// <param name="path">The path of the desired resource</param>
        /// <param name="parameter">The sent parameter</param>
        /// <returns>The success of the request</returns>
        public async Task<bool> PostAsync<TParameter>(string path, TParameter parameter)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = baseAddress;
                client.DefaultRequestHeaders.Clear();

                var response = await client.PostAsJsonAsync(path, parameter);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Error during POST request!");
                }

                var apiResponse = await response.Content.ReadAsAsync<AlpApiResponse>();
                if (!apiResponse.Success)
                {
                    throw new Exception(apiResponse.Message);
                }

                return apiResponse.Success;
            }
        }
    }
}
