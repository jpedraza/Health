using System;

namespace Health.Data.Attributes
{
    //Comment
    //TODO: Поработать над атрибутом NotNullable
    [AttributeUsage(AttributeTargets.All)]
    public class NotNullable : Attribute
    {
        public override bool Match(object obj)
        {
            if (obj == null)
            {
                return true;
            }
            throw new NullReferenceException();
        }
    }
}