using System;
using Jewson.Logging.Interfaces;
using log4net;

namespace Jewson.Logging
{
    public class Logger : ILogger
    {
        private readonly ILog _logger;

        public Logger(Type type)
        {
            _logger = log4net.LogManager.GetLogger(type);
        }

        public bool IsDebugEnabled => _logger.IsDebugEnabled;
        public bool IsInfoEnabled => _logger.IsInfoEnabled;
        public bool IsWarnEnabled => _logger.IsWarnEnabled;
        public bool IsErrorEnabled => _logger.IsErrorEnabled;
        public bool IsFatalEnabled => _logger.IsFatalEnabled;

        public void Debug(object message)
        {
            _logger.Debug(message);
        }

        public void Info(object message)
        {
            _logger.Info(message);
        }

        public void Warn(object message)
        {
            _logger.Warn(message);
        }

        public void Error(object message)
        {
            _logger.Error(message);
        }

        public void Fatal(object message)
        {
            _logger.Fatal(message);
        }

        public void Debug(Exception ex, string message)
        {
            _logger.Debug(message, ex);
        }

        public void Info(Exception ex, string message)
        {
            _logger.Info(message, ex);
        }

        public void Warn(Exception ex, string message)
        {
            _logger.Warn(message, ex);
        }

        public void Error(Exception ex, string message)
        {
            _logger.Error(message, ex);
        }

        public void Fatal(Exception ex, string message)
        {
            _logger.Fatal(message, ex);
        }
    }
}
