using System;

namespace Aklion.Infrastructure.Logger
{
    public class Logger : ILogger
    {
        public void LogInfo(string message, int userId = 0, object data = null)
        {
        }

        public void LogInfoException(string message, Exception exception, int userId = 0, object data = null)
        {
        }

        public void LogWarning(string message, int userId = 0, object data = null)
        {
        }

        public void LogWarningException(string message, Exception exception, int userId = 0, object data = null)
        {
        }

        public void LogError(string message, int userId = 0, object data = null)
        {
        }

        public void LogErrorException(string message, Exception exception, int userId = 0, object data = null)
        {
        }
    }
}