using System;
using Infrastructure.Json;
using Infrastructure.Logger.Models;
using Microsoft.Extensions.Options;
using NLog;

namespace Infrastructure.Logger
{
    public class Logger : ILogger
    {
        private readonly bool _isEnabled;
        private readonly NLog.ILogger _logger;

        public Logger(IOptions<LoggerConfiguration> options)
        {
            _isEnabled = options.Value.IsEnabled;
            _logger = LogManager.GetLogger(options.Value.AppName);
        }

        public void Trace(string tag, string message = null, object data = null, Exception exception = null)
        {
            if (_isEnabled)
            {
                _logger.Trace(exception, GetMessage(tag, message, data, exception));
            }
        }

        public void Debug(string tag, string message = null, object data = null, Exception exception = null)
        {
            if (_isEnabled)
            {
                _logger.Debug(exception, GetMessage(tag, message, data, exception));
            }
        }

        public void Info(string tag, string message = null, object data = null, Exception exception = null)
        {
            if (_isEnabled)
            {
                _logger.Info(exception, GetMessage(tag, message, data, exception));
            }
        }

        public void Warning(string tag, string message = null, object data = null, Exception exception = null)
        {
            if (_isEnabled)
            {
                _logger.Warn(exception, GetMessage(tag, message, data, exception));
            }
        }

        public void Error(string tag, string message = null, object data = null, Exception exception = null)
        {
            if (_isEnabled)
            {
                _logger.Error(exception, GetMessage(tag, message, data, exception));
            }
        }

        public void Fatal(string tag, string message = null, object data = null, Exception exception = null)
        {
            if (_isEnabled)
            {
                _logger.Fatal(exception, GetMessage(tag, message, data, exception));
            }
        }

        private static string GetMessage(string tag, string message = null, object data = null, Exception exception = null)
        {
            var result = string.Empty;

            result += $",\"Tag\":\"{tag}\"";
            result += !string.IsNullOrEmpty(message) ? $",\"Message\":\"{message}\"" : string.Empty;
            result += data != null ? $",\"Data\":{data.ToJsonString()}" : string.Empty;
            result += exception != null ? $",\"Exception\":{GetExceptionMessage(exception)}" : string.Empty;

            return result;
        }

        private static string GetExceptionMessage(Exception exception)
        {
            var result = new
            {
                Type = exception.GetType().Name,
                exception.Message,
                exception.Data,
                exception.StackTrace,
                InnerException = exception.InnerException == null
                    ? null
                    : new
                    {
                        Type = exception.InnerException.GetType().Name,
                        exception.InnerException.Message,
                        exception.InnerException.Data,
                        exception.InnerException.StackTrace
                    }
            };

            return result.ToJsonString();
        }
    }
}