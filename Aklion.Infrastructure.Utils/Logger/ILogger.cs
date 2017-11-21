using System;

namespace Aklion.Infrastructure.Utils.Logger
{
    public interface ILogger
    {
        void LogInfo(string message, int userId = 0, object data = null);

        void LogInfoException(string message, Exception exception, int userId = 0, object data = null);

        void LogWarning(string message, int userId = 0, object data = null);

        void LogWarningException(string message, Exception exception, int userId = 0, object data = null);

        void LogError(string message, int userId = 0, object data = null);

        void LogErrorException(string message, Exception exception, int userId = 0, object data = null);
    }
}