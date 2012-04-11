using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Model.Attributes;

namespace Model.Entities
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

        [DisplayName("Имя")]
        [Required(ErrorMessage = "Необходимо указать имя специальности.")]
        public string Name { get; set; }

        [DisplayName("Доктора"), NotDisplay]
        public virtual ICollection<Doctor> Doctors { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}