using API.Service.Interface;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace API.Service
{
    /// <summary>
    /// Handley encryption operation
    /// </summary>
    public class EncryptionService: IEncryptionService
    {
        private readonly IDataProtector _protector;
        private readonly ILogger<EncryptionService> _logger;

        public EncryptionService(IDataProtectionProvider provider, ILogger<EncryptionService> logger)
        {
            _protector = provider.CreateProtector(GetType().FullName);
            _logger = logger;
        }

        /// <summary>
        /// encrypts generic data
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public string Encrypt<T>(T data)
        {
            _logger.LogDebug(new
            {
                action = nameof(Encrypt)
            }.ToString());

            var dataString = JsonConvert.SerializeObject(data);
            var protectedDataString = _protector.Protect(dataString);
            return protectedDataString;
        }

        /// <summary>
        /// decrypts generic data
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="protectedDataString"></param>
        /// <returns></returns>
        public T Decrypt<T>(string protectedDataString)
        {
            _logger.LogDebug(new
            {
                action = nameof(Decrypt)
            }.ToString());

            var dataString = _protector.Unprotect(protectedDataString);
            var data = JsonConvert.DeserializeObject<T>(dataString);
            return data;
        }
    }
}