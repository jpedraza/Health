using System.Collections;
using System.Collections.Generic;

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
    }
}
