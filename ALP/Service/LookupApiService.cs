using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ALP.Service.Interface;
using Common.Model.Dto;

namespace ALP.Service
{
    /// <summary>
    /// Used to make server calls about lookup dtos
    /// </summary>
    /// <typeparam name="T">Dto type</typeparam>
    public class LookupApiService<T> : ILookupApiService<T> where T : LookupDtoBase
    {
        /// <summary>
        /// Service that makes the server calls
        /// </summary>
        private readonly IAlpApiService _apiService;
        private readonly IAlpLoggingService<LookupApiService<T>> _loggingService;

        /// <summary>
        /// Used to create the prefix of the path from the dto type.
        /// </summary>
        private const string dtoPostFix = "Dto";
        private string ControllerPrefix { get => typeof(T).Name.Replace(dtoPostFix,""); }

        /// <summary>
        /// Constructor
        /// Sets the injected service
        /// </summary>
        /// <param name="apiService">injected service</param>
        public LookupApiService(IAlpApiService apiService, IAlpLoggingService<LookupApiService<T>> loggingService)
        {
            _apiService = apiService;
            _loggingService = loggingService;
        }

        /// <summary>
        /// Sends a request to the server to create a new database entry from the sent dto
        /// </summary>
        /// <param name="dto">dto to create a database entry from</param>
        /// <returns>dto of the inserted entry</returns>
        public async Task<bool> AddNew(T dto)
        {
            _loggingService.LogDebug(new
            {
                action = nameof(AddNew),
                dto = dto.ToString()
            });

            return await _apiService.PostAsync<T>(CreateUrl(nameof(AddNew)), dto);
        }

        /// <summary>
        /// Sends a request to retrieve all entries of the given dto from the database
        /// </summary>
        /// <returns>List containing all the dtos, which have corresponding database entries</returns>
        public async Task<List<T>> GetAll()
        {
            _loggingService.LogDebug(new
            {
                action = nameof(GetAll)
            });

            return await _apiService.GetAsync<List<T>>(CreateUrl(nameof(GetAll)));
        }

        /// <summary>
        /// Sends a request to retrieve all available entries of the given dto from the database
        /// An entry is available if it is not locked
        /// </summary>
        /// <returns>List of available dtos, which have corresponding database entries</returns>
        public async Task<List<T>> GetAvailable()
        {
            _loggingService.LogDebug(new
            {
                action = nameof(GetAvailable)
            });

            return await _apiService.GetAsync<List<T>>(CreateUrl(nameof(GetAvailable)));
        }

        /// <summary>
        /// Sends a request to the server to change the identified entry's lockstate to its inverse
        /// </summary>
        /// <param name="dtoId">Dto's id</param>
        public async Task ToggleLockStateById(int dtoId)
        {
            _loggingService.LogDebug(new
            {
                action = nameof(ToggleLockStateById),
                 dtoId
            });

            await _apiService.PostAsync(CreateUrl(nameof(ToggleLockStateById)), dtoId);
        }

        /// <summary>
        /// Sends a request to update the database entry according to the dto parameter
        /// </summary>
        /// <param name="dto">The dto, by which the update is requested</param>
        /// <returns>The updated entry's dto</returns>
        public async Task<T> Update(T dto)
        {
            _loggingService.LogDebug(new
            {
                action = nameof(Update),
                dto = dto.ToString()
            });

            return await _apiService.PostAsync<T, T>(CreateUrl(nameof(Update)), dto);
        }

        /// <summary>
        /// Creates the resource path from the called method's name and the dto's type
        /// </summary>
        /// <param name="actionName">Method's name</param>
        /// <returns>resource path</returns>
        private string CreateUrl(string actionName)
        {
            StringBuilder builder = new StringBuilder(ControllerPrefix);
            builder.Append("/");
            builder.Append(actionName);
            builder.Append(ControllerPrefix);
            return builder.ToString();
        }
    }
}
