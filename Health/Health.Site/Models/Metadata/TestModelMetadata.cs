using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Health.Core.Entities.Virtual;
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
}