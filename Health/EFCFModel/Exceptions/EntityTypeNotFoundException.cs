using System;

namespace Model.Exceptions
{
    public class EntityTypeNotFoundException : GuidException
    {
        public EntityTypeNotFoundException(string message, string guid, Exception innerException) : base(message, guid, innerException)
        {
        }
    }
}
