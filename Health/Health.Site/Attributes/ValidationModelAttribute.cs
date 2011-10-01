using System;
using System.Web.Mvc;

namespace Health.Site.Attributes
{
    [AttributeUsage(AttributeTargets.Method,AllowMultiple = true)]
    public class ValidationModelAttribute : ActionFilterAttribute
    {
        public Type For { get; set; }
        public Type Use { get; set; }
        public string Alias { get; set; }

        public ValidationModelAttribute(Type @for, Type use) : this()
        {
            For = @for;
            Use = use;
        }

        public ValidationModelAttribute() { Alias = "form"; }
    }
}