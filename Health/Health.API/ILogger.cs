using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Health.API
{
    public interface ILogger
    {
        void Log(string message, int level);

        void Trace(string message);

        void Debug(string message);

        void Info(string message);

        void Warn(string message);

        void Error(string message);

        void Fatal(string message);

        bool IsEnabled();

        bool IsEnabled(int level);

        bool IsTraceEnabled { get; }

        bool IsDebugEnabled { get; }

        bool IsInfoEnabled { get; }

        bool IsWarnEnabled { get; }

        bool IsErrorEnabled { get; }

        bool IsFatalEnabled { get; }
    }
}
