using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PrototypeHM.DB.Attributes;

namespace PrototypeHM
{

    class checkEnitiesValid
    {
        public static bool checkValid(Type typeOfObject, object validObject)
        {
            var _isValid = true;
            var propertiesInfo = typeOfObject.GetProperties();
            foreach (var propertyInfo in propertiesInfo)
            {
                var _prpAtt = propertyInfo.GetCustomAttributes(true);
                var _isPrpSimpleOrCompound = _prpAtt.FirstOrDefault(a =>
                {
                    return a.GetType() == typeof(SimpleOrCompoundModelAttribute);
                })
                as SimpleOrCompoundModelAttribute;

                if (_prpAtt != null && _isPrpSimpleOrCompound != null)
                {
                    if (_isPrpSimpleOrCompound.IsSimple)
                    {
                        if (propertyInfo.GetValue(validObject, null) != null)
                        {
                            if (propertyInfo.PropertyType == typeof(string))
                            {
                                _isValid = (propertyInfo.GetValue(validObject, null) as string != null) &&
                                    (propertyInfo.GetValue(validObject, null) as string != "");
                            }

                            if (propertyInfo.PropertyType == typeof(int) | propertyInfo.PropertyType == typeof(double))
                            {
                                _isValid = propertyInfo.GetValue(validObject, null) != (object)0;
                            }

                            if (propertyInfo.PropertyType == typeof(object))
                            {
                                if (validObject != null)
                                {
                                    var stringMassiv = propertyInfo.GetValue(validObject, null) as string[];
                                    if (stringMassiv != null)
                                    {
                                        _isValid = stringMassiv[0] != "";
                                    }
                                }
                            }


                        }
                        else
                        {
                            _isValid = checkValid(propertyInfo.PropertyType, propertyInfo.GetValue(validObject, null));
                        }
                    }
                }
                else
                {
                    _isValid = false;
                }
                
            }
            return _isValid;
        }
    }
}
