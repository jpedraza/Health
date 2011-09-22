using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Health.Site.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ClassMetadataAttribute : Attribute
    {
        private readonly Type _metadataType;

        public ClassMetadataAttribute(Type metadata_type)
        {
            _metadataType = metadata_type;
        }

        public Type GetMetadataType()
        {
            return _metadataType;
        }
    }
}