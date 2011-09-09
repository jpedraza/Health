using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Health.Site.Models.Metadata
{
    public class PatientMetadata
    {
        [DisplayName("Логин")]
        public string Login { get; set; }
    }
}