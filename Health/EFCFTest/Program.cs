using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Objects;
using System.Reflection;
using EFCFModel;
using EFCFModel.Entities;

namespace EFCFTest
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var context = new EFHealthContext();
            //context.Database.Initialize(true);
            var relationshipManager = new SchemaManager();
            IList<Relation> relations = relationshipManager.GetRelations(typeof (Patient));
            foreach (Relation relation in relations)
            {
                Console.WriteLine(string.Format("Relation {0}: from {1} to {2}",
                                                relation.RelationType, relation.FromType, relation.ToType));
            }
            Console.WriteLine("Complete");
            IList<Type> entities = relationshipManager.GetAllScaffoldEntities();
            foreach (Type entity in entities)
            {
                Console.WriteLine(entity.Name);
            }
            DbSet<User> users = context.Set<User>();
            foreach (User user in users)
            {
                Console.WriteLine(user.GetType().BaseType);
            }
            Console.WriteLine(Assembly.GetAssembly(typeof(System.Data.Entity.DbContext)).GetName().FullName);
            Console.ReadLine();
            /*context.Dispose();*/
        }
    }
}