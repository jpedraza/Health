using System;

namespace EFCFModel.Exceptions
{
    public abstract class GuidException : Exception
    {
        public string Guid { get; set; }

        protected GuidException(string message, string guid, Exception innerException) 
            : base(message, innerException)
        {
            Guid = guid;
        }
    }
}
