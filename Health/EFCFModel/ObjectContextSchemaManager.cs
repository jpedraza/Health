using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Metadata.Edm;
using System.Data.Objects;
using System.Linq;
using System.Reflection;
using EFCFModel.Exceptions;

namespace EFCFModel
{
    public class ObjectContextSchemaManager : ISchemaManager
    {
        private readonly ObjectContext _context;
        private readonly MetadataWorkspace _workspace;

        public ObjectContextSchemaManager(ObjectContext context)
        {
            _context = context;
            _workspace = _context.MetadataWorkspace;
        }

        private EntityType GetEntityType(Type t)
        {
            var entityType = _workspace.GetType(t.Name, t.Namespace, DataSpace.OSpace) as EntityType;
            if (entityType == null)
                throw new EntityTypeNotFoundException(string.Format("Entity type {0} not found in OSpace.", t.FullName),
                                                      "{A9A252B1-78F0-4F8B-B54F-D82F1BC48525}", null);
            return entityType;
        }

        public bool HasBaseType(Type t)
        {
            EntityType entityType = GetEntityType(t);
            return entityType.BaseType != null;
        }

        public Type GetBaseType(Type t)
        {
            EntityType entityType = GetEntityType(t);
            EdmType baseType = entityType;
            while (baseType.BaseType != null)
                baseType = baseType.BaseType;
            return Type.GetType(baseType.FullName);
        }

        public bool HasKey(Type t)
        {
            EntityType entityType = GetEntityType(t);
            return entityType.KeyMembers.Any();
        }

        public PropertyInfo Key(Type t)
        {
            EntityType entityType = GetEntityType(t);
            return entityType.KeyMembers.Any() ? t.GetProperty(entityType.KeyMembers[0].Name) : null;
        }

        public IList<Relation> GetRelations<T>()
            where T : class
        {
            return GetRelations(typeof(T));
        }

        public IList<Relation> GetRelations(Type t)
        {
            EntityType entityType = GetEntityType(t);
            var relations = new List<Relation>();
            foreach (NavigationProperty navigationProperty in entityType.NavigationProperties)
            {
                var relation = new Relation
                                   {
                                       FromProperty = t.GetProperty(navigationProperty.Name),
                                       FromType = t,
                                       ToType =
                                           Type.GetType(
                                               navigationProperty.ToEndMember.TypeUsage.EdmType.Name.Replace(
                                                   "reference[", "").Replace("]", ""))
                                   };
                if (navigationProperty.FromEndMember.RelationshipMultiplicity == RelationshipMultiplicity.Many &&
                    navigationProperty.ToEndMember.RelationshipMultiplicity == RelationshipMultiplicity.One ||
                    navigationProperty.FromEndMember.RelationshipMultiplicity == RelationshipMultiplicity.Many &&
                    navigationProperty.ToEndMember.RelationshipMultiplicity == RelationshipMultiplicity.ZeroOrOne)
                {
                    relation.RelationType = RelationType.ManyToOne;
                    relation.ToProperty =
                        relation.ToType.GetProperties().FirstOrDefault(
                            p => p.PropertyType == typeof(ICollection<>).MakeGenericType(t));
                }
                if (navigationProperty.FromEndMember.RelationshipMultiplicity == RelationshipMultiplicity.Many &&
                    navigationProperty.ToEndMember.RelationshipMultiplicity == RelationshipMultiplicity.Many)
                {
                    relation.RelationType = RelationType.ManyToMany;
                    relation.ToProperty =
                        relation.ToType.GetProperties().FirstOrDefault(
                            p => p.PropertyType == t);
                }
                if (navigationProperty.FromEndMember.RelationshipMultiplicity == RelationshipMultiplicity.One &&
                    navigationProperty.ToEndMember.RelationshipMultiplicity == RelationshipMultiplicity.Many)
                {
                    relation.RelationType = RelationType.OneToMany;
                    relation.ToProperty =
                        relation.ToType.GetProperties().FirstOrDefault(
                            p => p.PropertyType == t);
                }
                relations.Add(relation);
            }
            return relations;
        }

        public IEnumerable<Type> GetAllEntities()
        {
            return from entity in _workspace.GetItems(DataSpace.OSpace)
                   let ent = entity as EntityType
                   where entity is EntityType
                   select Type.GetType(ent.FullName);
        }

        public IEnumerable<Type> GetAllScaffoldEntities()
        {
            return from entity in GetAllEntities()
                   where entity.GetCustomAttributes(true).Any(a => a is ScaffoldTableAttribute)
                   let fullName = entity.FullName
                   where fullName != null
                   select Type.GetType(fullName);
        }
    }
}
