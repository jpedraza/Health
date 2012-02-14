using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using PrototypeHM.DB;

namespace PrototypeHM
{
    public static class ExtensionMethods
    {
        internal static IList<object> ToListOfObjects<T>(this IList<T> list)
            where T : class
        {
            var listObjs = new List<object>();
            foreach (T obj in list)
            {
                listObjs.Add(obj);
            }
            return listObjs;
        }

        internal static IList<object> ToListOfObjects(this IList list)
        {
            var listObjs = new List<object>();
            foreach (object obj in list)
            {
                listObjs.Add(obj);
            }
            return listObjs;
        }

        public static IBindingList ToBindingList<T>(this IList<T> list)
        {
            var listObjs = new BindingList<T>();
            foreach (T obj in list)
            {
                listObjs.Add(obj);
            }
            return listObjs; 
        }

        internal static IList<T> ToList<T>(this IBindingList list)
            where T : class
        {
            var l = new List<T>();
            foreach (object o in list)
            {
                l.Add(o as T);
            }
            return l;
        }

        internal static void AddRange(this IBindingList list, IBindingList objs)
        {
            foreach (object obj in objs)
            {
                list.Add(obj);
            }
        }

        internal static void RemoveRange(this IBindingList list, IBindingList objs)
        {
            foreach (object obj in objs)
            {
                list.Remove(obj);
            }
        }

       

        /// <summary>
        /// Проверяет правильность данных, отправляемых на запись
        /// </summary>
        /// <param name="repository">метод расширения</param>
        /// <param name="checkingPropertys">добавьте параметры</param>
        /// <returns></returns>
        internal static object[] CheckWrittingPropertys(this Repository repository, params object[] checkingPropertys)
        {
            if (checkingPropertys == null)
            {
                return null;
            }
            foreach (var checkingProperty in checkingPropertys)
            {
                if (checkingProperty == null)
                {
                    return null;
                }
            }

            foreach (var checkingProperty in checkingPropertys)
            {
                var @string = checkingProperty.ToString();
                if (@string == string.Empty)
                {
                    return null;
                }
            }
            return checkingPropertys;
        }
    }
}
