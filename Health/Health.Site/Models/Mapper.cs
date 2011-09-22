using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Health.Site.Models
{
    /// <summary>
    /// Маппер свойств.
    /// </summary>
    public class Mapper
    {
        /// <summary>
        /// Копирует свойства одного объекта в одноименные и однотипные свойства другого объекта.
        /// </summary>
        /// <typeparam name="TFrom">Тип "от кого".</typeparam>
        /// <typeparam name="TTo">Тип "кому".</typeparam>
        /// <param name="from">Экземпляр "от кого".</param>
        /// <returns>Экземпляр "кому".</returns>
        public TTo Map<TFrom, TTo>(TFrom from) where TTo : class
        {
            PropertyInfo[] from_properties = typeof (TFrom).GetProperties();
            Type to_type = typeof (TTo);
            var to = Activator.CreateInstance(to_type) as TTo;
            if (to == null) throw new Exception("Не могу создать экземпляр класса.");
            foreach (PropertyInfo from_property in from_properties)
            {
                PropertyInfo to_property = to_type.GetProperty(from_property.Name, from_property.PropertyType);
                if (to_property == null) continue;
                object value = from_property.GetValue(from, null);
                to_property.SetValue(to, value, null);
            }
            return to;
        }
    }
}