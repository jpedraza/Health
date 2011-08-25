using System;
using System.ComponentModel;
using Health.Core.API;

namespace Health.Core.Exceptions
{
    public abstract class BaseException : Exception
    {
        protected BaseException()
        {
            Logger = new Logger(TypeDescriptor.GetClassName(GetType()));
        }

        protected BaseException(string message) : base(message)
        {
            Logger.Warn(message);
        }

        protected ILogger Logger { get; private set; }
    }
}