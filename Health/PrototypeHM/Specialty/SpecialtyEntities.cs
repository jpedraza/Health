using System.ComponentModel;
using PrototypeHM.DB.Attributes;

namespace PrototypeHM.Specialty
{
    public class Specialty
    {
        [Hide, NotEdit]
        public int SpecialtyId { get; set; }
        [DisplayName(@"Имя")]
        public string Name { get; set; }
    }
}
