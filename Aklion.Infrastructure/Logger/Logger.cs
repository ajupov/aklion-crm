using System;

namespace Aklion.Infrastructure.Logger
{
    public class Logger : ILogger
    {
        public void Info(string tag, string message = null, object data = null, Exception exception = null)
        {
        }

        public void Warning(string tag, string message = null, object data = null, Exception exception = null)
        {
        }

        public void Error(string tag, string message = null, object data = null, Exception exception = null)
        {
        }
    }
}