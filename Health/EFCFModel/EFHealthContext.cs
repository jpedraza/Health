using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Objects;
using EFCFModel.Entities;

namespace EFCFModel
{
    public class EFHealthContext : DbContext
    {
        static EFHealthContext()
        {
            Database.SetInitializer<EFHealthContext>(null);
        }

        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Diagnosis> Diagnosis { get; set; }
        public DbSet<DiagnosisClass> DiagnosisClasses { get; set; }
        public DbSet<FunctionalAbnormality> FunctionalAbnormalities { get; set; }
        public DbSet<FunctionalClass> FunctionalClasses { get; set; }
        public DbSet<Parameter> Parameters { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Specialty> Specialties { get; set; }
        public DbSet<Survey> Surveys { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ParameterStorage> ParameterStorages { get; set; }

        public ObjectContext ObjectContext { get { return ((IObjectContextAdapter) this).ObjectContext; } }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<System.Data.Entity.ModelConfiguration.Conventions.OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<System.Data.Entity.ModelConfiguration.Conventions.ManyToManyCascadeDeleteConvention>();
        }
    }
}