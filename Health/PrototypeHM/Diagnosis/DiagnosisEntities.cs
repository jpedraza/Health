using System.ComponentModel;
using PrototypeHM.DB;
using PrototypeHM.DB.Attributes;

namespace PrototypeHM.Diagnosis
{
    public class DiagnosisFullData : QueryStatus
    {
        [Hide]
        public int Id { get; set; }

        [DisplayName(@"Имя")]
        public string Name { get; set; }

        [DisplayName(@"Код")]
        public string Code { get; set; }

        [DisplayName(@"Класс")]
        public string ClassName { get; set; }
    }
}
