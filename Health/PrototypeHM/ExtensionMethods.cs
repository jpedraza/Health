using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace PrototypeHM
{
    internal static class ExtensionMethods
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

        internal static IBindingList ToBindingList<T>(this IList<T> list)
        {
            var listObjs = new BindingList<T>();
            foreach (T obj in list)
            {
                listObjs.Add(obj);
            }
            return listObjs; 
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
    }
}
