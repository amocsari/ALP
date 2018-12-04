using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Service.Interface
{
    public interface IAlpLoggingService<T>
    {
        void LogDebug(string message);
        void LogDebug(string message, Exception e);
        void LogDebug(object o);
        void LogError(string message);
        void LogError(string message, Exception e);
        void LogInformation(string message);
        void LogInformation(string message, Exception e);
        void LogFatal(string message);
        void LogFatal(string message, Exception e);
    }
}
