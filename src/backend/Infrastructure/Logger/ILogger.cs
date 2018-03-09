using System;

namespace Infrastructure.Logger
{
    public interface ILogger
    {
        void Trace(string tag, string message = null, object data = null, Exception exception = null);

        void Debug(string tag, string message = null, object data = null, Exception exception = null);

        void Info(string tag, string message = null, object data = null, Exception exception = null);

        void Warning(string tag, string message = null, object data = null, Exception exception = null);

        void Error(string tag, string message = null, object data = null, Exception exception = null);

        void Fatal(string tag, string message = null, object data = null, Exception exception = null);
    }
}