using System;

namespace Aklion.Infrastructure.Logger
{
    public interface ILogger
    {
        void Info(string tag, string message = null, object data = null, Exception exception = null);

        void Warning(string tag, string message = null, object data = null, Exception exception = null);

        void Error(string tag, string message = null, object data = null, Exception exception = null);
    }
}