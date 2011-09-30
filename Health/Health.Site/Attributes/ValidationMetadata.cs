using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using Health.Core.Entities.POCO;

namespace Health.Site.Attributes
{
    public class ValidationMetadata : ActionFilterAttribute
    {
        public Type For { get; set; }
        public Type Use { get; set; }

        public ValidationMetadata(Type @for, Type use)
        {
            For = @for;
            Use = use;
        }
    }
}