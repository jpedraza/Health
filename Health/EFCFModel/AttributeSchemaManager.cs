using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using Model.Entities;

namespace Model
{
    public class AttributeSchemaManager : ISchemaManager
    {
        public bool HasBaseType(Type t)
        {
            return t.BaseType != null && t.BaseType != typeof (object);
        }

        public Type GetBaseType(Type t)
        {
            Type baseType = t;
            while (baseType.BaseType != null && baseType.BaseType != typeof (object))
                baseType = baseType.BaseType;
            return baseType;
        }

        public bool HasInherited(Type t)
        {
            return Assembly.GetAssembly(t).GetTypes().Any(tt => tt.IsSubclassOf(t));
        }

        public bool HasKey(Type t)
        {
            PropertyInfo[] propertyInfos = t.GetProperties();
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                if (propertyInfo.GetCustomAttributes(true).Any(a => a is KeyAttribute))
                    return true;
            }
            return false;
        }

        public PropertyInfo Key(Type t)
        {
            PropertyInfo[] propertyInfos = t.GetProperties();
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                if (propertyInfo.GetCustomAttributes(true).Any(a => a is KeyAttribute))
                    return propertyInfo;
            }
            return null;
        }

        public IList<Relation> GetRelations<T>()
            where T : class
        {
            return GetRelations(typeof (T));
        }

        public IList<Relation> GetRelations(Type t)
        {
            var relations = new List<Relation>();
            PropertyInfo[] fromProperties = t.GetProperties();
            foreach (PropertyInfo fromProperty in fromProperties)
            {
                if (fromProperty.PropertyType.Name == typeof (ICollection<>).Name &&
                    fromProperty.PropertyType.IsGenericType)
                {
                    Type to = fromProperty.PropertyType.GetGenericArguments()[0];
                    PropertyInfo[] toProperties = to.GetProperties();
                    PropertyInfo toProperty = toProperties.FirstOrDefault(p => p.PropertyType == t);
                    if (toProperty != null)
                    {
                        var relation = new Relation
                                           {
                                               FromType = toProperty.PropertyType,
                                               FromProperty = fromProperty,
                                               ToType = to,
                                               ToProperty = toProperty,
                                               RelationType = RelationType.OneToMany
                                           };
                        relations.Add(relation);
                    }
                    toProperty =
                        toProperties.FirstOrDefault(p => p.PropertyType == typeof (ICollection<>).MakeGenericType(t));
                    if (toProperty != null)
                    {
                        var relation = new Relation
                                           {
                                               FromType = toProperty.PropertyType.GetGenericArguments()[0],
                                               FromProperty = fromProperty,
                                               ToType = to,
                                               ToProperty = toProperty,
                                               RelationType = RelationType.ManyToMany
                                           };
                        relations.Add(relation);
                    }
                }
                else
                {
                    PropertyInfo[] toProperties = fromProperty.PropertyType.GetProperties();
                    PropertyInfo toProperty =
                        toProperties.FirstOrDefault(p => p.PropertyType == typeof (ICollection<>).MakeGenericType(t));
                    if (toProperty != null)
                    {
                        var relation = new Relation
                                           {
                                               FromType = toProperty.PropertyType.GetGenericArguments()[0],
                                               FromProperty = fromProperty,
                                               ToType = fromProperty.PropertyType,
                                               ToProperty = toProperty,
                                               RelationType = RelationType.ManyToOne
                                           };
                        relations.Add(relation);
                    }
                }
            }
            return relations;
        }

        public IEnumerable<Type> GetAllEntities()
        {
            return Assembly.GetExecutingAssembly().GetTypes().Where(
                t => t.Namespace == typeof(IIdentity).Namespace && t.IsClass && t.IsPublic).ToList();
        }

        public IEnumerable<Type> GetAllScaffoldEntities()
        {
            return
                GetAllEntities().Where(
                    t => !t.IsAbstract && t.GetCustomAttributes(true).Any(a => a is ScaffoldTableAttribute)).ToList();
        }
    }
}
