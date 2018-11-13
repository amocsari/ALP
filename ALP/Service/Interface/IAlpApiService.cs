using System.Threading.Tasks;

namespace ALP.Service
{
    /// <summary>
    /// Used to send http requests to the server
    /// </summary>
    public interface IAlpApiService
    {
        /// <summary>
        /// The server's address
        /// Needs to be updated
        /// </summary>
        Task<TResponse> GetAsync<TResponse>(string path);

        /// <summary>
        /// Sends a DELETE request to the server
        /// Returns the parsed content of the response
        /// </summary>
        /// <typeparam name="TResponse">Expected type of the response</typeparam>
        /// <param name="path">path of the deletable resource</param>
        /// <param name="id">Identification of the deletable resource</param>
        /// <returns>Content of the server's response</returns>
        Task<TResponse> PostAsync<TParameter, TResponse>(string path, TParameter parameter);

        /// <summary>
        /// Sends a DELETE request to the server
        /// Returns a boolean depending on the success of the request
        /// </summary>
        /// <param name="path">the path of the deletable resource</param>
        /// <param name="id">the identification of the deletable resource</param>
        /// <returns>The success of the deletion</returns>
        Task<bool> PostAsync<TParameter>(string path, TParameter parameter);

        /// <summary>
        /// Sends a GET request to the server
        /// </summary>
        /// <typeparam name="TResponse">The expected type of the response</typeparam>
        /// <param name="path">The path of the desired resource</param>
        /// <returns>The content of the response, desired resource</returns>
        Task<TResponse> DeleteAsync<TResponse>(string path, int id);

        /// <summary>
        /// Sends a POST request to the server
        /// Returns the parsed content of the response
        /// </summary>
        /// <typeparam name="TParameter">The type of the sent parameter</typeparam>
        /// <typeparam name="TResponse">The expected type of the response</typeparam>
        /// <param name="path">The path of the desired resource</param>
        /// <param name="parameter">The sent parameter</param>
        /// <returns>The parsed content of the response</returns>
        Task<bool> DeleteAsync(string path, int id);
    }
}
