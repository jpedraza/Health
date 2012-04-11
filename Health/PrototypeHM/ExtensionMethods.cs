using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Objects;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Prototype
{
    internal static class ExtensionMethods
    {
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

    internal static class ExEnumerable
    {
        internal static IList ToList(this IEnumerable<object> obj, Type entityType)
        {
            return (IList) typeof (Enumerable).GetMethod("ToList").MakeGenericMethod(entityType).Invoke(typeof (Enumerable), new[] {obj});
        }

        internal static IList IterateToList(this IEnumerable<object> obj, Type entityType)
        {
            var list = Activator.CreateInstance(typeof(List<>).MakeGenericType(entityType)) as IList;
            if (obj != null && list != null)
                foreach (object o in obj)
                    list.Add(o);
            return list;
        }
    }

    internal static class ExObjectContext
    {
        internal static object CreateObjectSet(this ObjectContext context, Type entityType)
        {
            MethodInfo loadMethod = context.GetType().GetMethod("CreateObjectSet", new Type[0]);
            loadMethod = loadMethod.MakeGenericMethod(entityType);
            return loadMethod.Invoke(context, null);
        }
    }

    internal static class ExLinq
    {
        internal static object FirstOrDefault(this IEnumerable set, Type t, Func<object, bool> where)
        {
            return ((IEnumerable<object>) set).FirstOrDefault(where);
        }
    }

    internal static class ExType
    {
        internal static string GetDisplayName(this Type t)
        {
            var att = t.GetCustomAttributes(true).FirstOrDefault(a => a is DisplayNameAttribute) as DisplayNameAttribute;
            return att == null ? t.Name : att.DisplayName;
        }
    }

    internal static class ExQueryable
    {
        internal static Expression<Func<object, bool>> PropertyFilter(Type t, string propertyName, object value)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(object), "o");
            Expression<Func<object, bool>> where =
                Expression.Lambda<Func<object, bool>>(
                    Expression.Equal(Expression.Property(Expression.Convert(parameter, t), propertyName),
                                     Expression.Constant(value)), parameter);
            return where;
        }
    }
}