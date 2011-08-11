using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Health.Core.Exceptions
{
    public class RepositoryException : BaseException
    {
        protected RepositoryException() {}

        protected RepositoryException(string message) : base(message) { }
    }
}
