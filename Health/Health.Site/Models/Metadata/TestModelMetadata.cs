using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Health.Core.Entities.POCO;
using Health.Site.Models.Validators;

namespace Health.Site.Models.Metadata
{
    public class TestModelMetadata
    {
        [CustomValidation(typeof(TestModelValidator), "ContainsA")]
        [DisplayName("Some name for name property")]
        [StringLength(6, ErrorMessage = "Очень длинная строка")]
        public string Name { get; set; }

        public int Code { get; set; }

        public DateTime Date { get; set; }
    }
}