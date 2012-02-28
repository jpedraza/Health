using System;
using System.Reflection;

namespace EFCFModel.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class ByteTypeAttribute : Attribute
    {
        protected readonly string _property;

        public ByteTypeAttribute(string property)
        {
            _property = property;
        }

        public Type GetByteType(object o)
        {
            string[] path = _property.Split('.');
            object obj = o;
            Type objType = o.GetType();
            foreach (string s in path)
            {
                PropertyInfo property = obj.GetType().GetProperty(s);
                obj = property.GetValue(obj, null);
                objType = property.PropertyType;
                if (obj == null) return null;
            }
            if (objType != typeof(Type)) 
                throw new Exception("End object is not \"Type\".");
            return obj as Type;
        }
    }
}
