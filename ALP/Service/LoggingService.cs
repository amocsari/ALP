using ALP.Service.Interface;
using log4net;
using System;

namespace ALP.Service
{
    /// <summary>
    /// Handles logging tasks
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AlpLoggingService<T>: IAlpLoggingService<T>
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(T));

        public void LogDebug(string message)
        {
            log.Debug(message);
        }
        public void LogDebug(string message, Exception e)
        {
            log.Debug(message, e);
        }

        public void LogError(string message)
        {
            log.Error(message);
        }

        public void LogError(string message, Exception e)
        {
            log.Error(message, e);
        }

        public void LogInformation(string message)
        {
            log.Info(message);
        }

        public void LogInformation(string message, Exception e)
        {
            log.Info(message, e);
        }

        public void LogFatal(string message)
        {
            log.Fatal(message);
        }

        public void LogFatal(string message, Exception e)
        {
            log.Fatal(message, e);
        }

        public void LogDebug(object o)
        {
            log.Debug(o.ToString());
        }
    }
}
