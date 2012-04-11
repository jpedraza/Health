using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Model.Attributes;

namespace Model.Entities
{
    [Table("Surveys"), ScaffoldTable(true), DisplayName("Хирургическая операция")]
    public class Survey : IIdentity
    {
        public Survey()
        {
            SurveysStorages = new List<SurveyStorage>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Hide]
        public int Id { get; set; }

        [DisplayName("Имя")]
        [Required(ErrorMessage = "Необходимо указать название операции.")]
        public string Name { get; set; }

        [DisplayName("Описание"), EditMode(EditMode.Multiline)]
        [Required(ErrorMessage = "Необходимо описание операции.")]
        public string Description { get; set; }

        [NotDisplay, DisplayName("Хранилище операций")]
        public virtual ICollection<SurveyStorage> SurveysStorages { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}