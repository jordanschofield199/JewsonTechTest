using System;

namespace Jewson.Logging.Interfaces
{
    public interface ILogger
    {
        bool IsDebugEnabled { get; }
        bool IsInfoEnabled { get; }
        bool IsWarnEnabled { get; }
        bool IsErrorEnabled { get; }
        bool IsFatalEnabled { get; }

        /* Log a message object */
        void Debug(object message);
        void Info(object message);
        void Warn(object message);
        void Error(object message);
        void Fatal(object message);

        /* Log a message object and exception */
        void Debug(Exception ex, string message);
        void Info(Exception ex, string message);
        void Warn(Exception ex, string message);
        void Error(Exception ex, string message);
        void Fatal(Exception ex, string message);
    }
}
