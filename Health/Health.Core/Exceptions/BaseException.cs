using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Health.API;

namespace Health.Core.Exceptions
{
    public abstract class BaseException : Exception
    {
        protected ILogger Logger { get; private set; }

        protected BaseException()
        {
            Logger = new Logger(TypeDescriptor.GetClassName(GetType()));
        }

        protected BaseException(string message) : base(message)
        {
            Logger.Warn(message);
        }
    }
}
