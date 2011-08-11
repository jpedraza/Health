using System;

namespace Health.Core.Exceptions
{
    public class ServiceException : BaseException
    {
        public ServiceException() { }

        public ServiceException(string message) : base(message) { }
    }
}