using System;
using System.Data.Entity;
using EFCFModel;
using EFCFModel.Entities;

namespace Support.Tasks
{
    public class TestDataTask : ITask
    {
        #region Implementation of ITask

        public void Process(DbContext context)
        {
            context.Configuration.AutoDetectChangesEnabled = false;
            context.Configuration.ValidateOnSaveEnabled = false;
            var ra = new Role {Name = "Admin"};
            var rd = new Role {Name = "Doctor"};
            var rp = new Role {Name = "Patient"};
            context.Set<Role>().AddRange(ra, rd, rp);
            context.SaveChanges();
            var u1 = new User
                         {
                             FirstName = "Антон",
                             LastName = "Петров",
                             ThirdName = "Валентинович",
                             Login = "anton",
                             Password = "anton",
                             Token = "fjdsfklnsdklfns",
                             Birthday = DateTime.Now.AddYears(-27),
                             Role = ra
                         };
            context.Set<User>().Add(u1);
            context.SaveChanges();
            var s1 = new Specialty
                         {
                             Name = "Pediator"
                         };
            context.Set<Specialty>().Add(s1);
            context.SaveChanges();
            var d1 = new Doctor
                         {
                             FirstName = "Макар",
                             LastName = "Солнышкин",
                             ThirdName = "Валентинович",
                             Login = "makar",
                             Password = "makar",
                             Token = "fjdsfklnsdklfns",
                             Birthday = DateTime.Now.AddYears(-38),
                             Role = rd,
                             Specialty = s1
                         };
            var d2 = new Doctor
                         {
                             FirstName = "Антон",
                             LastName = "Солнышкин",
                             ThirdName = "Валентинович",
                             Login = "anton",
                             Password = "anton",
                             Token = "fjdsfklnsdklfns",
                             Birthday = DateTime.Now.AddYears(-38),
                             Role = rd,
                             Specialty = s1
                         };
            context.Set<Doctor>().AddRange(d1, d2);
            context.SaveChanges();
            var p1 = new Patient
                         {
                             FirstName = "Илья",
                             LastName = "Колбаскин",
                             ThirdName = "Валентинович",
                             Login = "ilja",
                             Password = "ilja",
                             Token = "fjdsfklnsdklfns",
                             Birthday = DateTime.Now.AddYears(-12),
                             Role = rp,
                             Card = "dsdasdas",
                             Mother = "Анна Анатольевна",
                             StartDateOfObservation = DateTime.Now.AddYears(-2),
                             Doctor = d1
                         };
            context.Set<Patient>().Add(p1);
            context.SaveChanges();
            var a1 = new Appointment
                         {
                             Doctor = d1,
                             Patient = p1,
                             Date = DateTime.Now.AddMonths(-1)
                         };
            context.Set<Appointment>().Add(a1);
            context.SaveChanges();
            var dsc = new DiagnosisClass
                          {
                              Name = "Сердечные",
                              Code = "454445"
                          };
            context.Set<DiagnosisClass>().Add(dsc);
            context.SaveChanges();
            var ds = new Diagnosis
                         {
                             Name = "Сердечная недостаточность",
                             Code = "45568687",
                             DiagnosisClass = dsc
                         };
            ds.Patients.Add(p1);
            context.Set<Diagnosis>().Add(ds);
            context.SaveChanges();
        }

        #endregion
    }
}