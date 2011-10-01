using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Health.Core.TypeProvider
{
    /// <summary>
    /// Динамический дескриптор типа.
    /// </summary>
    public class DynamicTypeDescriptor : CustomTypeDescriptor
    {
        /// <summary>
        /// Тип метаданных.
        /// </summary>
        private readonly Type _metadataType;

        public DynamicTypeDescriptor(ICustomTypeDescriptor parent, Type metadataType) : base(parent)
        {
            _metadataType = metadataType;
        }

        public override PropertyDescriptorCollection GetProperties()
        {
            PropertyDescriptorCollection baseCollection = base.GetProperties();
            if (_metadataType != null)
            {
                IList<PropertyDescriptor> tempPropertyDescriptors = new List<PropertyDescriptor>();
                foreach (PropertyDescriptor propertyDescriptor in baseCollection)
                {
                    PropertyInfo propertyInfo = _metadataType.GetProperty(propertyDescriptor.Name,
                                                                       propertyDescriptor.PropertyType);
                    if (propertyInfo != null)
                    {
                        object[] newAttributesTemp = propertyInfo.GetCustomAttributes(false);
                        Attribute[] newAttributes = Array.ConvertAll(newAttributesTemp, ObjectTypeToAttributeType);
                        if (newAttributes.Length > 0)
                        {
                            tempPropertyDescriptors.Add(new PropertyDescriptorWrapper(propertyDescriptor, newAttributes));
                        }
                        else
                        {
                            tempPropertyDescriptors.Add(propertyDescriptor);
                        }
                    }
                    else
                    {
                        tempPropertyDescriptors.Add(propertyDescriptor);
                    }
                }
                return new PropertyDescriptorCollection(tempPropertyDescriptors.ToArray(), true);
            }
            return baseCollection;
        }

        private static Attribute ObjectTypeToAttributeType(object o)
        {
            return o as Attribute;
        }

        private class PropertyDescriptorWrapper : PropertyDescriptor
        {
            private readonly PropertyDescriptor _wrappedPropertyDescriptor;

            public PropertyDescriptorWrapper(PropertyDescriptor wrappedPropertyDescriptor, Attribute[] newAttributes)
                : base(wrappedPropertyDescriptor, newAttributes)
            {
                _wrappedPropertyDescriptor = wrappedPropertyDescriptor;
            }

            public override bool CanResetValue(object component)
            {
                return _wrappedPropertyDescriptor.CanResetValue(component);
            }

            public override Type ComponentType
            {
                get { return _wrappedPropertyDescriptor.ComponentType; }

            }

            public override object GetValue(object component)
            {
                return _wrappedPropertyDescriptor.GetValue(component);
            }

            public override bool IsReadOnly
            {
                get { return _wrappedPropertyDescriptor.IsReadOnly; }
            }

            public override Type PropertyType
            {
                get { return _wrappedPropertyDescriptor.PropertyType; }
            }

            public override void ResetValue(object component)
            {
                _wrappedPropertyDescriptor.ResetValue(component);
            }

            public override void SetValue(object component, object value)
            {
                _wrappedPropertyDescriptor.SetValue(component, value);
            }

            public override bool ShouldSerializeValue(object component)
            {
                return _wrappedPropertyDescriptor.ShouldSerializeValue(component);
            }
        }
    }
}
