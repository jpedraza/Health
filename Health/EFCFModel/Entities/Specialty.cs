using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using EFCFModel.Attributes;

namespace EFCFModel.Entities
{
    [Table("Specialities"), ScaffoldTable(true), DisplayName("Специальность")]
    public class Specialty : IIdentity
    {
        public Specialty()
        {
            Doctors = new List<Doctor>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Hide]
        public int Id { get; set; }

        [StringLength(255), DisplayName("Имя")]
        public string Name { get; set; }

        [DisplayName("Доктора"), NotDisplay]
        public virtual ICollection<Doctor> Doctors { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}