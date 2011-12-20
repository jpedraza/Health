using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using PrototypeHM.DB.Attributes;

namespace PrototypeHM.DB.Mappers
{
    public class PropertyToColumnMapper<TResult> : ISqlMapper<TResult, SqlDataReader>
        where TResult : class , new()
    {
        #region Implementation of ISqlMapper<TResult,in SqlDataReader>

        /// <summary>
        /// Сформировать результат.
        /// </summary>
        /// <param name="reader">Набор исходных данных.</param>
        /// <returns>Результат.</returns>
        public IList<TResult> Map(SqlDataReader reader)
        {
            Type objectType = typeof(TResult);
            PropertyInfo[] properties = objectType.GetProperties().Where(
                p => p.GetCustomAttributes(true).Where(a => a.GetType() == typeof(NotMapAttribute)).Count() == 0).ToArray();
            IList<TResult> objects = new List<TResult>();
            while (reader.Read())
            {
                var obj = new TResult();
                foreach (PropertyInfo property in properties)
                {
                    object cell = Convert.ChangeType(reader[property.Name], property.PropertyType);
                    property.SetValue(obj, cell, null);
                }
                objects.Add(obj);
            }
            return objects;
        }

        #endregion
    }
}