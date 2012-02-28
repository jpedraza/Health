using System;
using System.Data.Entity;
using System.Text;
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
                             Birthday = DateTime.Now.AddYears(-27),
                             Role = ra
                         };
            context.Set<User>().Add(u1);
            context.SaveChanges();
            var s1 = new Specialty {Name = "Педиатор"};
            var s2 = new Specialty {Name = "Кардиолог"};
            context.Set<Specialty>().AddRange(s1, s2);
            context.SaveChanges();
            var d1 = new Doctor
                         {
                             FirstName = "Макар",
                             LastName = "Солнышкин",
                             ThirdName = "Валентинович",
                             Login = "makar",
                             Password = "makar",
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
                             Birthday = DateTime.Now.AddYears(-38),
                             Role = rd,
                             Specialty = s2
                         };
            context.Set<Doctor>().AddRange(d1, d2);
            context.SaveChanges();
            var parameter1 = new DoubleParameter
                                 {
                                     Name = "Пульс",
                                     DefaultValue = BitConverter.GetBytes(60),
                                     MinValue = 15,
                                     MaxValue = 250
                                 };
            var parameter2 = new StringParameter
                                 {
                                     Name = "Давление",
                                     DefaultValue = Encoding.UTF8.GetBytes("120x80")
                                 };
            context.Set<Parameter>().AddRange(parameter1, parameter2);
            var p1 = new Patient
                         {
                             FirstName = "Илья",
                             LastName = "Колбаскин",
                             ThirdName = "Валентинович",
                             Login = "ilja",
                             Password = "ilja",
                             Birthday = DateTime.Now.AddYears(-12),
                             Role = rp,
                             Card = "9A5CD8E5",
                             Mother = "Анна Анатольевна",
                             StartDateOfObservation = DateTime.Now.AddYears(-2),
                             Doctor = d1
                         };
            p1.Parameters.Add(parameter1);
            p1.Parameters.Add(parameter2);
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
                              Code = "D1A56E05"
                          };
            context.Set<DiagnosisClass>().Add(dsc);
            context.SaveChanges();
            var ds = new Diagnosis
                         {
                             Name = "Сердечная недостаточность",
                             Code = "5984C8BE",
                             DiagnosisClass = dsc
                         };
            ds.Patients.Add(p1);
            context.Set<Diagnosis>().Add(ds);
            context.SaveChanges();
            context.Configuration.AutoDetectChangesEnabled = true;
            context.Configuration.ValidateOnSaveEnabled = true;
        }

        #endregion
    }
}