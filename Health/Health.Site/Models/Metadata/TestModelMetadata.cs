using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Health.Site.Models.Validators;
using System.Web.Mvc;

namespace Health.Site.Models.Metadata
{
    public class TestModelMetadata
    {
        [CustomValidation(typeof (TestModelValidator), "ContainsA")]
        [DisplayName("Some name for name property")]
        [StringLength(6, ErrorMessage = "Очень длинная строка")]
        public string Name { get; set; }

        [DisplayName("Код")]
        [Required(ErrorMessage="Не надо указывать код")]
        public int Code { get; set; }

        public DateTime Date { get; set; }
    }

    public class SomeTestModelMetadata
    {
        [DisplayName("New name for propety")]
        public string Name { get; set; }
    }

    public class PatientMetadata
    {
        [DisplayName("Имя для токена")]
        public string Token { get; set; }
    }
    
    public class PeriodMetadata
    {
        [DisplayName("Год")]
        [Required(ErrorMessage="Необходимо указать год.")]
        [Range(0, 200, ErrorMessage="Число лет должно быть от 0 до 200.")]
        public int Years { get; set; }
    }
}