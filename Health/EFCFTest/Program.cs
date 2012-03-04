using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Metadata.Edm;
using System.Data.Objects;
using System.Reflection;
using System.Runtime.Caching;
using EFCFModel;
using EFCFModel.Entities;

namespace EFCFTest
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WindowHeight = Console.WindowHeight * 2;
            Console.WindowWidth = Console.WindowWidth * 2;
            Console.BufferHeight = Console.BufferHeight * 2;
            Console.BufferWidth = Console.BufferWidth * 2;
            var context = new EFHealthContext();
            //context.Database.Initialize(true);
            var relationshipManager = new ObjectContextSchemaManager(context.ObjectContext);
            IList<Relation> relations = relationshipManager.GetRelations(typeof (Patient));
            foreach (Relation relation in relations)
            {
                Console.WriteLine(string.Format("Relation {0}: from {1} to {2}",
                                                relation.RelationType, relation.FromType, relation.ToType));
            }
            Console.WriteLine("Complete");
            IEnumerable<Type> entities = relationshipManager.GetAllScaffoldEntities();
            foreach (Type entity in entities)
            {
                Console.WriteLine(entity.Name);
            }
            /*DbSet<User> users = context.Set<User>();
            foreach (User user in users)
            {
                Console.WriteLine(user.GetType().BaseType);
            }*/
            /*Console.WriteLine(Assembly.GetAssembly(typeof(DbContext)).GetName().FullName);
            var schemaManager = new ObjectContextSchemaManager(context.ObjectContext);
            foreach (Relation relation in schemaManager.GetRelations(typeof(Patient)))
            {
                Console.WriteLine(string.Format("Relation {0}: from {1} to {2}",
                                                relation.RelationType, relation.FromType, relation.ToType));
            }
            Console.WriteLine(tname);*/
            Console.ReadLine();
            /*context.Dispose();*/
        }
    }
}