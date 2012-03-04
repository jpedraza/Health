using System;
using System.Collections.Generic;
using System.Reflection;

namespace EFCFModel
{
    public interface ISchemaManager
    {
        bool HasBaseType(Type t);
        Type GetBaseType(Type t);
        bool HasKey(Type t);
        PropertyInfo Key(Type t);

        IList<Relation> GetRelations<T>()
            where T : class;

        IList<Relation> GetRelations(Type t);
        IEnumerable<Type> GetAllEntities();
        IEnumerable<Type> GetAllScaffoldEntities();
    }
}
