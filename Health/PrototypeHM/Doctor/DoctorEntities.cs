using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using PrototypeHM.DB;
using PrototypeHM.DB.Attributes;
using PrototypeHM.User;

namespace PrototypeHM.Doctor
{
    public class DoctorFullData : UserFullData
    {
        [DisplayName(@"Специальность"), SingleSelectEditMode(typeof(OperationsContext<Specialty.Specialty>), "Name")]
        public string Specialty { get; set; }

        [DisplayName(@"Роль"), NotEdit]
        public new string Role { get; set; }

        public DoctorFullData():base()
        {
            Specialty = string.Empty;
            Role = string.Empty;
        }
    }

    public class PatientForDoctor : QueryStatus, IIdentity
    {
        [Hide]
        public int Id { get; set; }
        [Hide]
        public string FirstName { get; set; }
        [Hide]
        public string LastName { get; set; }
        [Hide]
        public string ThirdName { get; set; }
        [NotMap, DisplayName(@"ФИО")]
        public string FullName { get { return string.Format("{0} {1} {2}", LastName, FirstName, ThirdName); }
            
        }
        [DisplayName(@"Карта")]
        public string Card { get; set; }
        [DisplayName(@"Полюс")]
        public string Policy { get; set; }

        public PatientForDoctor():base()
        {
            foreach (var pI in this.GetType().GetProperties())
            {
                if (pI.PropertyType == typeof(string))
                {
                    try
                    {
                        pI.SetValue(this, string.Empty, null);
                    }
                    catch
                    {
                        
                    }
                }

                if (pI.PropertyType == typeof(DateTime))
                {
                    try
                    {
                        pI.SetValue(this, DateTime.Today, null);
                    }
                    catch
                    {
                        
                    }
                }
            }
        }
    }

    public class DoctorDetail : DoctorFullData
    {
        [DisplayName(@"Пациенты"), MultiSelectEditMode(typeof(OperationsContext<PatientForDoctor>), "FirstName", TypeMappingEnum.ManyToMany)]
        public IList<PatientForDoctor> Patients { get; set; }

        public DoctorDetail():base()
        {
            Patients = new Collection<PatientForDoctor>();
        }
    }
}
