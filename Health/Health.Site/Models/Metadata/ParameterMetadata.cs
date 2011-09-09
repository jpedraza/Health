using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Health.Site.Models.Metadata
{
    public class ParameterMetadata
    {
        [DisplayName("Ифентификатор параметра")]
        public int ParameterId { get; set; }
    }
}