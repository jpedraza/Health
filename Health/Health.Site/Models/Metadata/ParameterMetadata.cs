using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Health.Site.Models.Metadata
{
    public class ParameterMetadata
    {
        [DisplayName("Ифентификатор параметра")]
        public int Id { get; set; }

        [DisplayName("Имя параметра")]
        public int Name { get; set; }
    }

    public class IfSubParameterMetadata : ParameterMetadata
    {
        [Required(ErrorMessage = "Укажите параметр.")]
        public new int Id { get; set; }
    }
}