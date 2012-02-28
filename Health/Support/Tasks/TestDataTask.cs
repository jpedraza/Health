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
            var functionalClass1 = new FunctionalClass
                                     {
                                         Code = "Функциональный класс I",
                                         Description =
                                             "Пациенты с заболеванием сердца, у которых обычные физические нагрузки не вызывают одышки, утомления или сердцебиения."
                                     };
            var functionalClass2 = new FunctionalClass
                                       {
                                           Code = "Функциональный класс II",
                                           Description =
                                               "Пациенты с заболеванием сердца и умеренным ограничением физической активности. При обычных физических нагрузках наблюдаются одышка, усталость и сердцебиение."
                                       };
            var functionalClass3 = new FunctionalClass
                                       {
                                           Code = "Функциональный класс III",
                                           Description =
                                               "Пациенты с заболеванием сердца и выраженным ограничением физической активности. В состоянии покоя жалобы отсутствуют, но даже при незначительных физических нагрузках появляются одышка, усталость и сердцебиение."
                                       };
            var functionalClass4 = new FunctionalClass
                                       {
                                           Code = "Функциональный класс IV",
                                           Description =
                                               "Пациенты с заболеванием сердца, у которых любой уровень физической активности вызывает указанные выше субъективные симптомы. Последние возникают и в состоянии покоя."
                                       };
            context.Set<FunctionalClass>().AddRange(functionalClass1, functionalClass2, functionalClass3, functionalClass4);
            context.SaveChanges();
            var survey1 = new Survey
                              {
                                  Name = "Пластика ДМЖП по методике двойной заплаты в условиях ИК.",
                                  Description = "Пластика ДМЖП по методике двойной заплаты в условиях ИК."
                              };
            var survey2 = new Survey
                              {
                                  Name = "Пластика ДМПП,ДМЖП,ИСЛА,аномалия Эбштейна,радик.коррекц. тетрады Фалло.",
                                  Description =
                                      "Пластика ДМПП,ДМЖП,ИСЛА,аномалия Эбштейна,радик.коррекц. тетрады Фалло."
                              };
            var survey3 = new Survey
                              {
                                  Name = "Реконструкции пути оттока от ПЖ.",
                                  Description = "Реконструкции пути оттока от ПЖ."
                              };
            context.Set<Survey>().AddRange(survey1, survey2, survey3);
            context.SaveChanges();
            var roleAdmin = new Role {Name = "Admin"};
            var roleDoctor = new Role {Name = "Doctor"};
            var rolePatient = new Role {Name = "Patient"};
            context.Set<Role>().AddRange(roleAdmin, roleDoctor, rolePatient);
            context.SaveChanges();
            var u1 = new User
                         {
                             FirstName = "Антон",
                             LastName = "Петров",
                             ThirdName = "Валентинович",
                             Login = "anton",
                             Password = "anton",
                             Birthday = DateTime.Now.AddYears(-27),
                             Role = roleAdmin
                         };
            context.Set<User>().Add(u1);
            context.SaveChanges();
            var specialty1 = new Specialty {Name = "Педиатор"};
            var specialty2 = new Specialty {Name = "Кардиолог"};
            context.Set<Specialty>().AddRange(specialty1, specialty2);
            context.SaveChanges();
            var doctor1 = new Doctor
                         {
                             FirstName = "Макар",
                             LastName = "Солнышкин",
                             ThirdName = "Валентинович",
                             Login = "makar",
                             Password = "makar",
                             Birthday = DateTime.Now.AddYears(-38),
                             Role = roleDoctor,
                             Specialty = specialty1
                         };
            var doctor2 = new Doctor
                         {
                             FirstName = "Антон",
                             LastName = "Солнышкин",
                             ThirdName = "Валентинович",
                             Login = "anton",
                             Password = "anton",
                             Birthday = DateTime.Now.AddYears(-38),
                             Role = roleDoctor,
                             Specialty = specialty2
                         };
            context.Set<Doctor>().AddRange(doctor1, doctor2);
            context.SaveChanges();
            var parameter1 = new DoubleParameter
                                 {
                                     Name = "Пульс",
                                     DefaultValue = BitConverter.GetBytes(60.0),
                                     MinValue = 15,
                                     MaxValue = 250
                                 };
            var parameter2 = new StringParameter
                                 {
                                     Name = "Давление",
                                     DefaultValue = Encoding.UTF8.GetBytes("120x80")
                                 };
            context.Set<Parameter>().AddRange(parameter1, parameter2);
            var patient1 = new Patient
                         {
                             FirstName = "Илья",
                             LastName = "Колбаскин",
                             ThirdName = "Валентинович",
                             Login = "ilja",
                             Password = "ilja",
                             Birthday = DateTime.Now.AddYears(-12),
                             Role = rolePatient,
                             Card = "9A5CD8E5",
                             Mother = "Анна Анатольевна",
                             StartDateOfObservation = DateTime.Now.AddYears(-2),
                             Doctor = doctor1
                         };
            patient1.Parameters.Add(parameter1);
            patient1.Parameters.Add(parameter2);
            patient1.FunctionalClass = functionalClass2;
            context.Set<Patient>().Add(patient1);
            context.SaveChanges();
            var surveyStorage1 = new SurveyStorage
                                     {
                                         Patient = patient1,
                                         Survey = survey2,
                                         Date = DateTime.Now.AddDays(-5)
                                     };
            var surveyStorage2 = new SurveyStorage
                                     {
                                         Patient = patient1,
                                         Survey = survey3,
                                         Date = DateTime.Now.AddDays(-1)
                                     };
            context.Set<SurveyStorage>().AddRange(surveyStorage1, surveyStorage2);
            context.SaveChanges();
            var appointment1 = new Appointment
                         {
                             Doctor = doctor1,
                             Patient = patient1,
                             Date = DateTime.Now.AddMonths(-1)
                         };
            context.Set<Appointment>().Add(appointment1);
            context.SaveChanges();
            var diagnosisClass1 = new DiagnosisClass
                                      {
                                          Name = "Болезни системы кровообращения",
                                          Code = "IX"
                                      };
            var diagnosisClass2 = new DiagnosisClass
                                      {
                                          Name =
                                              "Врожденные аномалии [пороки крови], деформации и хромосомные нарушения",
                                          Code = "XVII"
                                      };
            context.Set<DiagnosisClass>().AddRange(diagnosisClass1, diagnosisClass2);
            context.SaveChanges();
            var diagnosis1 = new Diagnosis
                                 {
                                     Name = "Другие функциональные нарушения после операций на сердце",
                                     Code = "I97.1",
                                     DiagnosisClass = diagnosisClass1
                                 };
            var diagnosis2 = new Diagnosis
                                 {
                                     Name =
                                         "Другие нарушения системы кровообращения после медицинских процедур, не классифицированные в других рубриках",
                                     Code = "I97.8",
                                     DiagnosisClass = diagnosisClass1
                                 };
            var diagnosis3 = new Diagnosis
                                 {
                                     Name = "Дефект предсердной перегородки",
                                     Code = "Q21.1",
                                     DiagnosisClass = diagnosisClass2
                                 };
            var diagnosis4 = new Diagnosis
                                 {
                                     Name = "Врожденный порок сердца неуточненный",
                                     Code = "Q24.9",
                                     DiagnosisClass = diagnosisClass2
                                 };
            diagnosis1.Patients.Add(patient1);
            diagnosis2.Patients.Add(patient1);
            diagnosis4.Patients.Add(patient1);
            context.Set<Diagnosis>().AddRange(diagnosis1, diagnosis2, diagnosis3, diagnosis4);
            context.SaveChanges();
            context.Configuration.AutoDetectChangesEnabled = true;
            context.Configuration.ValidateOnSaveEnabled = true;
        }

        #endregion
    }
}